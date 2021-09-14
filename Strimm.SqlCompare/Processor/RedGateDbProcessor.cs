using log4net;
using log4net.Repository.Hierarchy;
using RedGate.Shared.SQL.ExecutionBlock;
using RedGate.SQLCompare.Engine;
using RedGate.SQLCompare.Engine.Filter;
using RedGate.SQLCompare.Engine.ReadFromBackup;
using RedGate.SQLCompare.Engine.ReadFromFolder;
using Strimm.SqlCompare.Enums;
using Strimm.SqlCompare.Model;
using Strimm.SqlCompare.Settings;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strimm.SqlCompare.Extensions;
using Strimm.SqlCompare.Exceptions;
using RedGate.Shared.Utils;
using System.Text.RegularExpressions;

namespace Strimm.SqlCompare.Processor
{
    public class RedGateDbProcessor : IDbProcessor
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RedGateDbProcessor));

        /// <summary>
        /// This method will take snapshot of the target database
        /// </summary>
        /// <param name="targetDbConnection">Connection properties for a target database</param>
        public string SaveDbSnapshot(DbConnectionProperties targetDbConnection)
        {
            Logger.Info(String.Format("Taking db snapshot of an existing database: {0}\\{1}", targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName));

            string destSnapshotFile;

            using (Database destDb = new Database())
            {
                destSnapshotFile = GlobalConfig.GetDbSnapshotFileName(targetDbConnection.DatabaseName);

                Logger.Debug(String.Format("Registering database: {0}\\{1}", targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName));
                destDb.Register(GetConnectionProperties(targetDbConnection), Options.Default);

                Logger.Debug(String.Format("Saving database snapshot: {0}\\{1}", targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName));
                destDb.SaveToDisk(destSnapshotFile);
            }

            return destSnapshotFile;
        }

        /// <summary>
        ///  This method will restore a previously saved and latest database snapshot to the target database
        /// </summary>
        /// <param name="targetDbConnection">Connection properties for a target database</param>
        /// <param name="snapshotFilePath">Overide path for a snapshot file to use</param>
        public bool RestoreDbSnapshot(DbConnectionProperties targetDbConnection, string snapshotFilePath)
        {
            Logger.Info(String.Format("Restoring db from an existing backup: {0}\\{1}", targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName));

            bool isSuccess = false;

            FileInfo snapshotFile = new FileInfo(snapshotFilePath);
            if (!snapshotFile.Exists)
            {
                throw new ProcessorException(String.Format("Snapshot cannot be restored. File '{0}' does not exist.", snapshotFilePath));
            }

            using (Database target = new Database(), targetSnapshot = new Database())
            {
                ConnectionProperties properties = GetConnectionProperties(targetDbConnection);

                Logger.Debug(String.Format("Registering database: {0}\\{1}", targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName));
                target.Register(properties, Options.Default);

                // Load content of the snapshot file
                targetSnapshot.LoadFromDisk(snapshotFilePath);

                // Compare the database against the scripts.
                // Comparing in this order makes the target db the second database
                Differences snapshotVsTarget = targetSnapshot.CompareWith(target, Options.Default);

                // Select all of the differences for synchronization
                snapshotVsTarget.ToList().ForEach(x => x.Selected = true);

                // Calculate the work to do using sensible default options
                Work work = new Work();
                work.BuildFromDifferences(snapshotVsTarget, Options.Default, true);

                // We can now access the messages and warnings
                Logger.Debug("Warnings:");
                work.Messages.ToList().ForEach(x => Logger.Debug(String.Format("Work message: {0}", x.Text)));

                Logger.Debug("Warnings:");
                work.Warnings.ToList().ForEach(x => Logger.Debug(String.Format("Work warning: {0}", x.Text)));

                isSuccess = SyncDatabaseChanges(properties, work, null, null, false);
            }

            return isSuccess;
        }

        /// <summary>
        /// This method will backup target database to a script folder, all database entities will be
        /// written out to the disk as individual script/sql files
        /// </summary>
        /// <param name="targetDbConnection">Connection properties for a target database</param>
        public string BackupDbToScriptFolder(DbConnectionProperties targetDbConnection)
        {
            Logger.Info(String.Format("Generating db backup from an existing database: {0}\\{1}", targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName));

            string folder = null;

            using (BackupSetDatabase destDb = new BackupSetDatabase())
            {
                folder = GlobalConfig.CreateNewDbBackupScriptFolder(targetDbConnection.DatabaseName);

                var properties = GetConnectionProperties(targetDbConnection);

                Logger.Debug(String.Format("Registering database: {0}\\{1}", targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName));
                destDb.Register(properties, Options.Default);

                Logger.Debug(String.Format("Saving database snapshot: {0}\\{1}", targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName));
                destDb.SaveToFolder(folder, new ScriptDatabaseInformation() );
            }

            return folder;
        }

        /// <summary>
        /// This method will restore SQL scripts from the latest database backup to a target database
        /// </summary>
        /// <param name="targetDbConnection">Connection properties for a target database</param>
        /// <param name="scriptFolder">Override location of the script folder to use</param>
        public bool RestoreDbFromScriptFolder(DbConnectionProperties targetDbConnection, string scriptFolder)
        {
            Logger.Info(String.Format("Restoring db from an existing backup: {0}\\{1}", targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName));

            bool isSuccess = false;

            DirectoryInfo folder = new DirectoryInfo(scriptFolder);
            if (folder == null || !folder.Exists)
            {
                throw new ProcessorException(String.Format("Invalid script folder specified '{0}'", scriptFolder));
            }

            using (Database target = new Database(), scriptDb = new Database())
            {
                var properties = GetConnectionProperties(targetDbConnection);

                Logger.Debug(String.Format("Registering database: {0}\\{1}", targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName));
                target.Register(properties, Options.Default);

                // Establish the schema from the scripts stored in the backup scripts folder
                // Passing in null for the database information parameter causes SQL Compare to read the
                // XML file supplied in the folder.
                scriptDb.Register(scriptFolder, null, Options.Default);

                // Log all parse errors
                scriptDb.ParserMessages.ToList().ForEach(x => Logger.Debug(String.Format("Script Backup Parsing Warning :{0} in {1}, line {2}", x.Type, x.File, x.LineNumber)));

                // Compare the database against the scripts.
                // Comparing in this order makes the target db the second database
                Differences scriptFolderVsTarget = scriptDb.CompareWith(target, Options.Default);

                // Select all of the differences for synchronization
                scriptFolderVsTarget.ToList().ForEach(x => x.Selected = true);

                // Calculate the work to do using sensible default options
                // The database backup script folder is to be updated, so the runOnTwo parameter is true
                Work work = new Work();
                work.BuildFromDifferences(scriptFolderVsTarget, Options.Default, true);

                // We can now access the messages and warnings
                Logger.Debug("Warnings:");
                work.Messages.ToList().ForEach(x => Logger.Debug(String.Format("Work message: {0}", x.Text)));

                Logger.Debug("Warnings:");
                work.Warnings.ToList().ForEach(x => Logger.Debug(String.Format("Work warning: {0}", x.Text)));

                isSuccess = SyncDatabaseChanges(properties, work, null, null, false);
            }

            return isSuccess;
        }

        /// <summary>
        /// This method will generate diff script between two databases, a souce database and a target database.
        /// Client can specify include & exclude script options as well as the flag if only jira items should be scripted
        /// </summary>
        /// <param name="sourceDbConnection">Source database connection properties</param>
        /// <param name="targetDbConnection">Target database connection properties</param>
        /// <param name="includeOptions">include script options</param>
        /// <param name="excludeOptions">exclude script options</param>
        /// <param name="scriptJiraItemsOnly">script jira items only flag (applies to sprocs, views, functions)</param>
        /// <returns>SQL script filepath</returns>
        public string GenerateSqlDiffScriptBetweenTwoDatabases(DbConnectionProperties sourceDbConnection, DbConnectionProperties targetDbConnection, List<KeyValuePair<DiffFilterType, string>> includeOptions, List<KeyValuePair<DiffFilterType, string>> excludeOptions, bool scriptJiraItemsOnly)
        {
            Logger.Info(String.Format("Generating db diff script for source database '{0}\\{1}' and target database '{2}\\{3}'", sourceDbConnection.DatabaseServer, sourceDbConnection.DatabaseName, targetDbConnection.DatabaseName, targetDbConnection.DatabaseName));

            using (Database srcDb = new Database(), destDb = new Database())
            {
                string diffScriptFilePath = GlobalConfig.GenerateFilePathForDatabaseSqlDiffScript(sourceDbConnection.DatabaseName, targetDbConnection.DatabaseName);

                try
                {
                    var sourceProperties = GetConnectionProperties(sourceDbConnection);
                    var targetProperties = GetConnectionProperties(targetDbConnection);
                    
                    Logger.Debug(String.Format("Registering with source database: {0}\\{1}", sourceDbConnection.DatabaseServer, sourceDbConnection.DatabaseName));
                    srcDb.Register(sourceProperties, Options.Default);

                    Logger.Debug(String.Format("Registering with destination database: {0}\\{1}", targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName));
                    destDb.Register(targetProperties, Options.Default);

                    Differences srcVsDestDbDifferences = srcDb.CompareWith(destDb, Options.Default);

                    using (var file = new StreamWriter(diffScriptFilePath))
                    {
                        Work w = new Work();

                        srcVsDestDbDifferences.ToList().ForEach(x => x.Selected = true);

                        w.BuildFromDifferences(srcVsDestDbDifferences, Options.Default.Except(new [] 
                                                                            {
                                                                               Options.AddDatabaseUseStatement,
                                                                               Options.CaseSensitiveObjectDefinition,
                                                                               Options.DropAndCreateInsteadOfAlter,
                                                                               Options.IgnoreOwners,
                                                                               Options.IgnorePermissions,
                                                                               Options.IgnoreSchemaObjectAuthorization,
                                                                               Options.IgnoreUsers
                                                                            }), true);

                        var jiraItemsRegex = scriptJiraItemsOnly ? GlobalConfig.GetApplicableJiraItemsRegex() : null;

                        for (int i = 0; i < w.ExecutionBlock.BatchCount; i++)
                        {
                            var batch = w.ExecutionBlock.GetBatch(i);
                            var sqlContent = batch.Contents;
                            //var regex = new Regex("(CREATE LOGIN|sp_addrolemember|sp_droprolemember|AUTHORIZATION|db_accessadmin|db_backupoperator|Altering members|DROP USER|CREATE SCHEMA)w*");
                            //if (!regex.Match(sqlContent).Success && !batch.Marker)

                            if (!batch.Marker && ShouldSyncDiff(batch, jiraItemsRegex))
                            {
                                file.WriteLine(sqlContent);
                            }
                        }

                        file.Flush();
                        file.Close();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(String.Format("Error occured while generating diff script"), ex);
                }

                return diffScriptFilePath;
            }            
        }

        public bool SyncDatabases(DbConnectionProperties sourceDbConnection, DbConnectionProperties targetDbConnection, List<KeyValuePair<DiffFilterType, string>> includeOptions, List<KeyValuePair<DiffFilterType, string>> excludeOptions, bool scriptJiraItemsOnly)
        {
            Logger.Info(String.Format("Generating db diff script for source database '{0}\\{1}' and target database '{2}\\{3}'", sourceDbConnection.DatabaseServer, sourceDbConnection.DatabaseName, targetDbConnection.DatabaseName, targetDbConnection.DatabaseName));

            bool isSuccess = false;

            using (Database srcDb = new Database(), destDb = new Database())
            {
                try
                {
                    var sourceProperties = GetConnectionProperties(sourceDbConnection);
                    var targetProperties = GetConnectionProperties(targetDbConnection);

                    Logger.Debug(String.Format("Registering with source database: {0}\\{1}", sourceDbConnection.DatabaseServer, sourceDbConnection.DatabaseName));
                    srcDb.Register(sourceProperties, Options.Default);

                    Logger.Debug(String.Format("Registering with destination database: {0}\\{1}", targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName));
                    destDb.Register(targetProperties, Options.Default);

                    Differences srcVsDestDbDifferences = srcDb.CompareWith(destDb, Options.Default);

                    Work work = new Work();

                    work.BuildFromDifferences(srcVsDestDbDifferences, Options.Default, true);

                    DifferenceFilter df = CreateFilterFromClientOptions(includeOptions);
                    var filteredDifferences = df.FilterDifferences(srcVsDestDbDifferences);

                    if (filteredDifferences.Count() > 0)
                    {
                        filteredDifferences.ToList().ForEach(x => x.Selected = true);
                    }
                    else
                    {
                        srcVsDestDbDifferences.ToList().ForEach(x => x.Selected = true);
                    }

                    using (ExecutionBlock block = work.ExecutionBlock)
                    {
                        // Display the SQL used to deploy
                        Logger.Debug(String.Format("SQL to deploy: {0}", block.GetString()));

                        // Finally, use a BlockExecutor to run the SQL against the target database
                        BlockExecutor executor = new BlockExecutor();
                        executor.ExecuteBlock(block, targetProperties.ToDBConnectionInformation());
                    }

                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    Logger.Error(String.Format("Error occured while generating diff script"), ex);
                }
            }

            return isSuccess;
        }

        public bool SyncDatabaseFromExistingDiffFile(DbConnectionProperties targetDbConnection, string existingSqlDiffFile)
        {
            Logger.Info(String.Format("Syncing database '{0}\\{1}' from existing diff file '{2}'", targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName, existingSqlDiffFile));

            bool isSuccess = false;

            var targetProperties = GetConnectionProperties(targetDbConnection);
            var sqlContent = ReadExistingDiffSQLFromFile(existingSqlDiffFile);

            using (SqlConnection conn = new SqlConnection(targetProperties.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    sqlContent = sqlContent.Replace("IF @@ERROR <> 0 SET NOEXEC ON", "")
                                           .Replace("SET NUMERIC_ROUNDABORT OFF", "")
                                           .Replace("SET QUOTED_IDENTIFIER OFF", "")
                                           .Replace("IF @@ERROR <> 0 SET NOEXEC ON","");

                    cmd.CommandText = sqlContent;
                    cmd.ExecuteNonQuery();

                    isSuccess = true;
                }
            }

            return isSuccess;
        }

        private string ReadExistingDiffSQLFromFile(string existingSqlDiffFile)
        {
            var file = new FileInfo(existingSqlDiffFile);
            var builder = new StringBuilder();

            if (file != null && file.Exists)
            {
                string line;
                var stream = new StreamReader(file.OpenRead());

                while ((line = stream.ReadLine()) != null)
                {
                    builder.Append(line);
                }
            }
            else
            {
                throw new ProcessorException(String.Format("Unable to read existing SQL diff file '{0}'", existingSqlDiffFile));
            }

            return builder.ToString();
        }

        private ConnectionProperties GetConnectionProperties(DbConnectionProperties properties)
        {
            return properties != null
                        ? new ConnectionProperties(properties.DatabaseServer, properties.DatabaseName, properties.UserName, properties.Password)
                        : new ConnectionProperties();
        }

        //If it's necessary to run the code produced, the BlockExecutor cannot be used, unfortunately, 
        //and the code has to be broken into batches again and run by the ADO .NET provider (SqlCommand). 
        //The final method, actionToRun, will run the ExecutionBlock
        private bool SyncDatabaseChanges(ConnectionProperties targetConnectionProperties, Work work, List<KeyValuePair<DiffFilterType, string>> includeOptions, List<KeyValuePair<DiffFilterType, string>> excludeOptions, bool applyJiraItemsOnly)
        {
            bool isSuccess = false;

            using (SqlConnection conn = new SqlConnection(targetConnectionProperties.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    for (int i = 0; i < work.ExecutionBlock.BatchCount; i++)
                    {
                        var batch = work.ExecutionBlock.GetBatch(i);
                        var jiraItemsRegex = applyJiraItemsOnly ? GlobalConfig.GetApplicableJiraItemsRegex() : null;

                        if (!batch.Marker && ShouldSyncDiff(batch, jiraItemsRegex))
                        {
                            cmd.CommandText = batch.Contents;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    isSuccess = true;
                }
            }

            return isSuccess;
        }

        private static bool ShouldSyncDiff(Batch batch, string jiraItemsRegex)
        {
            bool shouldSync = false;

            if (batch != null)
            {
                var sqlScript = batch.Contents;

                if (!String.IsNullOrWhiteSpace(jiraItemsRegex))
                {
                    var r = new System.Text.RegularExpressions.Regex(jiraItemsRegex);
                    shouldSync = r.Match(sqlScript).Success;
                }
                else
                {
                    var r = new System.Text.RegularExpressions.Regex("(CREATE LOGIN|sp_addrolemember|sp_droprolemember|AUTHORIZATION|db_accessadmin|db_backupoperator|Altering members|DROP USER|CREATE SCHEMA|sp_addextendedproperty)w*");
                    shouldSync = !r.Match(sqlScript).Success;
                }
            }

            return shouldSync;
        }

        private DifferenceFilter CreateFilterFromClientOptions(List<KeyValuePair<DiffFilterType, string>> options)
        {
            var differencesFilter = new DifferenceFilter();

            differencesFilter.SetObjectTypeExclude(ObjectType.User);
            differencesFilter.SetObjectTypeExclude(ObjectType.Role);
            differencesFilter.SetObjectTypeExclude(ObjectType.Route);
            differencesFilter.SetObjectTypeExclude(ObjectType.Rule);
            differencesFilter.SetObjectTypeExclude(ObjectType.Assembly);
            differencesFilter.SetObjectTypeExclude(ObjectType.AsymmetricKey);
            differencesFilter.SetObjectTypeExclude(ObjectType.Certificate);
            differencesFilter.SetObjectTypeExclude(ObjectType.DdlTrigger);
            differencesFilter.SetObjectTypeExclude(ObjectType.ExtendedProperty);
            differencesFilter.SetObjectTypeExclude(ObjectType.FullTextCatalog);
            differencesFilter.SetObjectTypeExclude(ObjectType.FullTextStoplist);
            differencesFilter.SetObjectTypeExclude(ObjectType.SymmetricKey);
            differencesFilter.SetObjectTypeExclude(ObjectType.Synonym);
            differencesFilter.SetObjectTypeExclude(ObjectType.XmlSchemaCollection);

            differencesFilter.SetFilterObjectTypeIncludesFromOptions(options);

            return differencesFilter;
        }

        private void StatusCallback(object sender, StatusEventArgs e)
        {
            // Fired by the SqlProvider to indicate events
            if (e.Message != null)
            {
                Console.WriteLine("\r{0}", e.Message);
            }

            if (e.Percentage != -1)
            {
                Console.Write("\r \r{0}%", e.Percentage);
            }

        }
    }
}

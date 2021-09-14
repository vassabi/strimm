using log4net;
using Strimm.SqlCompare.Exceptions;
using Strimm.SqlCompare.Model;
using Strimm.SqlCompare.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using xSQL.Licensing.SqlServer;
using xSQL.Schema.Core;
using xSQL.Schema.SqlServer;
using xSQL.SchemaCompare.SqlServer;

namespace Strimm.SqlCompare.Processor
{
    public class xSqlDbProcessor : IDbProcessor
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(xSqlDbProcessor));

        public xSqlDbProcessor()
        {
            SqlLicenseStore.CreateInstance(new SqlSchemaSdkProduct());
            SqlLicenseStore.Instance.RegisterLicense("SD70110230-1C505B3E34-20454", "XmYfHXtNkQcT+/gZrIbcN1IBTzzPLXkREjNXskwMKdc=");
        }

        public string SaveDbSnapshot(DbConnectionProperties targetDbConnection)
        {
            Logger.Info(String.Format("Taking db snapshot of an existing database: {0}\\{1}", targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName));

            SqlServer server;
            SqlDatabase database;

            string destSnapshotFile = GlobalConfig.GetDbSnapshotFileName(targetDbConnection.DatabaseName);

            //--create the SQL Server object, connect using Windows authentication
            server = new SqlServer(targetDbConnection.DatabaseServer, targetDbConnection.GetConnectionString());

            //--create the database object
            database = server.GetDatabase(targetDbConnection.DatabaseName);

            //--attach an handler to SchemaOperation even in order to get progress information during the schema read
            database.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);

            //--attach an handler to SnapshotOperation event to get snapshot feedback
            database.SnapshotOperation += new EventHandler<SnapshotOperationEventArgs>(database_SnapshotOperation);

            //--read the database schema
            database.ReadSchema();

            //--create the snapshot
            database.SaveToSnapshot(destSnapshotFile);

            return destSnapshotFile;
        }

        public bool RestoreDbSnapshot(DbConnectionProperties targetDbConnection, string snapshotFilePath)
        {
            SqlDatabase database;
            SqlServer targetServer;
            SqlDatabase yDatabase;
            SqlSchemaCompare comparer;
            ScriptManager sqlScript;
            ScriptExecutionStatusEnum status;

            bool isSuccess = false;

            FileInfo snapshotFile = new FileInfo(snapshotFilePath);
            if (!snapshotFile.Exists)
            {
                throw new ProcessorException(String.Format("Snapshot cannot be restored. File '{0}' does not exist.", snapshotFilePath));
            }

            //--create the database from the snapshot file
            database = SqlDatabase.CreateFromSnapshot(snapshotFilePath, database_SnapshotOperation);
            targetServer = new SqlServer(targetDbConnection.DatabaseServer, targetDbConnection.GetConnectionString());

            yDatabase = targetServer.GetDatabase(targetDbConnection.DatabaseName);

            comparer = new SqlSchemaCompare(database, yDatabase);

            // exclude some objects from the comparison
            comparer.Options.CompareUsers = false;
            comparer.Options.CompareSchemas = false;
            comparer.Options.CompareDatabaseRoles = false;
            comparer.Options.CompareApplicationRoles = false;

            // attach event handlers to these events in order to get some progress information during the schema read and compare
            comparer.LeftDatabase.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);
            comparer.RightDatabase.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);
            comparer.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);

            // use the generic event EntityPairingFinished to exclude an object from the comparison or
            // the SqlTablePairingFinished event which is raised specifically for database tables.
            // the table-specific event performs slightly better since it has to check only tables and not every object in the database
            // objects are usually excluded after the pairing is finished. 
            comparer.SqlTablePairingFinished += new EventHandler<SqlTablePairEventArgs>(comparer_SqlTablePairingFinished);

            // step 1: read the schema 
            comparer.ReadSchema();

            // step 2: pair the database objects
            comparer.PairObjects();

            // step 3: compare the schema
            comparer.Compare();

            // check for errors that could have occurred during the schema compare. 
            // some errors are handled quietly and do not stop the process, those that are critical throw exceptions
            // quiet errors are collected and stored into the ErrorRepository object
            if (ErrorRepository.Instance.HasErrors())
            {
                Console.WriteLine("Some errors occurred during the database compare");
                Console.Write(ErrorRepository.Instance.GetErrors());
            }

            // check the database status; exit if no schema differences are found.
            if (comparer.SqlDatabasePair.ComparisonStatus == ComparisonStatusEnum.Equal)
            {
                Logger.Debug("Nothing to sync. Both databases are identical");
                return false;
            }

            // step 4: get the T-SQL script intended for the right database; that is the script that should be executed 
            // on Target database in order to make it the same as the Source database
            sqlScript = comparer.GetRightDatabaseScript();

            if (!sqlScript.IsEmpty())
            {
                // use this event to get some progress during the script execution
                sqlScript.SchemaScriptExecuting += new EventHandler<SchemaScriptEventArgs>(sqlScript_SchemaScriptExecuting);

                // execute the synchronization script 
                status = sqlScript.Execute();

                // check the status
                if (status == ScriptExecutionStatusEnum.Succeeded)
                {
                    Logger.Debug("Database synchronization succeeded.");
                    isSuccess = true;

                }
                else if (status == ScriptExecutionStatusEnum.Canceled)
                {
                    Logger.Debug("Database synchronization was canceled.");
                    isSuccess = true;
                }
                else
                {
                    //--check for silent errors
                    if (ErrorRepository.Instance.HasErrors())
                    {
                        Logger.Debug("Some errors occurred during the script execution.");
                        Logger.Error(ErrorRepository.Instance.GetErrors());
                    }
                }
            }

            return isSuccess;
        }

        public string BackupDbToScriptFolder(DbConnectionProperties targetDbConnection)
        {
            SqlServer server;
            SqlDatabase database;
            ScriptingOptions options;
            string scriptFolder;

            //--create the SQL Server object, connect using Windows authentication
            server = new SqlServer(targetDbConnection.DatabaseServer, targetDbConnection.GetConnectionString());

            //--create the database object
            database = server.GetDatabase(targetDbConnection.DatabaseName);

            //--attach an handler to database.SchemaOperation even in order to get progress information during the schema read
            database.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);

            //--read the database schema
            database.ReadSchema();

            //--create scripting options; 
            options = new ScriptingOptions()
            {
                CreateScript = true,
                DropScript = true,
                AlterScript = true,
                CheckExistence = true,
                ScriptTriggers = true
            };

            scriptFolder = GlobalConfig.CreateNewDbBackupScriptFolder(targetDbConnection.DatabaseName);

            var sprocs = database.SqlProcedures;
            var tables = database.SqlTables;
            var views = database.SqlViews;
            var functions = database.SqlFunctions;
            var triggers = database.SqlDatabaseTriggers;
            var roles = database.SqlDatabaseRoles;
            var users = database.SqlUsers;
            var udts = database.SqlUserDefinedDataTypes;
            var udtts = database.SqlUserDefinedTableTypes;

            if (CheckFolderAndCreatedIfMissing(scriptFolder))
            {
                if (sprocs != null)
                {
                    if (CheckFolderAndCreatedIfMissing(Path.Combine(scriptFolder, "Stored Procedures")))
                    {
                        string folder = Path.Combine(scriptFolder, "Stored Procedures");

                        sprocs.ToList().ForEach(x =>
                        {
                            var name = x.Name;
                            var sql = x.GetScript(options);
                            var filename = Path.Combine(folder, String.Format("{0}.sql", name));
                            WriterSqlContent(sql, filename);
                        });
                    }

                    if (CheckFolderAndCreatedIfMissing(Path.Combine(scriptFolder, "Tables")))
                    {
                        string folder = Path.Combine(scriptFolder, "Tables");

                        tables.ToList().ForEach(x =>
                        {
                            var name = x.Name;
                            var sql = x.GetScript(options);
                            var filename = Path.Combine(folder, String.Format("{0}.sql", name));
                            WriterSqlContent(sql, filename);
                        });
                    }

                    if (CheckFolderAndCreatedIfMissing(Path.Combine(scriptFolder, "Views")))
                    {
                        string folder = Path.Combine(scriptFolder, "Views");

                        views.ToList().ForEach(x =>
                        {
                            var name = x.Name;
                            var sql = x.GetScript(options);
                            var filename = Path.Combine(folder, String.Format("{0}.sql", name));
                            WriterSqlContent(sql, filename);
                        });
                    }

                    if (CheckFolderAndCreatedIfMissing(Path.Combine(scriptFolder, "Functions")))
                    {
                        string folder = Path.Combine(scriptFolder, "Functions");

                        functions.ToList().ForEach(x =>
                        {
                            var name = x.Name;
                            var sql = x.GetScript(options);
                            var filename = Path.Combine(folder, String.Format("{0}.sql", name));
                            WriterSqlContent(sql, filename);
                        });
                    }

                    if (CheckFolderAndCreatedIfMissing(Path.Combine(scriptFolder, "Triggers")))
                    {
                        string folder = Path.Combine(scriptFolder, "Triggers");

                        triggers.ToList().ForEach(x =>
                        {
                            var name = x.Name;
                            var sql = x.GetScript(options);
                            var filename = Path.Combine(folder, String.Format("{0}.sql", name));
                            WriterSqlContent(sql, filename);
                        });
                    }

                    if (CheckFolderAndCreatedIfMissing(Path.Combine(scriptFolder, "Roles")))
                    {
                        string folder = Path.Combine(scriptFolder, "Roles");

                        roles.ToList().ForEach(x =>
                        {
                            var name = x.Name;
                            var sql = x.GetScript(options);
                            var filename = Path.Combine(folder, String.Format("{0}.sql", name));
                            WriterSqlContent(sql, filename);
                        });
                    }

                    if (CheckFolderAndCreatedIfMissing(Path.Combine(scriptFolder, "Users")))
                    {
                        string folder = Path.Combine(scriptFolder, "Users");

                        users.ToList().ForEach(x =>
                        {
                            var name = x.Name;
                            var sql = x.GetScript(options);
                            var filename = Path.Combine(folder, String.Format("{0}.sql", name));
                            WriterSqlContent(sql, filename);
                        });
                    }

                    if (CheckFolderAndCreatedIfMissing(Path.Combine(scriptFolder, "User_Defined_Data_Types")))
                    {
                        string folder = Path.Combine(scriptFolder, "User_Defined_Data_Types");

                        udts.ToList().ForEach(x =>
                        {
                            var name = x.Name;
                            var sql = x.GetScript(options);
                            var filename = Path.Combine(folder, String.Format("{0}.sql", name));
                            WriterSqlContent(sql, filename);
                        });
                    }

                     if (CheckFolderAndCreatedIfMissing(Path.Combine(scriptFolder, "User_Defined_Table_Types")))
                    {
                        string folder = Path.Combine(scriptFolder, "User_Defined_Table_Types");

                        udtts.ToList().ForEach(x =>
                        {
                            var name = x.Name;
                            var sql = x.GetScript(options);
                            var filename = Path.Combine(folder, String.Format("{0}.sql", name));
                            WriterSqlContent(sql, filename);
                        });
                    }             
                }
            }

            return scriptFolder;
        }

        public string GenerateSqlDiffScriptBetweenTwoDatabases(DbConnectionProperties sourceDbConnection, DbConnectionProperties targetDbConnection, List<KeyValuePair<Enums.DiffFilterType, string>> includeOptions, List<KeyValuePair<Enums.DiffFilterType, string>> excludeOptions, bool scriptJiraItemsOnly)
        {
            SqlServer srcServer;
            SqlServer targetServer;
            SqlDatabase xDatabase, yDatabase;
            SqlSchemaCompare comparer;
            ScriptManager sqlScript;
            ScriptExecutionStatusEnum status;
            SqlTablePair pair;

            string diffScriptFilePath = GlobalConfig.GenerateFilePathForDatabaseSqlDiffScript(sourceDbConnection.DatabaseName, targetDbConnection.DatabaseName);

            // create the SQL Server object
            srcServer = new SqlServer(sourceDbConnection.DatabaseServer, sourceDbConnection.GetConnectionString());
            targetServer = new SqlServer(targetDbConnection.DatabaseServer, targetDbConnection.GetConnectionString());

            // create the left database
            xDatabase = srcServer.GetDatabase(sourceDbConnection.DatabaseName);

            // create the right database
            yDatabase = targetServer.GetDatabase(targetDbConnection.DatabaseName);

            // create the comparer
            comparer = new SqlSchemaCompare(xDatabase, yDatabase);

            // attach event handlers to these events in order to get some progress information during the schema read and compare
            comparer.LeftDatabase.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);
            comparer.RightDatabase.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);
            comparer.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);

            // use the generic event EntityPairingFinished to exclude an object from the comparison or
            // the SqlTablePairingFinished event which is raised specifically for database tables.
            // the table-specific event performs slightly better since it has to check only tables and not every object in the database
            // objects are usually excluded after the pairing is finished. 
            comparer.SqlTablePairingFinished += new EventHandler<SqlTablePairEventArgs>(comparer_SqlTablePairingFinished);

            // step 1: read the schema 
            comparer.ReadSchema();

            // step 2: pair the database objects
            comparer.PairObjects();

            // this is another way of an excluding an object; it should be done always after the pairing finishes
            pair = comparer.SqlTablePairs["employees"];
            if (pair != null)
                pair.Included = false;


            // step 3: compare the schema
            comparer.Compare();

            // check for errors that could have occurred during the schema compare. 
            if (ErrorRepository.Instance.HasErrors())
            {
                Logger.Debug("Some errors occurred during the database compare");
                Logger.Error(ErrorRepository.Instance.GetErrors());
            }

            // check the database status; exit if no schema differences are found.
            if (comparer.SqlDatabasePair.ComparisonStatus == ComparisonStatusEnum.Equal)
            {
                return null;
            }

            // step 4: get the T-SQL script intended for the right database; that is the script that should be executed 
            // on Target database in order to make it the same as the Source database
            sqlScript = comparer.GetRightDatabaseScript();

            if (!sqlScript.IsEmpty())
            {
                if (scriptJiraItemsOnly)
                {
                    FilterSqlScriptForJiraItems(sqlScript);
                }

                using (var file = new StreamWriter(diffScriptFilePath))
                {
                    var sqlContent = sqlScript.GetScript();

                    file.Write(sqlContent);

                    file.Flush();
                    file.Close();
                }
            }

            return diffScriptFilePath;
        }

        public bool SyncDatabases(DbConnectionProperties sourceDbConnection, DbConnectionProperties targetDbConnection, List<KeyValuePair<Enums.DiffFilterType, string>> includeOptions, List<KeyValuePair<Enums.DiffFilterType, string>> excludeOptions, bool scriptJiraItemsOnly)
        {
            SqlServer srcServer;
            SqlServer targetServer;
            SqlDatabase xDatabase, yDatabase;
            SqlSchemaCompare comparer;
            ScriptManager sqlScript;
            ScriptExecutionStatusEnum status;

            bool isSuccess = true;

            // create the SQL Server object
            srcServer = new SqlServer(sourceDbConnection.DatabaseServer, sourceDbConnection.GetConnectionString());
            targetServer = new SqlServer(targetDbConnection.DatabaseServer, targetDbConnection.GetConnectionString());

            // create the left database
            xDatabase = srcServer.GetDatabase(sourceDbConnection.DatabaseName);

            // create the right database
            yDatabase = srcServer.GetDatabase(targetDbConnection.DatabaseName);

            // create the comparer
            comparer = new SqlSchemaCompare(xDatabase, yDatabase);

            // exclude some objects from the comparison
            comparer.Options.CompareUsers = false;
            comparer.Options.CompareSchemas = false;
            comparer.Options.CompareDatabaseRoles = false;
            comparer.Options.CompareApplicationRoles = false;

            // attach event handlers to these events in order to get some progress information during the schema read and compare
            comparer.LeftDatabase.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);
            comparer.RightDatabase.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);
            comparer.SchemaOperation += new EventHandler<SchemaOperationEventArgs>(database_SchemaOperation);

            // use the generic event EntityPairingFinished to exclude an object from the comparison or
            // the SqlTablePairingFinished event which is raised specifically for database tables.
            // the table-specific event performs slightly better since it has to check only tables and not every object in the database
            // objects are usually excluded after the pairing is finished. 
            comparer.SqlTablePairingFinished += new EventHandler<SqlTablePairEventArgs>(comparer_SqlTablePairingFinished);

            // step 1: read the schema 
            comparer.ReadSchema();

            // step 2: pair the database objects
            comparer.PairObjects();

            // step 3: compare the schema
            comparer.Compare();

            // check for errors that could have occurred during the schema compare. 
            // some errors are handled quietly and do not stop the process, those that are critical throw exceptions
            // quiet errors are collected and stored into the ErrorRepository object
            if (ErrorRepository.Instance.HasErrors())
            {
                Console.WriteLine("Some errors occurred during the database compare");
                Console.Write(ErrorRepository.Instance.GetErrors());
            }

            // check the database status; exit if no schema differences are found.
            if (comparer.SqlDatabasePair.ComparisonStatus == ComparisonStatusEnum.Equal)
            {
                Logger.Debug("Nothing to sync. Both databases are identical");
                return false;
            }

            // step 4: get the T-SQL script intended for the right database; that is the script that should be executed 
            // on Target database in order to make it the same as the Source database
            sqlScript = comparer.GetRightDatabaseScript();

            if (!sqlScript.IsEmpty())
            {
                if (scriptJiraItemsOnly)
                {
                    FilterSqlScriptForJiraItems(sqlScript);
                }

                // use this event to get some progress during the script execution
                sqlScript.SchemaScriptExecuting += new EventHandler<SchemaScriptEventArgs>(sqlScript_SchemaScriptExecuting);

                // execute the synchronization script 
                status = sqlScript.Execute();

                // check the status
                if (status == ScriptExecutionStatusEnum.Succeeded)
                {
                    Logger.Debug("Database synchronization succeeded.");
                    isSuccess = true;
                    
                }
                else if (status == ScriptExecutionStatusEnum.Canceled)
                {
                    Logger.Debug("Database synchronization was canceled.");
                    isSuccess = true;
                }
                else
                {
                    //--check for silent errors
                    if (ErrorRepository.Instance.HasErrors())
                    {
                        Logger.Debug("Some errors occurred during the script execution.");
                        Logger.Error(ErrorRepository.Instance.GetErrors());
                    }
                }
            }

            return isSuccess;
        }

        #region Public Method - Not Implemented

        public bool SyncDatabaseFromExistingDiffFile(DbConnectionProperties destDbConnection, string existingSqlDiffFile)
        {
            throw new NotImplementedException();
        }

        public bool RestoreDbFromScriptFolder(DbConnectionProperties targetDbConnection, string scriptFolder)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        private bool WriterSqlContent(string sql, string targetFile)
        {
            if (String.IsNullOrEmpty(sql) || String.IsNullOrEmpty(targetFile))
            {
                return false;
            }

            using (var writer = new StreamWriter(targetFile))
            {
                writer.Write(sql);
                writer.Flush();
                writer.Close();
            }

            return true;
        }

        private bool CheckFolderAndCreatedIfMissing(string folder)
        {
            if (String.IsNullOrEmpty(folder))
            {
                return false;
            }

            DirectoryInfo sprocFolder = new DirectoryInfo(folder);
            if (!sprocFolder.Exists)
            {
                sprocFolder.Create();
            }

            return true;
        }

        private void FilterSqlScriptForJiraItems(ScriptManager sqlScript)
        {
            var jiraItemsRegex = GlobalConfig.GetApplicableJiraItemsRegex();

            var cleanScriptCollection = new SchemaScriptFragmentCollection();

            var regex = new Regex(jiraItemsRegex);

            sqlScript.TransactionalScripts.ForEach(x =>
            {
                var transScript = x.GetScript();
                if (regex.Match(transScript).Success)
                {
                    cleanScriptCollection.Add(x);
                }
            });

            sqlScript.TransactionalScripts.Clear();
            sqlScript.TransactionalScripts.AddRange(cleanScriptCollection);
        }

        #endregion

        #region Events

        private static void database_SnapshotOperation(object sender, SnapshotOperationEventArgs e)
        {
            //--exclude verbose messages
            if (e.Message.MessageType != OperationMessageTypeEnum.Verbose)
                Logger.Debug(e.Message.Text);
        }

        private static void database_SchemaOperation(object sender, SchemaOperationEventArgs e)
        {
            //--exclude verbose messages
            if (e.Message.MessageType != OperationMessageTypeEnum.Verbose)
                Logger.Debug(e.Message.Text);
        }

        private static void sqlScript_SchemaScriptExecuting(object sender, SchemaScriptEventArgs e)
        {
            Logger.Debug(String.Format("{0} {1}", DateTime.Now.ToString("HH:mm:ss"), e.Script));
        }

        private static void comparer_SqlTablePairingFinished(object sender, SqlTablePairEventArgs e)
        {
            if (e.Pair.ContainsMember("employees", SqlEntityTypeEnum.Table))
                e.Pair.Included = false;
        }

        #endregion

    }
}

using log4net;
using Strimm.SqlCompare.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Strimm.SqlCompare.Extensions;
using Strimm.SqlCompare.Processor;
using Strimm.SqlCompare.Model;
using Strimm.SqlCompare.Settings;
using Strimm.SqlCompare.Exceptions;

namespace Strimm.SqlCompare.Compare
{
    public class SchemaComparer
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SchemaComparer));

        private IDbProcessor activeProcessor;

        public SchemaComparer()
        {
            activeProcessor = ProcessorFactory.GetInstance().GetProcessor();
        }

        /// <summary>
        /// This method will execute user defined database compare option
        /// </summary>
        /// <param name="srcDbConnection">Source database connection</param>
        /// <param name="destDbConnection">Destination database connection</param>
        /// <param name="options">Sync compare options between 2 databases</param>
        /// <param name="action">SqlOption enum that specifies what action should be performed between 2 databases</param>
        public void Execute(RunTimeParameters runtimeParams)
        {
            switch (runtimeParams.ActionToPerform)
            {
                case SqlAction.SQL_BACKUP_TO_SCRIPT_FOLDER:
                    BackupDatabaseToScriptFolder(runtimeParams.TargetDbConnection);
                    break;
                case SqlAction.SQL_RESTORE_FROM_SCRIPT_FOLDER:
                    RestoreDatabaseFromScriptFolder(runtimeParams.TargetDbConnection, runtimeParams.CustomScriptFolderPath);
                    break;
                case SqlAction.SQL_ROLLBACK_LATEST_CHANGES:
                    RollbackLatestDatabaseChanges(runtimeParams.TargetDbConnection, runtimeParams.CustomSnapshotFilePath);
                    break;
                case SqlAction.SQL_SYNC_SRC_TO_DEST_DB:
                    SyncDatabases(runtimeParams.SourceDbConnection, runtimeParams.TargetDbConnection, runtimeParams.IncludeOptions, runtimeParams.ExcludeOptions, runtimeParams.ScriptJiraItemsOnly);
                    break;
                case SqlAction.SQL_TAKE_DB_SNAPSHOT:
                    TakeDatabaseSnapshot(runtimeParams.TargetDbConnection);
                    break;
                case SqlAction.SQL_GEN_SQL_DIFF_SCRIPT:
                    GenerateSqlDiffFileForDatabase(runtimeParams.SourceDbConnection, runtimeParams.TargetDbConnection, runtimeParams.IncludeOptions, runtimeParams.ExcludeOptions, runtimeParams.ScriptJiraItemsOnly);
                    break;
                case SqlAction.SQL_SYNC_FROM_DIFF_SCRIPT:
                    SyncDatabaseFromDiffFile(runtimeParams.TargetDbConnection, runtimeParams.ExistingDiffFilePath);
                    break;
                default:
                    {
                        Logger.Error("Specified SQL Action is not supported");
                        break;
                    }
            }
        }

        private void GenerateSqlDiffFileForDatabase(DbConnectionProperties sourceDbConnection, DbConnectionProperties targetDbConnection, List<KeyValuePair<DiffFilterType, string>> includeOptions, List<KeyValuePair<DiffFilterType, string>> excludeOptions, bool scriptJiraItemsOnly)
        {
            Logger.Info(String.Format("Generating SQL diff file between '{0}\\{1}' and '{2}\\{3}'", sourceDbConnection.DatabaseServer, sourceDbConnection.DatabaseName, targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName));

            try
            {
                string diffFilePath = activeProcessor.GenerateSqlDiffScriptBetweenTwoDatabases(sourceDbConnection, targetDbConnection, includeOptions, excludeOptions, scriptJiraItemsOnly);

                if ((new FileInfo(diffFilePath)).Exists)
                {
                    Logger.Debug(String.Format("Successfully generated SQL diff file '{0}' between '{1}\\{2}' and '{3}\\{4}'", diffFilePath, sourceDbConnection.DatabaseServer, sourceDbConnection.DatabaseName, targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName));
                }
                else
                {
                    Logger.Debug(String.Format("Failed to generat SQL diff file between '{0}\\{1}' and '{2}\\{3}'", sourceDbConnection.DatabaseServer, sourceDbConnection.DatabaseName, targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName));
                }
            }
            catch (ProcessorException pex)
            {
                Logger.Error(String.Format("Error occured while generating SQL diff file between '{0}\\{1}' and '{2}\\{3}'", sourceDbConnection.DatabaseServer, sourceDbConnection.DatabaseName, targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName), pex);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while generating SQL diff file between '{0}\\{1}' and '{2}\\{3}'", sourceDbConnection.DatabaseServer, sourceDbConnection.DatabaseName, targetDbConnection.DatabaseServer, targetDbConnection.DatabaseName), ex);
            }        
        }

        private void SyncDatabaseFromDiffFile(DbConnectionProperties destDbConnection, string existingSqlDiffFile)
        {
            if (String.IsNullOrWhiteSpace(existingSqlDiffFile) || !(new FileInfo(existingSqlDiffFile)).Exists)
            {
                Logger.Error(String.Format("Unable to sync database '{0}\\{1}' from existing diff file '{2}'. File does not exist", destDbConnection.DatabaseServer, destDbConnection.DatabaseName, existingSqlDiffFile));
                return;
            }

            try
            {
                if (activeProcessor.SyncDatabaseFromExistingDiffFile(destDbConnection, existingSqlDiffFile))
                {
                    Logger.Debug(String.Format("Successfully synced database '{0}\\{1}' from existing diff file '{2}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName, existingSqlDiffFile));
                }
                else
                {
                    Logger.Debug(String.Format("Failed to sync database '{0}\\{1}' from existing diff file '{2}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName, existingSqlDiffFile));
                }
            }
            catch (ProcessorException pex)
            {
                Logger.Error(String.Format("Error occured while syncing target database '{0}\\{1}' from existing diff file '{2}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName, existingSqlDiffFile), pex);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while syncing target database '{0}\\{1}' from file '{2}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName, existingSqlDiffFile), ex);
            }
        }

        private void BackupDatabaseToScriptFolder(DbConnectionProperties destDbConnection)
        {
            Logger.Info(String.Format("Initializing database back to a script folder for database '{0}\\{1}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName));

            try
            {
                var scriptFolder = activeProcessor.BackupDbToScriptFolder(destDbConnection);
                Logger.Debug(String.Format("Database '{0}\\{1}' was successfully backed up to script folder '{2}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName, scriptFolder));
            }
            catch (ProcessorException pex)
            {
                Logger.Error(String.Format("Error occured while backing up database '{0}\\{1}' to script folder", destDbConnection.DatabaseServer, destDbConnection.DatabaseName), pex);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while backing up database '{0}\\{1}' to script folder", destDbConnection.DatabaseServer, destDbConnection.DatabaseName), ex);
            }
        }

        private void RestoreDatabaseFromScriptFolder(DbConnectionProperties destDbConnection, string customScriptFolder)
        {
            if (String.IsNullOrWhiteSpace(customScriptFolder))
            {
                customScriptFolder = GlobalConfig.GetLatestDbBackupScriptFolder();
            }

            try
            {
                if (activeProcessor.RestoreDbFromScriptFolder(destDbConnection, customScriptFolder))
                {
                    Logger.Debug(String.Format("Successfully restored database '{0}\\{1}' from script folder '{2}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName, customScriptFolder));
                }
                else
                {
                    Logger.Debug(String.Format("Failed to restore database '{0}\\{1}' from script folder '{2}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName, customScriptFolder));
                }
            }
            catch (ProcessorException pex)
            {
                Logger.Error(String.Format("Error occured while restoring db '{0}\\{1}' from script folder '{2}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName, customScriptFolder), pex);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while restoring db '{0}\\{1}' from script folder '{2}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName, customScriptFolder), ex);
            }
        }

        private void RollbackLatestDatabaseChanges(DbConnectionProperties destDbConnection, string customSnapshotPath)
        {
            if (String.IsNullOrWhiteSpace(customSnapshotPath))
            {
                customSnapshotPath = GlobalConfig.GetLatestSnapshotFile();
            }

            try
            {
                if (activeProcessor.RestoreDbSnapshot(destDbConnection, customSnapshotPath))
                {
                    Logger.Debug(String.Format("Successfully restored database '{0}\\{1}' from latest snapshot '{2}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName, customSnapshotPath));
                }
                else
                {
                    Logger.Debug(String.Format("Failed to restore database '{0}\\{1}' from latest snapshot '{2}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName, customSnapshotPath));
                }
            }
            catch (ProcessorException pex)
            {
                Logger.Error(String.Format("Error occured while restoring latest database snapshot for target database '{0}\\{1}' from file '{2}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName, customSnapshotPath), pex);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while restoring latest database snapshot for target database '{0}\\{1}' from file '{2}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName, customSnapshotPath), ex);
            }
        }

        private void SyncDatabases(DbConnectionProperties srcDbConnection, DbConnectionProperties destDbConnection, List<KeyValuePair<DiffFilterType, string>> includeOptions, List<KeyValuePair<DiffFilterType, string>> excludeOptions, bool scriptJiraItemsOnly)
        {
            // Process of syncing two databases
            try
            {
                // 1. Take snapshot of the target database 
                string snapshotFilePath = activeProcessor.SaveDbSnapshot(destDbConnection);

                if ((new FileInfo(snapshotFilePath)).Exists)
                {
                    // 2. Generate SQL diff script between 2 databases
                    string diffScriptFileName = activeProcessor.GenerateSqlDiffScriptBetweenTwoDatabases(srcDbConnection, destDbConnection, includeOptions, excludeOptions, scriptJiraItemsOnly);

                    if ((new FileInfo(diffScriptFileName)).Exists)
                    {
                        // 3. Sync target database with changes from source database
                        if (activeProcessor.SyncDatabases(srcDbConnection, destDbConnection, includeOptions, excludeOptions, scriptJiraItemsOnly))
                        {
                            Logger.Debug(String.Format("Successfull synced source database '{0}\\{1}' to '{2}\\{3}'", srcDbConnection.DatabaseServer, srcDbConnection.DatabaseName, destDbConnection.DatabaseServer, destDbConnection.DatabaseName));
                        }
                        else
                        {
                            Logger.Warn(String.Format("Failed to sync source database '{0}\\{1}' to '{2}\\{3}'", srcDbConnection.DatabaseServer, srcDbConnection.DatabaseName, destDbConnection.DatabaseServer, destDbConnection.DatabaseName));
                        }
                    }
                    else
                    {
                        Logger.Warn(String.Format("Failed to locate SQL diff file at '{0}'", diffScriptFileName));
                    }
                }
                else
                {
                    Logger.Warn(String.Format("Failed to locate snapshot file generated for database '{0}\\{1}', file '{2}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName, snapshotFilePath));
                }
            }
            catch (ProcessorException pex)
            {
                Logger.Error(String.Format("Error occured while syncing database change from '{0}\\{1}' to '{2}\\{3}'", srcDbConnection.DatabaseServer, srcDbConnection.DatabaseName, destDbConnection.DatabaseServer, destDbConnection.DatabaseName), pex);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while syncing database change from '{0}\\{1}' to '{2}\\{3}'", srcDbConnection.DatabaseServer, srcDbConnection.DatabaseName, destDbConnection.DatabaseServer, destDbConnection.DatabaseName), ex);
            }

        }

        private void TakeDatabaseSnapshot(DbConnectionProperties destDbConnection)
        {
            Logger.Info(String.Format("Intializing process to take snapshot of database '{0}\\{1}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName));

            try
            {
                string snapshotFile = activeProcessor.SaveDbSnapshot(destDbConnection);

                if (File.Exists(snapshotFile))
                {
                    Logger.Debug(String.Format("Snapshot was successfully taken of database '{0}\\{1}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName));
                }
                else
                {
                    Logger.Warn(String.Format("Failed to take snapshot of database '{0}\\{1}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while writing database snapshot for '{0}\\{1}'", destDbConnection.DatabaseServer, destDbConnection.DatabaseName), ex);
            }
        }
    }
}

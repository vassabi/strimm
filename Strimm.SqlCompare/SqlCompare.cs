using log4net;
using Strimm.SqlCompare.Compare;
using Strimm.SqlCompare.Enums;
using Strimm.SqlCompare.Model;
using Strimm.SqlCompare.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.SqlCompare
{
    public class SqlCompare
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SqlCompare));

        /**
         * Command line arguments and options for running this application:
         * 
         * 1. Take database snapshot: sqlcompare -action=snapshot -destdb=dev
         * 2. Generate database diff script between 2 databases: sqlcomare -action=diff -srcdb=dev -destdb=qa
         * 3. Backup database to script folder: sqlcompare -action=backup -destdb=dev
         * 4. Rollback from existing snapshot: sqlcompare -action=rollback -destdb=dev -snapshotfile=c:\..\mysnapshot.snp
         * 5. Generate database diff script between 2 databases based on latest jiras only: sqlcomare -action=diff -srcdb=dev -destdb=qa -jiraonly=true
         */
        [STAThread]
        static void Main(string[] args)
        {            
            try
            {
                log4net.Config.XmlConfigurator.Configure();

                Logger.Info("Starting db schema comparer application...");

                var runtimeParams = GetRunTimeParametes(args);

                var schemaCompare = new SchemaComparer();
                schemaCompare.Execute(runtimeParams); 
            }
            catch (ArgumentException aex)
            {
                Logger.Error("Error occured while running SQL Schema Compare", aex);
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while running SQL Schema Compare", ex);
            }
        }

        private static RunTimeParameters GetRunTimeParametes(string[] args)
        {
            Logger.Info("Reading command line parameters...");

            RunTimeParameters runtimeParams = new RunTimeParameters();

            if (args != null && args.Length > 0)
            {
                Logger.Info("//////////////////////////////////////////");
                Logger.Info("Command line parameters summary:");

                foreach (string arg in args) {
                    var argData = arg.Split('=');
                    var pair = argData != null && argData.Length == 2
                                ? new KeyValuePair<string, string>(argData[0],argData[1])
                                : new KeyValuePair<string, string>();

                    if (!String.IsNullOrEmpty(pair.Key))
                    {
                        switch (pair.Key)
                        {
                            case "-action":
                                runtimeParams.ActionToPerform = GetActionToPerform(pair);
                                Logger.Info(String.Format("Action: {0}", runtimeParams.ActionToPerform.ToString()));
                                break;
                            case "-srcdb":
                                runtimeParams.SourceDbConnection = GetDatabaseConnection(pair);
                                Logger.Info(String.Format("Source DB Server: {0}", runtimeParams.SourceDbConnection.DatabaseServer));
                                Logger.Info(String.Format("Source DB Name: {0}", runtimeParams.SourceDbConnection.DatabaseName));
                                Logger.Info(String.Format("Source DB User: {0}", runtimeParams.SourceDbConnection.UserName));
                                Logger.Info(String.Format("Source DB Password: {0}", runtimeParams.SourceDbConnection.Password));
                                break;
                            case "-destdb":
                                runtimeParams.TargetDbConnection = GetDatabaseConnection(pair);
                                Logger.Info(String.Format("Dest DB Server: {0}", runtimeParams.TargetDbConnection.DatabaseServer));
                                Logger.Info(String.Format("Dest DB Name: {0}", runtimeParams.TargetDbConnection.DatabaseName));
                                Logger.Info(String.Format("Dest DB User: {0}", runtimeParams.TargetDbConnection.UserName));
                                Logger.Info(String.Format("Dest DB Password: {0}", runtimeParams.TargetDbConnection.Password));
                                break;
                            case "-jiraonly":
                                runtimeParams.ScriptJiraItemsOnly = GetScriptJiraOnly(pair);
                                Logger.Info(String.Format("Jira file to apply: {0}", runtimeParams.ScriptJiraItemsOnly));
                                break;
                            case "-snapshotfile":
                                runtimeParams.CustomSnapshotFilePath = GetSnapshotFilePath(pair);
                                Logger.Info(String.Format("Custom snapshot file: {0}", runtimeParams.CustomSnapshotFilePath));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("No run-time command line arguments specified! Schema comparison aborted.");
            }

            Logger.Info("//////////////////////////////////////////");

            return runtimeParams;
        }

        private static string GetSnapshotFilePath(KeyValuePair<string, string> pair)
        {
            FileInfo snapshot = new FileInfo(pair.Value);
            if (snapshot.Exists)
            {
                if (snapshot.Extension != ".snp")
                {
                    throw new ArgumentException(String.Format("Invalid snapshot file specified '{0}'", pair.Value));
                }
            }
            else
            {
                throw new ArgumentException(String.Format("Snapshot file '{0}' can not be found.", pair.Value));
            }

            return snapshot.FullName;
        }

        private static bool GetScriptJiraOnly(KeyValuePair<string, string> pair)
        {
            bool scriptJiraOnly = false;
            Boolean.TryParse(pair.Value, out scriptJiraOnly);

            return scriptJiraOnly;
        }

        private static DbConnectionProperties GetDatabaseConnection(KeyValuePair<string, string> pair)
        {
            DbConnectionProperties connection;

            if (pair.Value == "qa")
            {
                connection = GlobalConfig.GetDbConnectionByType(DbType.Qa);
            }
            else if (pair.Value == "prod")
            {
                connection = GlobalConfig.GetDbConnectionByType(DbType.Prod);
            }
            else if (pair.Value == "test")
            {
                connection = GlobalConfig.GetDbConnectionByType(DbType.Test);
            }
            else
            {
                connection = GlobalConfig.GetDbConnectionByType(DbType.Dev);
            }

            return connection;
        }

        private static SqlAction GetActionToPerform(KeyValuePair<string, string> pair)
        {
            SqlAction action = SqlAction.SQL_TAKE_DB_SNAPSHOT;

            switch (pair.Value)
            {
                case "backup":
                    action = SqlAction.SQL_BACKUP_TO_SCRIPT_FOLDER;
                    break;
                case "restore":
                    action = SqlAction.SQL_RESTORE_FROM_SCRIPT_FOLDER;
                    break;
                case "rollback":
                    action = SqlAction.SQL_ROLLBACK_LATEST_CHANGES;
                    break;
                case "sync":
                    action = SqlAction.SQL_SYNC_SRC_TO_DEST_DB;
                    break;
                case "diff":
                    action = SqlAction.SQL_GEN_SQL_DIFF_SCRIPT;
                    break;
                default:
                    break;
            }

            return action;
        }
    }
}

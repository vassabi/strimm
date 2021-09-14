using log4net;
using Strimm.SqlCompare.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.SqlCompare.Settings
{
    public static class GlobalConfig
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GlobalConfig));

        private static readonly string DB_SNAPSHOT_PATH;
        private static readonly string DB_DIFF_SCRIPT_PATH;
        private static readonly string DB_BACKUP_PATH;
        private static readonly string DEV_DB_SERVER;
        private static readonly string DEV_DB_NAME;
        private static readonly string DEV_DB_USER;
        private static readonly string DEV_DB_PSWRD;
        private static readonly string QA_DB_SERVER;
        private static readonly string QA_DB_NAME;
        private static readonly string QA_DB_USER;
        private static readonly string QA_DB_PSWRD;
        private static readonly string PROD_DB_SERVER;
        private static readonly string PROD_DB_NAME;
        private static readonly string PROD_DB_USER;
        private static readonly string PROD_DB_PSWRD;
        private static readonly string JIRA_ITEMS_FILE_PATH;

        private static readonly string SNAPSHOT_FILE_EXT = ".snp";

        private static readonly DbConnectionProperties devConnectionProperties;
        private static readonly DbConnectionProperties qaConnectionProperties;
        private static readonly DbConnectionProperties prodConnectionProperties;

        static GlobalConfig()
        {
            try
            {
                DB_SNAPSHOT_PATH = ConfigurationManager.AppSettings["dbSnapshotPath"];
                DB_DIFF_SCRIPT_PATH = ConfigurationManager.AppSettings["dbDiffScriptsPath"];
                DB_BACKUP_PATH = ConfigurationManager.AppSettings["dbBackupPath"];
                DEV_DB_SERVER = ConfigurationManager.AppSettings["devDbServer"];
                DEV_DB_NAME = ConfigurationManager.AppSettings["devDbName"];
                DEV_DB_USER = ConfigurationManager.AppSettings["devDbUser"];
                DEV_DB_PSWRD = ConfigurationManager.AppSettings["devDbPassword"];
                QA_DB_SERVER = ConfigurationManager.AppSettings["qaDbServer"];
                QA_DB_NAME = ConfigurationManager.AppSettings["qaDbName"];
                QA_DB_USER = ConfigurationManager.AppSettings["qaDbUser"];
                QA_DB_PSWRD = ConfigurationManager.AppSettings["qaDbPassword"];
                PROD_DB_SERVER = ConfigurationManager.AppSettings["prodDbServer"];
                PROD_DB_NAME = ConfigurationManager.AppSettings["prodDbName"];
                PROD_DB_USER = ConfigurationManager.AppSettings["prodDbUser"];
                PROD_DB_PSWRD = ConfigurationManager.AppSettings["prodDbPassword"];
                JIRA_ITEMS_FILE_PATH = ConfigurationManager.AppSettings["jiraItemsFilePath"];

                devConnectionProperties = new DbConnectionProperties(DEV_DB_SERVER, DEV_DB_NAME, DEV_DB_USER, DEV_DB_PSWRD);
                qaConnectionProperties = new DbConnectionProperties(QA_DB_SERVER, QA_DB_NAME, QA_DB_USER, QA_DB_PSWRD);
                prodConnectionProperties = new DbConnectionProperties(PROD_DB_SERVER, PROD_DB_NAME, PROD_DB_USER, PROD_DB_PSWRD);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to read configuration properties", ex);
            }
        }

        #region Public Properties

        public static string ExecutingFolder
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }

        public static string SnapshotFolder
        {
            get
            {
                return DB_SNAPSHOT_PATH;
            }
        }

        public static string BackupFolder
        {
            get
            {
                return DB_BACKUP_PATH;
            }
        }

        public static string DiffScriptFolder
        {
            get
            {
                return DB_DIFF_SCRIPT_PATH;
            }
        }

        #endregion

        #region Public Methods

        public static DbConnectionProperties GetDbConnectionByType(DbType type)
        {
            return (type == DbType.Dev)
                        ? devConnectionProperties
                        : (type == DbType.Qa)
                            ? qaConnectionProperties
                            : prodConnectionProperties;
        }

        public static string GetDbSnapshotFileName(string dbName)
        {
            return DB_SNAPSHOT_PATH.IndexOf(':') > 0
                    ? Path.Combine(DB_SNAPSHOT_PATH, String.Format("{0}-{1}{2}", dbName, DateTime.Now.ToString("MM-dd-yyyy_HH-mm"), SNAPSHOT_FILE_EXT))
                    : Path.Combine(Path.Combine(ExecutingFolder, DB_SNAPSHOT_PATH), String.Format("{0}-{1}{2}", dbName, DateTime.Now.ToString("MM-dd-yyyy_HH-mm"), SNAPSHOT_FILE_EXT));
        }

        public static string GetLatestDbBackupScriptFolder()
        {
            DirectoryInfo dir = new DirectoryInfo(DB_BACKUP_PATH);
            DirectoryInfo backupDir = dir.GetDirectories().OrderByDescending(x => x.CreationTime).First();

            return backupDir.FullName;
        }

        public static string GetLatestSnapshotFile()
        {
            DirectoryInfo dir = new DirectoryInfo(DB_SNAPSHOT_PATH);
            FileInfo latestSnapshot = dir.GetFiles().OrderByDescending(x => x.CreationTime).First();

            return latestSnapshot != null
                        ? latestSnapshot.FullName
                        : String.Empty;
        }

        public static string CreateNewDbBackupScriptFolder(string dbName)
        {
            string folder = String.Format("{0}_Backup_{1}", dbName, DateTime.Now.Ticks);
            folder = Path.Combine(DB_BACKUP_PATH, folder);

            DirectoryInfo dir = new DirectoryInfo(folder);
            if (dir.Exists)
            {
                dir.Delete();
            }

            dir.Create();

            return dir.FullName;
        }

        public static string GetApplicableJiraItemsRegex()
        {
            StringBuilder sb = new StringBuilder();

            using (var stream = new StreamReader(File.OpenRead(JIRA_ITEMS_FILE_PATH)))
            {
                sb.Append("(");
                string line;
                bool isFirst = true;

                while ((line = stream.ReadLine()) != null) {
                    if (!isFirst)
                    {
                        sb.Append("|").Append(line.Trim());
                    }
                    else
                    {
                        sb.Append(line.Trim());
                        isFirst = false;
                    }
                }

                if (isFirst)
                {
                    sb.Clear();
                }
                else
                {
                    sb.Append(")w*");
                }

            }

            return sb.ToString();
        }

        public static string GenerateFilePathForDatabaseSqlDiffScript(string sourceDbName, string targetDbName)
        {
            return Path.Combine(DB_DIFF_SCRIPT_PATH, String.Format("db_diff_{0}_to_{1}-{2}.sql", sourceDbName, targetDbName, DateTime.Now.ToString("MM-dd-yyyy_HH-mm")));
        }

        #endregion
    }
}

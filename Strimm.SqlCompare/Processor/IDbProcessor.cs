using Strimm.SqlCompare.Enums;
using Strimm.SqlCompare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.SqlCompare.Processor
{
    public interface IDbProcessor
    {
        string SaveDbSnapshot(DbConnectionProperties targetDbConnection);

        bool RestoreDbSnapshot(DbConnectionProperties targetDbConnection, string snapshotFilePath);

        string BackupDbToScriptFolder(DbConnectionProperties targetDbConnection);

        bool RestoreDbFromScriptFolder(DbConnectionProperties targetDbConnection, string scriptFolder);

        string GenerateSqlDiffScriptBetweenTwoDatabases(DbConnectionProperties sourceDbConnection, DbConnectionProperties targetDbConnection, List<KeyValuePair<DiffFilterType, string>> includeOptions, List<KeyValuePair<DiffFilterType, string>> excludeOptions, bool scriptJiraItemsOnly);

        bool SyncDatabases(DbConnectionProperties sourceDbConnection, DbConnectionProperties targetDbConnection, List<KeyValuePair<DiffFilterType, string>> includeOptions, List<KeyValuePair<DiffFilterType, string>> excludeOptions, bool scriptJiraItemsOnly);

        bool SyncDatabaseFromExistingDiffFile(DbConnectionProperties destDbConnection, string existingSqlDiffFile);
    }
}

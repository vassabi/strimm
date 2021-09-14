using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.SqlCompare.Enums
{
    public enum SqlAction
    {
        SQL_SYNC_SRC_TO_DEST_DB,
        SQL_SYNC_FROM_DIFF_SCRIPT,
        SQL_ROLLBACK_LATEST_CHANGES,
        SQL_RESTORE_FROM_SCRIPT_FOLDER,
        SQL_TAKE_DB_SNAPSHOT,
        SQL_BACKUP_TO_SCRIPT_FOLDER,
        SQL_GEN_SQL_DIFF_SCRIPT
    }
}

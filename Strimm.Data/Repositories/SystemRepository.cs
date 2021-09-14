using Strimm.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strimm.Model;
using Dapper;
using System.Data;
using System.Diagnostics.Contracts;
using log4net;
using Strimm.Model.Projections;
using Dapper.Contrib.Extensions;

namespace Strimm.Data.Repositories
{
    public class SystemRepository : RepositoryBase, ISystemRepository {

        private static readonly ILog Logger = LogManager.GetLogger(typeof(SystemRepository));

        public SystemRepository()
            : base()
        {

        }

        public bool RebuildAllIndexesOnDatabase()
        {
            bool isSuccess = false;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                this.StrimmDbConnection.Query("strimm.RebuildAllDatabaseIndexes", null, null, false, 30, commandType: CommandType.StoredProcedure);
                isSuccess = true;
            }

            return isSuccess;
        }
    }
}

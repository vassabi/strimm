using Dapper;
using log4net;
using Strimm.Data.Interfaces;
using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;


namespace Strimm.Data.Repositories
{
    public class VideoProviderRepository : RepositoryBase, IVideoProviderRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VideoProviderRepository));

        public VideoProviderRepository()
            : base()
        {

        }

        public List<VideoProvider> GetAllVideoProviders()
        {
            var providers = new List<VideoProvider>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    providers = this.StrimmDbConnection.Query<VideoProvider>("strimm.GetAllVideoProviders", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve all video providers", ex);
            }

            return providers;
        }

        public List<VideoProvider> GetActiveVideoProviders()
        {
            var providers = new List<VideoProvider>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    providers = this.StrimmDbConnection.Query<VideoProvider>("strimm.GetActiveVideoProviders", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve all active video providers", ex);
            }

            return providers;
        }
    }
}

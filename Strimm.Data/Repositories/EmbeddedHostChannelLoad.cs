using Dapper;
using log4net;
using Strimm.Data.Interfaces;
using Strimm.Model;
using Strimm.Model.Projections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Repositories
{
    public class EmbeddedHostChannelLoad : RepositoryBase, IEmbeddedHostChannelLoadRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ChannelTubeRepository));
        public EmbeddedHostChannelLoad()
            : base()
        {

        }

        public bool InsertEmbeddedHostChannelLoad(int channelTubeId, DateTime clientTime, string embeddedHostUrl,string accountNumber, bool isSingleChannelView, bool IsSubscribedDomain)
        {
            Contract.Requires(channelTubeId > 0, "channelTubeId cannot be null");
            

          
          bool isSuccess = false;

          try
          {
              if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
              {
                  this.StrimmDbConnection.Execute("strimm.InsertEmbeddedHostChannelLoad", new
                  {
                      ChannelTubeId = channelTubeId,
                      EmbeddedHostUrl = embeddedHostUrl,
                      AccountNumber = accountNumber,
                      IsSingleChannelView = isSingleChannelView,
                      IsSubscribedDomain = IsSubscribedDomain,
                      LoadDate = clientTime                         
                  }, null, 30, commandType: CommandType.StoredProcedure);
                  isSuccess = true;
              }
          }
          catch (Exception ex)
          {
              Logger.Error(String.Format("Failed to insertEmbeddedHostChannelLoad '{0}'", channelTubeId), ex);
          }

          return isSuccess;
           
        }

        public int InsertEmbeddedHostChannelLoadWithGet(int channelTubeId, DateTime clientTime, string embeddedHostUrl, string accountNumber, bool isSingleChannelView, bool IsSubscribedDomain)
        {
            Contract.Requires(channelTubeId > 0, "channelTubeId cannot be null");



            int embeddedHostChannelLoadId = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<int>("strimm.InsertEmbeddedHostChannelLoadWithGet", new
                    {
                        ChannelTubeId = channelTubeId,
                        EmbeddedHostUrl = embeddedHostUrl,
                        AccountNumber = accountNumber,
                        IsSingleChannelView = isSingleChannelView,
                        IsSubscribedDomain = IsSubscribedDomain,
                        LoadDate = clientTime
                    }, null, false, 30, commandType: CommandType.StoredProcedure);
                    embeddedHostChannelLoadId = results.FirstOrDefault();
                   
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insertEmbeddedHostChannelLoadwithget '{0}'", channelTubeId), ex);
            }

            return embeddedHostChannelLoadId;

        }

        public List<EmbeddedChannelPo> GetAllEmbeddedChannels()
        {
            List<EmbeddedChannelPo> embeddedChannelList = new List<EmbeddedChannelPo>();
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    embeddedChannelList = this.StrimmDbConnection.Query<EmbeddedChannelPo>("strimm.GetAllEmbeddedChannels", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve all embedded channels", ex);
            }

            return embeddedChannelList;

        }

        public List<EmbeddedChannelPo> GetEmbeddedChannelsByDate(DateTime clientTime)
        {
            List<EmbeddedChannelPo> channels = new List<EmbeddedChannelPo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channels = this.StrimmDbConnection.Query<EmbeddedChannelPo>("strimm.GetEmbeddedChannelsByDate", new { clientTime = clientTime }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve channel statistics for admin panel client time ={0}", clientTime), ex);
            }

            return channels;
        }

        public bool UpdateEmbeddedHostChannelLoadById(int embeddedChannelHostLoadId, double visitTime, DateTime loadEndTime)
        {
            bool isSuccess = false;
          

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.UpdateEmbeddedHostChannelLoadById", new
                    {
                        EmbeddedChannelHostLoadId = embeddedChannelHostLoadId,
                        VisitTime = visitTime,
                        LoadEndTime = loadEndTime
                       
                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to UpdateEmbeddedChannelHostLoad '{0}'", embeddedChannelHostLoadId), ex);
            }

            return isSuccess;

          
        }

        public List<EmbeddedChannelPo> GetListOfEmbededChannelsByChannelId(int channelTubeId)
        {
            List<EmbeddedChannelPo> channels = new List<EmbeddedChannelPo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channels = this.StrimmDbConnection.Query<EmbeddedChannelPo>("strimm.GetEmbeddedChannelByChannelTubeId", new { ChannelTubeId = channelTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to GetListOfEmbededChannelsByUserId  where channelTubeId ={0}", channelTubeId), ex);
            }

            return channels;
        }
    }
}

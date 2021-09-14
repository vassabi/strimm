using log4net;
using Strimm.Data.Interfaces;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Strimm.Data.Repositories
{
    public class ChannelSubscriptionRepository : RepositoryBase, IChannelSubscriptionRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ChannelSubscriptionRepository));

        public ChannelSubscriptionRepository()
            :base()
        {

        }

        public List<ChannelSubscription> GetAllChannelSubscriptionsByChannelTubeId(int channelTubeId)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");

            List<ChannelSubscription> subscriptions = new List<ChannelSubscription>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    subscriptions = this.StrimmDbConnection.Query<ChannelSubscription>("strimm.GetAllChannelSubscriptionsByChannelTubeId", new { ChannelTubeId = channelTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving channel subscription by channel tube Id={0}", channelTubeId), ex);
            }

            return subscriptions;
        }

        public ChannelSubscription GetChannelSubscriptionByChannelTubeIdAndUserId(int channelTubeId, int userId)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            ChannelSubscription subscription = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ChannelSubscription>("strimm.GetChannelSubscriptionByChannelTubeIdAndUserId", new { ChannelTubeId = channelTubeId, UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    subscription = results.Count == 1 ? results.First() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving channel subscription by channel tube Id={0} and User id = {1}", channelTubeId, userId), ex);
            }

            return subscription;
        }

        public bool InsertChannelSubscription(ChannelSubscription subscription)
        {
            Contract.Requires(subscription != null, "ChannelSubscription cannot be null");
            Contract.Requires(subscription.ChannelSubscriptionId == 0, "ChannelSubscriptionId should be equal to 0");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.InsertChannelSchedule", subscription, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert channel subscription for channel Tube Id={0} and user id '{1}'", subscription.ChannelTubeId, subscription.UserId), ex);
            }

            return isSuccess;
        }

        public bool DeleteChannelSubscriptionByChannelTubeIdAndUserId(int channelTubeId, int userId)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.userId", new { ChannelTubeId = channelTubeId, UserId = userId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete channel schedule with channel tube Id={0} and user Id = {1}", channelTubeId, userId), ex);
            }

            return isSuccess;
        }

        public bool DeleteChannelSubscriptionById(int channelSubscriptionId)
        {
            Contract.Requires(channelSubscriptionId > 0, "ChannelSubscriptionId should be greater then 0");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.DeleteChannelSubscriptionById", new { ChannelSubscriptionId = channelSubscriptionId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete channel schedule with Id={0}", channelSubscriptionId), ex);
            }

            return isSuccess;
        }
    }
}

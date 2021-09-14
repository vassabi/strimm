using Dapper;
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

namespace Strimm.Data.Repositories
{
   public class ChannelLikeRepository:RepositoryBase, IChannelLikeRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ChannelLikeRepository));

        public ChannelLikeRepository()
            : base()
        {

        }

        public int InsertChannelLike(ChannelLike like)
        {
            Contract.Requires(like != null, "Channellike cannot be null");
            Contract.Requires(like.ChannelLikeId == 0, "ChannelLikeId should be equal to 0");
            int likeCount = 0;
           // bool isSuccess = false;
          
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ChannelLike>("strimm.InsertChannelLikeWithGet", new
                    {
                        ChannelTubeId = like.ChannelTubeId,
                        UserId = like.UserId,
                        LikeStartDate = like.LikeStartDate,
                        IsLike = true
                    }, null, false, 30, commandType: CommandType.StoredProcedure);
                   // int rowcount = this.StrimmDbConnection.Execute("strimm.InsertChannelLikeWithGet", new {  }, null, 30, commandType: CommandType.StoredProcedure);
                    //isSuccess = rowcount > 0;
                    if(results!=null)
                    {
                        likeCount = results.Count();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert channel like for channel Tube Id={0} and user id '{1}'", like.ChannelTubeId, like.UserId), ex);
            }

            return likeCount;
        }

        public int DeleteChannelLikeByChannelTubeIdAndUserId(int channelTubeId, int userId, DateTime likeEndDate)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            int likeCount = 0;
            try
            {
                var results = this.StrimmDbConnection.Query<ChannelLike>("strimm.DeleteChannelLikeByChannelTubeIdAndUserIdWithGet", new
                {
                    ChannelTubeId =channelTubeId,
                    UserId = userId,
                    LikeEndDate =likeEndDate                   
                }, null, false, 30, commandType: CommandType.StoredProcedure);
                if (results != null)
                {
                    likeCount = results.Count();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete channel like with channel tube Id={0} and user Id = {1}", channelTubeId, userId), ex);
            }

            return likeCount;
        }

        public ChannelLike GetChannelLikeByChannelTubeIdAndUserId(int channelTubeId, int userId)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            ChannelLike like = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ChannelLike>("strimm.GetChannelLikeByChannelTubeIdAndUserId", new { ChannelTubeId = channelTubeId, UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    like = results.Count == 1 ? results.First() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving channel like by channel tube Id={0} and User id = {1}", channelTubeId, userId), ex);
            }

            return like;
        }
    }
}

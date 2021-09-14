using Strimm.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Diagnostics.Contracts;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;

namespace Strimm.Data.Repositories
{
    public class BoardRepository : RepositoryBase, IBoardRepository
    {
        public BoardRepository()
            : base()
        {

        }

        public string GetUserBoardNameByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            string name = String.Empty;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                name = this.StrimmDbConnection.ExecuteScalar<String>("strimm.GetUserBoardNameByUserId", new { UserId = userId }, null, 30, commandType: CommandType.StoredProcedure);
            }

            return name;
        }

        public long GetNumberOfBoardVisitorsByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            int count = 0;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                count = this.StrimmDbConnection.ExecuteScalar<int>("strimm.GetNumberOfBoardVisitorsByUserId", new { UserId = userId }, null, 30, commandType: CommandType.StoredProcedure);
            }

            return count;
        }

        public UserBoard GetUserBoardDataByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            UserBoard userBoard = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                using(var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetUserBoardDataByUserId", new { UserId = userId }, null, null, commandType: CommandType.StoredProcedure))
                {
                    userBoard = multi.Read<UserBoard>().Single();
                    userBoard.MyChannels = multi.Read<ChannelTubeModel>().ToList();
                    userBoard.FeaturedChannels = multi.Read<ChannelTubePo>().ToList();
                    userBoard.SubscribedChannels = multi.Read<ChannelTubePo>().ToList();
                }
            }

            return userBoard;
        }

        public UserBoard GetUserBoardDataByUserName(String userName, DateTime clientTime)
        {
            Contract.Requires(!String.IsNullOrEmpty(userName), "UserName was not specified");
            Contract.Requires(clientTime != null, "Client time is required");

            UserBoard userBoard = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetUserBoardDataByUserName", new { UserName = userName, ClientTime = clientTime }, null, null, commandType: CommandType.StoredProcedure))
                {
                    userBoard = multi.Read<UserBoard>().SingleOrDefault();

                    if (userBoard != null)
                    {
                        var myChannels = multi.Read<ChannelTubeModel>();
                        //var featuredChannels = multi.Read<ChannelTubePo>();
                        //var subscribedChannels = multi.Read<ChannelTubePo>();

                        userBoard.MyChannels = myChannels != null ? myChannels.ToList() : new List<ChannelTubeModel>();
                        //userBoard.FeaturedChannels = featuredChannels != null ? featuredChannels.ToList() : new List<ChannelTubePo>();
                        //userBoard.SubscribedChannels = subscribedChannels != null ? subscribedChannels.ToList() : new List<ChannelTubePo>();
                    }
                }
            }

            return userBoard;
        }

        public UserBoard GetUserBoardDataByPublicUrl(String publicUrl, DateTime clientTime)
        {
            Contract.Requires(!String.IsNullOrEmpty(publicUrl), "UserName was not specified");
            Contract.Requires(clientTime != null, "Client time is required");

            UserBoard userBoard = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetUserBoardDataByPublicUrl", new { PublicUrl = publicUrl, ClientTime = clientTime }, null, null, commandType: CommandType.StoredProcedure))
                {
                    userBoard = multi.Read<UserBoard>().SingleOrDefault();

                    if (userBoard != null)
                    {
                        var myChannels = multi.Read<ChannelTubeModel>();
                        //var featuredChannels = multi.Read<ChannelTubePo>();
                        //var subscribedChannels = multi.Read<ChannelTubePo>();

                        userBoard.MyChannels = myChannels != null ? myChannels.ToList() : new List<ChannelTubeModel>();
                        //userBoard.FeaturedChannels = featuredChannels != null ? featuredChannels.ToList() : new List<ChannelTubePo>();
                        //userBoard.SubscribedChannels = subscribedChannels != null ? subscribedChannels.ToList() : new List<ChannelTubePo>();
                    }
                }
            }

            return userBoard;
        }
    }
}

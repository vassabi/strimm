using Dapper;
using log4net;
using Strimm.Data.Interfaces;
using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using Strimm.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Strimm.Data.Repositories
{
    public class ChannelTubeRepository : RepositoryBase, IChannelTubeRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ChannelTubeRepository));

        public ChannelTubeRepository()
            : base()
        {

        }

        public List<ChannelTube> GetAllChannelTubes()
        {
            var channelTubes = new List<ChannelTube>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channelTubes = this.StrimmDbConnection.Query<ChannelTube>("strimm.GetAllChannelTubes", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve all channel tubes", ex);
            }

            return channelTubes;
        }

        public List<ChannelTube> GetAllFeaturedChannelTubes()
        {
            var channelTubes = new List<ChannelTube>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channelTubes = this.StrimmDbConnection.Query<ChannelTube>("strimm.GetAllFeaturedChannelTubes", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve all featured channel tubes", ex);
            }

            return channelTubes;
        }

        public List<ChannelTube> GetChannelTubeByUserIdForAdmin(int userId)
        {
            //[GetChannelTubeByUserIdForAdmin]
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            var channelTubes = new List<ChannelTube>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channelTubes = this.StrimmDbConnection.Query<ChannelTube>("strimm.GetChannelTubeByUserIdForAdmin", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve all channel tube for user Id = {0}", userId), ex);
            }

            return channelTubes;
        }

        public List<ChannelTubePo> GetChannelTubePosByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            var channelTubes = new List<ChannelTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channelTubes = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetChannelTubePosByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve all channel tube for user Id = {0}", userId), ex);
            }

            return channelTubes;
        }

        public List<ChannelTube> GetChannelTubesByTitleKeywords(List<string> keywords)
        {
            Contract.Requires((keywords != null || keywords.Count == 0), "Invalid or empty keywords list");
            
            var channelTubes = new List<ChannelTube>();
            var keywordsBuffer = new StringBuilder();

            keywords.ForEach(x => 
                {
                    keywordsBuffer.Append(x).Append(',');
                });

            string searchCriteria = keywordsBuffer.ToString().TrimEnd(',');

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channelTubes = this.StrimmDbConnection.Query<ChannelTube>("strimm.GetChannelTubesByTitleKeywords", new { Keywords = searchCriteria }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve channels using the following keywords: {0}", searchCriteria), ex);
            }

            return channelTubes;
        }

        public ChannelTube GetChannelTubeById(int channelTubeId)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");

            ChannelTube channelTube = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ChannelTube>("strimm.GetChannelTubeById", new { ChannelTubeId = channelTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    channelTube = results.Count == 1 ? results.First() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve channel tube with Id = {0}", channelTubeId), ex);
            }

            return channelTube;
        }

        public ChannelTube GetChannelTubeByName(string url)
        {
            Contract.Requires(!String.IsNullOrEmpty(url), "Invalid or missing channel tube name");

            ChannelTube channelTube = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ChannelTube>("strimm.GetChannelTubeByName", new { Name = url }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    channelTube = results.Count == 1 ? results.First() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve channel tube with Name = {0}", url), ex);
            }

            return channelTube;
        }

        public ChannelTube GetChannelTubeByUrl(string url)
        {
            Contract.Requires(!String.IsNullOrEmpty(url), "Invalid or missing channel tube URL");

            ChannelTube channelTube = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ChannelTube>("strimm.GetChannelTubeByUrl", new { Url = url }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    channelTube = results.Count == 1 ? results.First() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve channel tube with URL = {0}", url), ex);
            }

            return channelTube;
        }

        public void UpsertChannelRokuSettings(ChannelTubeRokuSettings settings)
        {
            string updquery = "Update strimm.ChannelTubeRokuSettings set AddedToRoku=" + (settings.AddedToRoku ? 1 : 0) + ", LastUpdateDate='" + settings.LastUpdateDate + "' where ChannelTubeId=" + settings.ChannelTubeId;
            string insquery = "Insert Into strimm.ChannelTubeRokuSettings (AddedToRoku, LastUpdateDate, ChannelTubeId) Values(" + (settings.AddedToRoku ? 1 : 0) + ", '"+ settings.LastUpdateDate + "', " + settings.ChannelTubeId +")";

            var exsettings = GetChannelRokuSettings(settings.ChannelTubeId);

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var command = this.StrimmDbConnection.CreateCommand();
                command.CommandText = exsettings == null ? insquery : updquery;
                command.ExecuteNonQuery();
            }
        }

        public ChannelTubeRokuSettings GetChannelRokuSettings(int ChannelTubeId)
        {
            string query = "Select * from strimm.ChannelTubeRokuSettings where ChannelTubeId = " + ChannelTubeId;
            var settings = new ChannelTubeRokuSettings();
            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                settings = this.StrimmDbConnection.Query<ChannelTubeRokuSettings>(query).FirstOrDefault();
            }
            return settings;
        }

        public ChannelTube GetChannelTubeByChannelScheduleId(int channelScheduleId)
        {
            Contract.Requires(channelScheduleId > 0, "Invalid or missing channel schedule id");

            ChannelTube channelTube = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ChannelTube>("strimm.GetChannelTubeByChannelScheduleId", new { ChannelScheduleId = channelScheduleId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    channelTube = results.Count == 1 ? results.First() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve channel tube using channel schedule id = {0}", channelScheduleId), ex);
            }

            return channelTube;
        }

        public List<ChannelTube> GetChannelTubesSubscribedByUserByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            List<ChannelTube> channelTubes = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channelTubes = this.StrimmDbConnection.Query<ChannelTube>("strimm.GetChannelTubesSubscribedByUserByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve channel tube subscribed to by user using user id = {0}", userId), ex);
            }

            return channelTubes;
        }

        public List<ChannelTube> GetChannelTubesByCategoryId(int categoryId)
        {
            Contract.Requires(categoryId > 0, "CategoryId should be greater then 0");

            List<ChannelTube> channelTubes = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channelTubes = this.StrimmDbConnection.Query<ChannelTube>("strimm.GetChannelTubesByCategoryId", new { CategoryId = categoryId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve channel tube for category with id = {0}", categoryId), ex);
            }

            return channelTubes;
        }

        public List<ChannelTube> GetHighlySubscribedChannelTubes(int rowCount)
        {
            throw new NotImplementedException();
        }

        public bool InsertChannelTube(int categoryId, string name, string description, string pictureUrl, string url, int userId)
        {
            Contract.Requires(categoryId > 0, "Invalid category id specified");
            Contract.Requires(userId > 0, "Specified user id is invalid");
            Contract.Requires(!String.IsNullOrEmpty(name), "ChannelTube name cannot be empty");

            bool isSuccess = false;                      

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.InsertChannelTube", new { CategoryId=categoryId, Name=name, Description=description, PictureUrl=pictureUrl,
                                            Url = url, UserId = userId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert channel tube named '{0}'", name), ex);
            }

            return isSuccess;
        }

        public ChannelTube InsertChannelTubeWithGet(int categoryId, int languageId, string name, string description, string pictureUrl, string url, int userId, bool isWhiteLabeled, string channelPassword, bool embedEnabled, bool muteOnStartup, string customLabel, string subscriberDomain, bool embedOnlyMode, bool matureContentEnabled, bool showPlayerControls, bool isPrivate, bool isLogoModeActive, string channelLogoUrl, bool playLiveFirst, bool keepGuideOpen=false)
          
        {
            Contract.Requires(categoryId > 0, "Invalid category id specified");
            Contract.Requires(userId > 0, "Specified user id is invalid");
            Contract.Requires(!String.IsNullOrEmpty(name), "ChannelTube name cannot be empty");

            ChannelTube channelTube = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ChannelTube>("strimm.InsertChannelTubeWithGet", new
                    {
                       CategoryId =categoryId,
	                   LanguageId =languageId,
	                   Name=name,
	                   Description=description, 
	                   PictureUrl =pictureUrl,
	                   Url =url,
	                   UserId =userId,
	                   IsWhiteLabeled =isWhiteLabeled,
	                   ChannelPassword =channelPassword,
	                   EmbedEnabled = embedEnabled,
	                   MuteOnStartup=muteOnStartup,
	                   CustomLabel =customLabel,
                       EmbedOnlyModeEnabled = embedOnlyMode,
                       MatureContentEnabled = matureContentEnabled,
                       CustomPlayerControlsEnabled = showPlayerControls,
                       IsPrivate=isPrivate,
                       IsLogoModeActive = isLogoModeActive,
                       CustomLogo = channelLogoUrl,
                       PlayLiveFirst=playLiveFirst,
                       KeepGuideOpen=keepGuideOpen
                       

	                                      

                    }, null, false, 30, commandType: CommandType.StoredProcedure);
                    channelTube = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert channel tube named '{0}'", name), ex);
            }

            return channelTube;
        }

        public bool InsertSubscriberDomain (int userId, int channelTubeId, string domainName)
        {
            Contract.Requires(channelTubeId > 0, "Specified channelTube id is invalid");
            Contract.Requires(userId > 0, "Specified user id is invalid");
          

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.InsertSubscribtionDomain", new
                    {
                         UserId =userId,
	                    UserDomain = domainName,
	                    ChannelTubeId =channelTubeId
                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert subscriberDomain tube named '{0}'", domainName), ex);
            }

            return isSuccess;
            
        }

        public bool UpdateSubscribtionDomain (int userId, int channelTubeId, string userDomain)
        {
            Contract.Requires(channelTubeId > 0, "Specified channelTube id is invalid");
            Contract.Requires(userId > 0, "Specified user id is invalid");


            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.UpdateSubscribtionDomain", new
                    {
                    
                        UserId = userId,
                        ChannelTubeId = channelTubeId,
                        UserDomain = userDomain
                        
                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert subscriberDomain tube named '{0}'", userDomain), ex);
            }

            return isSuccess;
        }
        public bool InsertChannnelPassword (int userId, int channelTubeId, string channelPassword)
        {
            Contract.Requires(channelTubeId > 0, "Specified channelTube id is invalid");
            Contract.Requires(userId > 0, "Specified user id is invalid");


            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.InsertChannnelPassword", new
                    {
                        UserId =userId,
	                    ChannelPassword =channelPassword,
	                    ChannelTubeId = channelTubeId
                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert password for channel tube named '{0}' and channelId '{1}'", channelPassword, channelTubeId), ex);
            }

            return isSuccess;

        }

        public bool UpdateChannelTube(ChannelTube channelTube)
        {
            Contract.Requires(channelTube != null, "ChannelTube cannot be null");
            Contract.Requires(channelTube.ChannelTubeId > 0, "ChannelTubeId should be be greater then 0");
            Contract.Requires(!String.IsNullOrEmpty(channelTube.Name), "ChannelTube name cannot be empty");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.UpdateChannelTube", new { 
                    	ChannelTubeId  = channelTube.ChannelTubeId,
	                    CategoryId = channelTube.CategoryId,
	                    Name = channelTube.Name,
	                    Description = channelTube.Description,
	                    PictureUrl = channelTube.PictureUrl,
	                    Url = channelTube.Url,	
	                    IsFeatured = channelTube.IsFeatured,
	                    IsPrivate = channelTube.IsPrivate,
	                    IsLocked = channelTube.IsLocked,
	                    UserId = channelTube.UserId,
	                    IsDeleted = channelTube.IsDeleted,
	                    IsAutoPilotOn = channelTube.IsAutoPilotOn,
	                    LanguageId = channelTube.LanguageId > 0 ? channelTube.LanguageId : 1,
	                    IsWhiteLabeled = channelTube.IsWhiteLabeled,
	                    ChannelPassword = channelTube.ChannelPassword,
	                    EmbedEnabled = channelTube.EmbedEnabled,
	                    MuteOnStartup = channelTube.MuteOnStartup,
                        CustomLabel = channelTube.CustomLabel,
                        EmbedOnlyModeEnabled = channelTube.EmbedOnlyModeEnabled,
	                    MatureContentEnabled= channelTube.MatureContentEnabled,
	                    CustomPlayerControlsEnabled = channelTube.CustomPlayerControlsEnabled,
                        IsLogoModeActive = channelTube.IsLogoModeActive,
	                    CustomLogo = channelTube.CustomLogo,
                        PlayLiveFirst=channelTube.PlayLiveFirst,
                        KeepGuideOpen=channelTube.KeepGuideOpen

                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update channel tube named '{0}', id = {1}", channelTube.Name, channelTube.ChannelTubeId), ex);
            }

            return isSuccess;
        }

        public bool InsertChannelSubscriptionByUserIdAndChannelTubeId(int userId, int channelTubeId, DateTime subscriptionStartDate)
        {
            Logger.Info(String.Format("Inserting new channel subscription for user with Id={0} and channel Tube Id={1} starting on '{2}'", userId, channelTubeId, subscriptionStartDate));

            Contract.Requires(userId > null, "UserId is not valid");
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");
            Contract.Requires(subscriptionStartDate > DateTime.MinValue && subscriptionStartDate < DateTime.MaxValue, "Invalid subscription start date specified");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.InsertChannelSubscriptionByUserIdAndChannelTubeId", new { UserId = userId, ChannelTubeId = channelTubeId, SubscriptionStartDate = subscriptionStartDate }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert new channel subscription for user with Id={0} and channel Tube Id={1} starting on '{2}'", userId, channelTubeId, subscriptionStartDate), ex);
            }

            return isSuccess;
        }

        public bool DeleteChannelSubscriptionByUserIdAndChannelTubeId(int userId, int channelTubeId, DateTime clientTime)
        {
            Logger.Info(String.Format("Deleting channel subscription for user with Id={0} to channel Tube with Id={1} as of {2}", userId, channelTubeId, clientTime.ToString()));

            Contract.Requires(userId > 0, "UserId is not valid");
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.DeleteChannelSubscriptionByChannelTubeIdAndUserId", new { UserId = userId, ChannelTubeId = channelTubeId, ClientTime = clientTime }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete channel subscription for user with Id={0} to channel Tube with Id={1} as of {2}", userId, channelTubeId, clientTime.ToString()), ex);
            }

            return isSuccess;
        }

        public bool DeleteSubscriberDomainByUserIdAndChannelTubeId(int userId, int channelTubeId, DateTime clientTime)
        {
            Logger.Info(String.Format("Deleting subscriber domain for user with Id={0} to channel Tube with Id={1} as of {2}", userId, channelTubeId, clientTime.ToString()));

            Contract.Requires(userId > 0, "UserId is not valid");
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.DeleteSubscriberDomainByUserIdAndChannelId", new { UserId = userId, ChannelTubeId = channelTubeId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete subscription domain for user with Id={0} to channel Tube with Id={1} as of {2}", userId, channelTubeId, clientTime.ToString()), ex);
            }

            return isSuccess;
        }
        public bool DeleteChannelTubeById(int channelTubeId)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.DeleteChannelTubeById", new { ChannelTubeId = channelTubeId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete channel tube with Id={0}", channelTubeId), ex);
            }

            return isSuccess;
        }

        public bool DeleteChannelSubscriptionById(int channelSubscriptionId)
        {
            Logger.Info(String.Format("Deleting channel subscription by Id={1}", channelSubscriptionId));

            Contract.Requires(channelSubscriptionId > null, "ChannelSubscriptionId is not valid");

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
                Logger.Error(String.Format("Failed to delete channel subscription by Id={0}", channelSubscriptionId), ex);
            }

            return isSuccess;
        }

        public ChannelTubePo GetChannelTubePoById(int channelTubeId)
        {
            Logger.Info(String.Format("Retriving channelTubePo for channel with Id={0}", channelTubeId));

            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");

            ChannelTubePo channel = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetChannelTubePoById", new { ChannelTubeId = channelTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    channel = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve ChannelTubePo by Id={0}", channelTubeId), ex);
            }

            return channel;
        }

        public List<ChannelTubePo> GetChannelTubePoByKeywords(List<string> keywords, DateTime clientTime)
        {
            Logger.Info("Retrieving channel tube Pos by keywords");

            Contract.Requires(keywords != null && keywords.Count > 0, "No keywords specified to perform the search");

            if (keywords == null || keywords.Count == 0)
            {
                throw new ArgumentException("No keywords specified to perform the search");
            }

            var channels = new List<ChannelTubePo>();
            var builder = new StringBuilder();

            keywords.ForEach(x =>
            {
                builder.Append(x).Append(",");
            });

            string keywordsString = builder.ToString().TrimEnd(',');

            try
            {

                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetChannelTubePoByKeywords", new { Keywords = keywordsString, ClientTime = clientTime }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve ChannelTubePo by keywords='{0}'", keywordsString), ex);
            }

            return channels;
        }


        public List<ChannelTubePo> GetAllChannelTubePosOnAutoPilot()
        {
            List<ChannelTubePo> channels = new List<ChannelTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetAllChannelTubePosOnAutoPilot", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve ChannelTubePo on AutoPilot", ex);
            }

            return channels;
        }

        public List<ChannelTubePo> GetCurrentlyPlayingTopChannels(DateTime clientTime)
        {
            List<ChannelTubePo> channels = new List<ChannelTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetCurrentlyPlayingTopChannels", new { ClientTime = clientTime }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve ChannelTubePo that are currently playing", ex);
            }

            return channels;
        }

        public List<ChannelTubePo> GetCurrentlyPlayingFavoriteChannelsForUserByUserId(int userId, DateTime clientTime)
        {
            List<ChannelTubePo> channels = new List<ChannelTubePo>();
           
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetCurrentlyPlayingFavoriteChannelsForUserByUserId", new { UserId = userId, ClientTime = clientTime }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve favorite ChannelTubePo that are currently playing for user with Id={0}", userId), ex);
            }

            return channels;
        }

        public List<ChannelTubePo> GetCurrentlyPlayingChannels(DateTime clientTime)
        {
            List<ChannelTubePo> channels = new List<ChannelTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    //channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetCurrentlyPlayingChannels", new { ClientTime = clientTime }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetCurrentlyPlaying_BASE", new { ClientTime = clientTime }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve favorite ChannelTubePo that are currently playing for client time ={0}", clientTime), ex);
            }

            return channels;
        }

        public List<ChannelTubePo> GetCurrentlyPlayingChannelsByUserId(DateTime clientTime, int userId)
        {
            List<ChannelTubePo> channels = new List<ChannelTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    //channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetCurrentlyPlayingChannelsByUserId", new { ClientTime = clientTime, UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetCurrentlyPlaying_BASE", new { ClientTime = clientTime, UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve favorite ChannelTubePo that are currently playing for client time ={0}", clientTime), ex);
            }

            return channels;
        }

         public List<ChannelTubePo> GetCurrentlyPlayingChannelsByCategoryName(DateTime clientTime, string categoryName)
        {
            List<ChannelTubePo> channels = new List<ChannelTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    //channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetCurrentlyPlayingChannelsByCategoryName", new { CategoryName = categoryName, ClientTime = clientTime }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetCurrentlyPlaying_BASE", new { CategoryName = categoryName, ClientTime = clientTime }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve favorite ChannelTubePo that are currently playing for client time ={0} and categoryName='{1}'", clientTime, categoryName), ex);
            }

            return channels;
        }
         public List<ChannelTubePo> GetCurrentlyPlayingChannelsByCategoryNameAndLanguageId(DateTime clientTime, int languageId)
         {
             List<ChannelTubePo> channels = new List<ChannelTubePo>();

             try
             {
                 if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                 {
                     //channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetCurrentlyPlayingChannelsByCategoryName", new { CategoryName = categoryName, ClientTime = clientTime }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                     channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetCurrentlyPlaying_BASE", new {  ClientTime = clientTime, LanguageId = languageId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                 }
             }
             catch (Exception ex)
             {
                 Logger.Error(String.Format("Failed to retrieve favorite ChannelTubePo that are currently playing for client time ={0} and LanguageId='{1}'", clientTime, languageId), ex);
             }

             return channels;
         }

         public List<ChannelTubePo> GetChannelsByUserIdAndClientTime(int userId, DateTime clientTime)
        {
            List<ChannelTubePo> channels = new List<ChannelTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetChannelsByUserIdAndClientTime", new { UserId = userId, ClientTime = clientTime }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user ChannelTubePo for user with Id={0} at '{1}'", userId, clientTime.ToString()), ex);
            }

            return channels;
        }

        public List<ChannelTubePo> GetAllFavoriteChannelsForUserByUserIdAndClientTime(int userId, DateTime clientTime)
        {
            List<ChannelTubePo> channels = new List<ChannelTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetAllFavoriteChannelsForUserByUserIdAndClientTime", new { UserId = userId, ClientTime = clientTime }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve favorite ChannelTubePo that are currently playing for user with Id={0}", userId), ex);
            }

            return channels;
        }
        public bool InsertUserChannelTubeViewByUserIdAndChannelTubeId(int channelTubeId, int? userId, DateTime viewTime)
        {
            Logger.Info(String.Format("Recording viewing occurance for channel with Id={0} for user with Id={1} at '{2}'", channelTubeId, userId, viewTime.ToString()));

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.InsertUserChannelTubeViewByUserIdAndChannelTubeId", new
                    {
                        ChannelTubeId = channelTubeId,
                        UserId = userId,
                        ViewTime = viewTime
                    },
                    null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert userChannelTubeView for channel with Id={0} for user with Id={1} at '{2}'", channelTubeId, userId, viewTime.ToString()), ex);
            }

            return isSuccess;
        }

        public List<ChannelTubePo> GetCurrentlyPlayingChannelsByUserNameAndClientTime(string userName, DateTime dateAndTime)
        {
            List<ChannelTubePo> channels = new List<ChannelTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    //channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetCurrentlyPlayingChannelsByUserNameAndClientTime", new { ClientTime = dateAndTime, Username = userName }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetCurrentlyPlaying_BASE", new { ClientTime = dateAndTime, Username = userName }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve currently playing channels for user '{0}' and client's time of '{1}'", userName, dateAndTime.ToString()), ex);
            }

            return channels;
        }

        public List<ChannelTubePo> GetCurrentlyPlayingChannelsByPublicUrlAndClientTime(string publicUrl, DateTime dateAndTime)
        {
            List<ChannelTubePo> channels = new List<ChannelTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    //channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetCurrentlyPlayingChannelsByPublicUrlAndClientTime", new { ClientTime = dateAndTime, PublicUrl = publicUrl }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetCurrentlyPlaying_BASE", new { ClientTime = dateAndTime, PublicUrl = publicUrl }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve currently playing channels for user '{0}' and client's time of '{1}'", publicUrl, dateAndTime.ToString()), ex);
            }

            return channels;
        }

        public List<ChannelTubePo> GetAllChannelsForUserByUserIdAndClientTime(int userId, DateTime date)
        {
            List<ChannelTubePo> channels = new List<ChannelTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetAllChannelsForUserByUserIdAndClientTime", new { UserId = userId, ClientTime = date }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve users ChannelTubePo that are currently playing for user with Id={0}", userId), ex);
            }

            return channels;
        }


        public List<UserChannelTubeView> GetChannelViewByUserIdChannelIdAndViewTime(int channelTubeId, int? userId, DateTime viewTime)
        {
            Logger.Info(String.Format("Retrievong view by date for channel with Id={0} for user with Id={1} at '{2}'", channelTubeId, userId, viewTime.ToString()));

            List<UserChannelTubeView> userChannelViews = new List<UserChannelTubeView>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    return userChannelViews = this.StrimmDbConnection.Query<UserChannelTubeView>("strimm.GetUserChannelTubeViewByUserIdChannelIdAndDate", new { UserId = userId, ChannelTubeId = channelTubeId, ViewTime = viewTime, }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to get userChannelTubeView for channel with Id={0} for user with Id={1} at '{2}' till '{3}'", channelTubeId, userId, viewTime.ToString()), ex);
            }

            return userChannelViews;
            
        }



        public bool SetChannelRatingByUserIdAndChannelTubeId(int userId, int channelTubeId, float ratingValue, DateTime enteredDate)
        {
            Logger.Info(String.Format("Set rating for channel with Id={0} for user with Id={1} at '{2}'", channelTubeId, userId, enteredDate.ToString()));

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.SetChannelRatingByUserIdAndChannelTubeId", new
                    {
                       
                        UserId = userId,
                        ChannelTubeId = channelTubeId,
                        Rating = ratingValue,
                        EnteredDate = enteredDate
                    },
                    null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed tset rating for channel with Id={0} for user with Id={1} at '{2}' till '{3}'", channelTubeId, userId, enteredDate.ToString()), ex);
            }

            return isSuccess;
        }

        public float GetUserRatingByUserIdAndChannelTubeId(int userId, int channelTubeId)
        {
            Logger.Info(String.Format("Get user rating for channel with Id={0} for user with Id={1}", channelTubeId, userId));

            float rating = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {

                    var dynamicParameter = new DynamicParameters();
                    dynamicParameter.Add("@UserId", userId);
                    dynamicParameter.Add("@ChannelTubeId", channelTubeId);
                    dynamicParameter.Add("@Rating", null, DbType.Double, ParameterDirection.Output);

                    rating = this.StrimmDbConnection.Execute("strimm.GetUserRatingByUserIdAndChannelTubeId", dynamicParameter,
                    null, 30, commandType: CommandType.StoredProcedure);

                    rating = (float)(dynamicParameter.Get<double?>("@Rating") ?? 0);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed get rating for channel with Id={0} for user with Id={1} at '{2}' ", channelTubeId, userId, ex));
            }

            return rating;
        }

        public List<ChannelTubePo> GetCurrentlyPlayingChannelsForLandingPage(string userName, string channelUrs, DateTime dateAndTime)
        {
            Logger.Info(String.Format("Retrieving currently playing channels for landing page, using: username='{0}', channels='{1}' at '{2}'",
                                        userName, channelUrs, dateAndTime));

            List<ChannelTubePo> channels = new List<ChannelTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    //channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetCurrentlyPlayingChannelsForLandingPage", 
                    //    new { 
                    //            ClientTime = dateAndTime, 
                    //            Username = userName,
                    //            Channels = channelUrs
                    //        }, 
                    //        null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetCurrentlyPlaying_BASE",
                        new
                        {
                            ClientTime = dateAndTime,
                            Username = userName,
                            Channels = channelUrs
                        },
                            null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve currently playing channels for landing page, using: username='{0}', channels='{1}' at '{2}'",
                                        userName, channelUrs, dateAndTime), ex);
            }

            return channels;        
        }

        public List<ChannelTubePo> GetChannelTubePosByUserIdForAdmin(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            var channelTubes = new List<ChannelTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channelTubes = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetChannelTubePosByUserIdForAdmin", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve all channel tube for user Id = {0}", userId), ex);
            }

            return channelTubes;
        }

        public List<ChannelStatistics> GetChannelStatistics(DateTime clientTime)
        {
            List<ChannelStatistics> channels = new List<ChannelStatistics>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channels = this.StrimmDbConnection.Query<ChannelStatistics>("strimm.GetChannelStatistics",new {clientTime = clientTime}, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve channel statistics for admin panel client time ={0}", clientTime), ex);
            }

            return channels;
        }
        public ChannelStatistics GetChannelStatisticsByChannelId(DateTime clientTime, int channelId)
        {
            ChannelStatistics channel = new ChannelStatistics();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channel = this.StrimmDbConnection.Query<ChannelStatistics>("strimm.GetChannelStatisticsByChannelId", new { clientTime = clientTime, channelId=channelId }, null, false, 30, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve channel statistics for admin panel client time ={0}", clientTime), ex);
            }

            return channel;
        }
        public TvGuideDataModel GetTvGuideDataByClientTime(DateTime clientTime, int pageIndex, int pageSize)
        {
            Contract.Requires(clientTime != null, "Client time is required");

            TvGuideDataModel tvGuideModel = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var dynamicParameter = new DynamicParameters();

                dynamicParameter.Add("@ClientTime", clientTime);
                dynamicParameter.Add("@PageIndex", pageIndex);
                dynamicParameter.Add("@PageSize", pageSize);
                dynamicParameter.Add("@PageCount", 0, DbType.Int32, ParameterDirection.Output);

                //using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetChannelsForTvGuideByClientTimeAndPageIndex", dynamicParameter, null, 30, commandType: CommandType.StoredProcedure))
                using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetChannelsForTvGuide_BASE", dynamicParameter, null, null, commandType: CommandType.StoredProcedure))
                {
                    tvGuideModel = new TvGuideDataModel();

                    var usersForActiveChannels = multi.Read<UserPo>().ToList();
                    var activeChannels = multi.Read<ChannelTubePo>().ToList();
                    var activeSchedules = multi.Read<VideoScheduleModel>().ToList();

                    tvGuideModel.PageCount = dynamicParameter.Get<int>("@PageCount");

                    BuildTvGuideModel(usersForActiveChannels, activeChannels, activeSchedules, tvGuideModel);

                    //if (activeChannels != null && activeChannels.Count > 0)
                    //{
                    //    activeChannels.ForEach(x =>
                    //    {
                    //        if (!tvGuideModel.ActiveChannels.Any(z => z.Channel.ChannelTubeId == x.ChannelTubeId))
                    //        {
                    //            var model = new TvGuideChannelDataModel();

                    //            model.User = usersForActiveChannels.SingleOrDefault(y => y.UserName == x.ChannelOwnerUserName);
                    //            model.Channel = new ChannelTubeModel(x);
                    //            model.VideoSchedules = activeSchedules.Where(y => y.ChannelTubeId == x.ChannelTubeId).ToList();

                    //            tvGuideModel.ActiveChannels.Add(model);
                    //        }
                    //    });
                    //}
                }
            }

            return tvGuideModel;
        }

        public TvGuideDataModel GetTvGuideDataTopChannelsByClientTime(DateTime clientTime, int pageIndex, int pageSize)
        {
            Contract.Requires(clientTime != null, "Client time is required");

            TvGuideDataModel tvGuideModel = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var dynamicParameter = new DynamicParameters();

                dynamicParameter.Add("@ClientTime", clientTime);
                dynamicParameter.Add("@PageIndex", pageIndex);
                dynamicParameter.Add("@PageSize", pageSize);
                dynamicParameter.Add("@PageCount", 0, DbType.Int32, ParameterDirection.Output);

                //using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetChannelsForTvGuideByClientTimeAndPageIndex", dynamicParameter, null, 30, commandType: CommandType.StoredProcedure))
                using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetTopChannelsForTvGuideByClientTimeAndPageIndex", dynamicParameter, null, null, commandType: CommandType.StoredProcedure))
                {
                    tvGuideModel = new TvGuideDataModel();

                    var usersForActiveChannels = multi.Read<UserPo>().ToList();
                    var activeChannels = multi.Read<ChannelTubePo>().ToList();
                    var activeSchedules = multi.Read<VideoScheduleModel>().ToList();

                    tvGuideModel.PageCount = dynamicParameter.Get<int>("@PageCount");

                    BuildTvGuideModel(usersForActiveChannels, activeChannels, activeSchedules, tvGuideModel);

                    //if (activeChannels != null && activeChannels.Count > 0)
                    //{
                    //    activeChannels.ForEach(x =>
                    //    {
                    //        if (!tvGuideModel.ActiveChannels.Any(z => z.Channel.ChannelTubeId == x.ChannelTubeId))
                    //        {
                    //            var model = new TvGuideChannelDataModel();

                    //            model.User = usersForActiveChannels.SingleOrDefault(y => y.UserName == x.ChannelOwnerUserName);
                    //            model.Channel = new ChannelTubeModel(x);
                    //            model.VideoSchedules = activeSchedules.Where(y => y.ChannelTubeId == x.ChannelTubeId).ToList();

                    //            tvGuideModel.ActiveChannels.Add(model);
                    //        }
                    //    });
                    //}
                }
            }

            return tvGuideModel;
        }

        public TvGuideDataModel GetTvGuideFavoriteChannelsForUserByClientTime(DateTime clientTime, int userId, int pageIndex, int pageSize)
        {
            Contract.Requires(clientTime != null, "Client time is required");
            Contract.Requires(userId > 0, "UserId is required");

            TvGuideDataModel tvGuideModel = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var dynamicParameter = new DynamicParameters();

                dynamicParameter.Add("@ClientTime", clientTime);
                dynamicParameter.Add("@UserId", userId);
                dynamicParameter.Add("@PageIndex", pageIndex);
                dynamicParameter.Add("@PageSize", pageSize);
                dynamicParameter.Add("@PageCount", 0, DbType.Int32, ParameterDirection.Output);

                using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetSubscribedChannelsForTvGuideByClientTimeUserIdAndPageIndex", dynamicParameter, null, null, commandType: CommandType.StoredProcedure))
                {
                    tvGuideModel = new TvGuideDataModel();

                    var usersForActiveChannels = multi.Read<UserPo>().ToList();
                    var activeChannels = multi.Read<ChannelTubePo>().ToList();
                    var activeSchedules = multi.Read<VideoScheduleModel>().ToList();

                    tvGuideModel.PageCount = dynamicParameter.Get<int>("@PageCount");

                    BuildTvGuideModel(usersForActiveChannels, activeChannels, activeSchedules, tvGuideModel);

                    //if (activeChannels != null && activeChannels.Count > 0)
                    //{
                    //    activeChannels.ForEach(x =>
                    //    {
                    //        if (!tvGuideModel.ActiveChannels.Any(z => z.Channel.ChannelTubeId == x.ChannelTubeId))
                    //        {
                    //            var model = new TvGuideChannelDataModel();

                    //            model.User = usersForActiveChannels.SingleOrDefault(y => y.UserName == x.ChannelOwnerUserName);
                    //            model.Channel = new ChannelTubeModel(x);
                    //            model.VideoSchedules = activeSchedules.Where(y => y.ChannelTubeId == x.ChannelTubeId).ToList();

                    //            tvGuideModel.ActiveChannels.Add(model);
                    //        }
                    //    });
                    //}
                }
            }

            return tvGuideModel;
        }

        public TvGuideDataModel GetTvGuideUserChannelsById(DateTime clientTime, int userId, int pageIndex, int pageSize)
        {
            Contract.Requires(clientTime != null, "Client time is required");
            Contract.Requires(userId > 0, "UserId is required");

            TvGuideDataModel tvGuideModel = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var dynamicParameter = new DynamicParameters();

                dynamicParameter.Add("@ClientTime", clientTime);
                dynamicParameter.Add("@UserId", userId);
                dynamicParameter.Add("@PageIndex", pageIndex);
                dynamicParameter.Add("@PageSize", pageSize);
                dynamicParameter.Add("@PageCount", 0, DbType.Int32, ParameterDirection.Output);

                //using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetChannelsForTvGuideByClientTimeUserIdAndPageIndex", dynamicParameter, null, 30, commandType: CommandType.StoredProcedure))
                using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetChannelsForTvGuide_BASE", dynamicParameter, null, null, commandType: CommandType.StoredProcedure))
                {
                    tvGuideModel = new TvGuideDataModel();

                    var usersForActiveChannels = multi.Read<UserPo>().ToList();
                    var activeChannels = multi.Read<ChannelTubePo>().ToList();
                    var activeSchedules = multi.Read<VideoScheduleModel>().ToList();

                    tvGuideModel.PageCount = dynamicParameter.Get<int>("@PageCount");

                    BuildTvGuideModel(usersForActiveChannels, activeChannels, activeSchedules, tvGuideModel);
                }
            }

            return tvGuideModel;
        }

        private void BuildTvGuideModel(List<UserPo> usersForActiveChannels, List<ChannelTubePo> activeChannels, List<VideoScheduleModel> activeSchedules, TvGuideDataModel tvGuideModel)
        {            
            if (activeChannels != null && activeChannels.Count > 0)
            {
                activeChannels.ForEach(x =>
                {
                    if (!tvGuideModel.ActiveChannels.Any(z => z.Channel.ChannelTubeId == x.ChannelTubeId))
                    {
                        var model = new TvGuideChannelDataModel();

                        model.User = usersForActiveChannels.SingleOrDefault(y => y.UserName == x.ChannelOwnerUserName);
                        model.Channel = new ChannelTubeModel(x);
                        //model.VideoSchedules = activeSchedules.Where(y => y.ChannelTubeId == x.ChannelTubeId).ToList();

                        var actualSchedule = activeSchedules.Where(y => y.ChannelTubeId == x.ChannelTubeId).ToList();

                        VideoScheduleModel groupVideoScheduleModel = null;

                        double currentGroupVideoDuration = 0;
                        var videoSchedulesInGroup = new List<VideoScheduleModel>();
                        int inGroupIndex = 0;

                        actualSchedule.ForEach(vs =>
                        {
                            var inMins = vs.Duration / 60;

                            if (inMins < 15)
                            {
                                if (inGroupIndex == 0)
                                {
                                    groupVideoScheduleModel = vs;
                                    groupVideoScheduleModel.CategoryName = "Other";
                                    groupVideoScheduleModel.Description = "Group of assorted videos of short duration:\r\n";
                                    groupVideoScheduleModel.VideoTubeTitle = "Assorted Videos";
                                    groupVideoScheduleModel.VideoProviderName = "Multiple";
                                }

                                currentGroupVideoDuration += inMins;
                                videoSchedulesInGroup.Add(vs);

                                if (currentGroupVideoDuration >= 15)
                                {
                                    if (videoSchedulesInGroup.Count == 1)
                                    {
                                        model.VideoSchedules.Add(vs);
                                    }
                                    else
                                    {
                                        groupVideoScheduleModel.Duration = (long)(vs.PlaybackEndTime - groupVideoScheduleModel.PlaybackStartTime).TotalSeconds;
                                        groupVideoScheduleModel.PlaybackEndTime = vs.PlaybackEndTime;
                                        groupVideoScheduleModel.PlaybackOrderNumber = model.VideoSchedules.Count + 10000;
                                        groupVideoScheduleModel.PlaytimeMessage = DateTimeUtils.PrintPlaytimeDuration(groupVideoScheduleModel.PlaybackStartTime, groupVideoScheduleModel.PlaybackEndTime);
                                        groupVideoScheduleModel.Description += vs.VideoTubeTitle;
                                        groupVideoScheduleModel.Description += "\r\n";

                                        model.VideoSchedules.Add(groupVideoScheduleModel);

                                    }

                                    videoSchedulesInGroup.Clear();
                                    currentGroupVideoDuration = 0;
                                    inGroupIndex = 0;
                                }
                                else
                                {
                                    if (inGroupIndex == 0)
                                    {
                                        groupVideoScheduleModel.PlaybackStartTime = vs.PlaybackStartTime;
                                        groupVideoScheduleModel.PlayerPlaybackStartTimeInSec = vs.PlayerPlaybackStartTimeInSec;
                                        groupVideoScheduleModel.ProviderVideoId = vs.ProviderVideoId;
                                        groupVideoScheduleModel.Thumbnail = vs.Thumbnail;
                                        groupVideoScheduleModel.Url = vs.Url;
                                        groupVideoScheduleModel.UserPublicUrl = vs.UserPublicUrl;
                                        groupVideoScheduleModel.VideoTubeId = vs.VideoTubeId;
                                        groupVideoScheduleModel.Description += vs.VideoTubeTitle;
                                        groupVideoScheduleModel.Description += "\r\n";
                                    }

                                    // This will be done for every video, even if it is short, so that if the next video is long
                                    // we already have all of the end info for group
                                    groupVideoScheduleModel.Duration = (long)(vs.PlaybackEndTime - groupVideoScheduleModel.PlaybackStartTime).TotalSeconds;
                                    groupVideoScheduleModel.PlaybackEndTime = vs.PlaybackEndTime;
                                    groupVideoScheduleModel.PlaybackOrderNumber = model.VideoSchedules.Count + 10000;
                                    groupVideoScheduleModel.PlaytimeMessage = DateTimeUtils.PrintPlaytimeDuration(groupVideoScheduleModel.PlaybackStartTime, groupVideoScheduleModel.PlaybackEndTime);
                                    groupVideoScheduleModel.Description += vs.VideoTubeTitle;
                                    groupVideoScheduleModel.Description += "\r\n";

                                    inGroupIndex++;
                                }
                            }
                            else
                            {
                                if (inGroupIndex > 0)
                                {
                                    model.VideoSchedules.Add(groupVideoScheduleModel);

                                    videoSchedulesInGroup.Clear();
                                    currentGroupVideoDuration = 0;
                                    inGroupIndex = 0;
                                }

                                model.VideoSchedules.Add(vs);
                            }
                        });

                        tvGuideModel.ActiveChannels.Add(model);
                    }
                });
            }
        }

        public TvGuideDataModel GetTvGuideChannelsByLanguageId(DateTime clientTime, int languageId, int pageIndex, int pageSize)
        {
            Contract.Requires(clientTime != null, "Client time is required");
            Contract.Requires(languageId > 0, "LanguageId is required");

            TvGuideDataModel tvGuideModel = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var dynamicParameter = new DynamicParameters();

                dynamicParameter.Add("@ClientTime", clientTime);
                dynamicParameter.Add("@LanguageId", languageId);
                dynamicParameter.Add("@PageIndex", pageIndex);
                dynamicParameter.Add("@PageSize", pageSize);
                dynamicParameter.Add("@PageCount", 0, DbType.Int32, ParameterDirection.Output);

                //using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetChannelsForTvGuideByClientTimeLanguageIdAndPageIndex", dynamicParameter, null, 30, commandType: CommandType.StoredProcedure))
                using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetChannelsForTvGuide_BASE", dynamicParameter, null, null, commandType: CommandType.StoredProcedure))
                {
                    tvGuideModel = new TvGuideDataModel();

                    var usersForActiveChannels = multi.Read<UserPo>().ToList();
                    var activeChannels = multi.Read<ChannelTubePo>().ToList();
                    var activeSchedules = multi.Read<VideoScheduleModel>().ToList();

                    tvGuideModel.PageCount = dynamicParameter.Get<int>("@PageCount");

                    BuildTvGuideModel(usersForActiveChannels, activeChannels, activeSchedules, tvGuideModel);

                    //if (activeChannels != null && activeChannels.Count > 0)
                    //{
                    //    activeChannels.ForEach(x =>
                    //    {
                    //        if (!tvGuideModel.ActiveChannels.Any(z => z.Channel.ChannelTubeId == x.ChannelTubeId))
                    //        {
                    //            var model = new TvGuideChannelDataModel();

                    //            model.User = usersForActiveChannels.SingleOrDefault(y => y.UserName == x.ChannelOwnerUserName);
                    //            model.Channel = new ChannelTubeModel(x);
                    //            model.VideoSchedules = activeSchedules.Where(y => y.ChannelTubeId == x.ChannelTubeId).ToList();

                    //            tvGuideModel.ActiveChannels.Add(model);
                    //        }
                    //    });
                    //}
                }
            }

            return tvGuideModel;
        }

        public TvGuideDataModel GetTvGuideChannelsByCategoryId(DateTime clientTime, int categoryId, int pageIndex, int pageSize)
        {
            Contract.Requires(clientTime != null, "Client time is required");
            Contract.Requires(categoryId > 0, "CategoryId is required");

            TvGuideDataModel tvGuideModel = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var dynamicParameter = new DynamicParameters();

                dynamicParameter.Add("@ClientTime", clientTime);
                dynamicParameter.Add("@CategoryId", categoryId);
                dynamicParameter.Add("@PageIndex", pageIndex);
                dynamicParameter.Add("@PageSize", pageSize);
                dynamicParameter.Add("@PageCount", 0, DbType.Int32, ParameterDirection.Output);

                //using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetChannelsForTvGuideByClientTimeCategoryIdAndPageIndex", dynamicParameter, null, 30, commandType: CommandType.StoredProcedure))
                using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetChannelsForTvGuide_BASE", dynamicParameter, null, null, commandType: CommandType.StoredProcedure))
                {
                    tvGuideModel = new TvGuideDataModel();
                    
                    var usersForActiveChannels = multi.Read<UserPo>().ToList();
                    var activeChannels = multi.Read<ChannelTubePo>().ToList();
                    var activeSchedules = multi.Read<VideoScheduleModel>().ToList();

                    tvGuideModel.PageCount = dynamicParameter.Get<int>("@PageCount");

                    BuildTvGuideModel(usersForActiveChannels, activeChannels, activeSchedules, tvGuideModel);

                    //if (activeChannels != null && activeChannels.Count > 0)
                    //{
                    //    activeChannels.ForEach(x =>
                    //    {
                    //        if (!tvGuideModel.ActiveChannels.Any(z => z.Channel.ChannelTubeId == x.ChannelTubeId))
                    //        {
                    //            var model = new TvGuideChannelDataModel();

                    //            model.User = usersForActiveChannels.SingleOrDefault(y => y.UserName == x.ChannelOwnerUserName);
                    //            model.Channel = new ChannelTubeModel(x);
                    //            model.VideoSchedules = activeSchedules.Where(y => y.ChannelTubeId == x.ChannelTubeId).ToList();

                    //            tvGuideModel.ActiveChannels.Add(model);
                    //        }
                    //    });
                    //}
                }
            }

            return tvGuideModel;
        }

        public TvGuideDataModel GetTvGuideChannelsByKeywords(DateTime clientTime, string keywords, int pageIndex, int pageSize)
        {
            Contract.Requires(clientTime != null, "Client time is required");
            Contract.Requires(!String.IsNullOrEmpty(keywords), "Keywords are required");

            TvGuideDataModel tvGuideModel = null;

            string[] words = keywords.Split(' ');
            StringBuilder sb = new StringBuilder();
            words.ToList().ForEach(x => sb.Append('*').Append(x).Append('*').Append(','));
            string search = sb.ToString().TrimEnd(',');

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var dynamicParameter = new DynamicParameters();

                dynamicParameter.Add("@ClientTime", clientTime);
                dynamicParameter.Add("@Keywords", search);
                dynamicParameter.Add("@PageIndex", pageIndex);
                dynamicParameter.Add("@PageSize", pageSize);
                dynamicParameter.Add("@PageCount", 0, DbType.Int32, ParameterDirection.Output);

                //using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetChannelsForTvGuideByClientTimeKeywordsAndPageIndex", dynamicParameter, null, 30, commandType: CommandType.StoredProcedure))
                using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetChannelsForTvGuide_BASE", dynamicParameter, null, null, commandType: CommandType.StoredProcedure))
                {
                    tvGuideModel = new TvGuideDataModel();

                    var usersForActiveChannels = multi.Read<UserPo>().ToList();
                    var activeChannels = multi.Read<ChannelTubePo>().ToList();
                    var activeSchedules = multi.Read<VideoScheduleModel>().ToList();

                    tvGuideModel.PageCount = dynamicParameter.Get<int>("@PageCount");

                    BuildTvGuideModel(usersForActiveChannels, activeChannels, activeSchedules, tvGuideModel);

                    //if (activeChannels != null && activeChannels.Count > 0)
                    //{
                    //    activeChannels.ForEach(x =>
                    //    {
                    //        if (!tvGuideModel.ActiveChannels.Any(z => z.Channel.ChannelTubeId == x.ChannelTubeId))
                    //        {
                    //            var model = new TvGuideChannelDataModel();

                    //            model.User = usersForActiveChannels.SingleOrDefault(y => y.UserName == x.ChannelOwnerUserName);
                    //            model.Channel = new ChannelTubeModel(x);
                    //            model.VideoSchedules = activeSchedules.Where(y => y.ChannelTubeId == x.ChannelTubeId).ToList();

                    //            tvGuideModel.ActiveChannels.Add(model);
                    //        }
                    //    });
                    //}
                }
            }

            return tvGuideModel;
        }

        public ChannelTubePo GetChannelTubePoWithUserDataByChannelIdAndUserId(int channelTubeId, int userId)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");
            Contract.Requires(userId > 0, "Invalid User Id specified 0");

            ChannelTubePo channelTube = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetChannelTubePoWithUserDataByChannelIdAndUserId", new { ChannelTubeId = channelTubeId, UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    channelTube = results.Count == 1 ? results.First() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve channel tube with Id = {0}", channelTubeId), ex);
            }

            return channelTube;
        }

        public ChannelTubePageModel GetCurrentlyPlayingChannelsSortedByPageIndex(DateTime clientTime, string sortBy, int? languageId, int pageIndex, int pageSize)
        {
            Contract.Requires(clientTime != null, "Client time is required");

            ChannelTubePageModel pageModel = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var dynamicParameter = new DynamicParameters();

                dynamicParameter.Add("@ClientTime", clientTime);
                dynamicParameter.Add("@SortBy", sortBy);
                dynamicParameter.Add("@LanguageId", languageId);
                dynamicParameter.Add("@PageIndex", pageIndex);
                dynamicParameter.Add("@PageSize", pageSize);
                dynamicParameter.Add("@PageCount", 0, DbType.Int32, ParameterDirection.Output);
                dynamicParameter.Add("@ChannelCount", 0, DbType.Int32, ParameterDirection.Output);

                using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetCurrentlyPlayingChannelsSortedByPageIndex", dynamicParameter, null, null, commandType: CommandType.StoredProcedure))
                {
                    pageModel = new ChannelTubePageModel();

                    var activeChannels = multi.Read<ChannelTubePo>().ToList();

                    pageModel.ChannelTubeModels = new List<ChannelTubeModel>();

                    activeChannels.ForEach(x => pageModel.ChannelTubeModels.Add(new ChannelTubeModel(x)));

                    pageModel.PageCount = dynamicParameter.Get<int>("@PageCount");
                    pageModel.ChannelCount = dynamicParameter.Get<int>("@ChannelCount");
                    pageModel.IsSuccess = true;
                    pageModel.PageIndex = pageIndex;
                    pageModel.NextPageIndex = (pageIndex < pageModel.PageCount) ? pageIndex + 1 : pageIndex;
                    pageModel.PrevPageIndex = (pageIndex > 1) ? pageIndex - 1 : pageIndex;                   
                }
            }

            return pageModel;
        }

        public UserChannelEntitlements GetUserChannelEntitlementsByUser(int userId)
        {
            Contract.Requires(userId > 0, "Invalid User Id specified 0");

            UserChannelEntitlements entitlements = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<UserChannelEntitlements>("strimm.GetUserChannelEntitlementsByUserId", new {  UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    entitlements = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve channel entitlements for user with Id = {0}", userId), ex);
            }

            return entitlements;
        }



        public object DeleteSubscriberDomain(int userId, int channelTubeId)
        {
            Contract.Requires(channelTubeId > 0, "Specified channelTube id is invalid");
            Contract.Requires(userId > 0, "Specified user id is invalid");


            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.DeleteSubscriberDomainByUserIdAndChannelId", new
                    {

                        UserId = userId,
                        ChannelTubeId = channelTubeId

                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete subscriber domain tube for user with id={0} and channelTubeId={2}", userId, channelTubeId), ex);
            }

            return isSuccess;
        }

        public TvGuideDataModel GetEmbeddedTvGuideByUserIdAndPageIndex(DateTime clientTimeDateTime, int userId, string domain, int pageIndex, int pageSize)
        {
            Contract.Requires(clientTimeDateTime != null, "Client time is required");

            TvGuideDataModel tvGuideModel = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var dynamicParameter = new DynamicParameters();

                dynamicParameter.Add("@ClientTime", clientTimeDateTime);
                dynamicParameter.Add("@UserId", userId);
                dynamicParameter.Add("@UserDomain", domain);
                dynamicParameter.Add("@PageIndex", pageIndex);
                dynamicParameter.Add("@PageSize", pageSize);
                dynamicParameter.Add("@PageCount", 0, DbType.Int32, ParameterDirection.Output);

                using (var multi = this.StrimmDbConnection.QueryMultiple("strimm.GetChannelsForTvGuideByClientTimeUserIdDomainAndPageIndex", dynamicParameter, null, null, commandType: CommandType.StoredProcedure))
                {
                    tvGuideModel = new TvGuideDataModel();

                    var usersForActiveChannels = multi.Read<UserPo>().ToList();
                    var activeChannels = multi.Read<ChannelTubePo>().ToList();
                    var activeSchedules = multi.Read<VideoScheduleModel>().ToList();

                    tvGuideModel.PageCount = dynamicParameter.Get<int>("@PageCount");

                    BuildTvGuideModel(usersForActiveChannels, activeChannels, activeSchedules, tvGuideModel);
                }
            }

            return tvGuideModel;
        }
        
    }
}

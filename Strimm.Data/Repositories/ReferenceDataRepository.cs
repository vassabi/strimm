using Strimm.Data.Interfaces;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using log4net;
using System.Diagnostics.Contracts;
using Strimm.Model.Projections;

namespace Strimm.Data.Repositories
{
    public class ReferenceDataRepository : RepositoryBase, IReferenceDataRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ReferenceDataRepository));

        public ReferenceDataRepository()
            : base()
        {

        }

        public List<Country> GetAllCountries()
        {
            var counties = new List<Country>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    counties = this.StrimmDbConnection.Query<Country>("strimm.GetAllCountries", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve all counties", ex);
            }

            return counties;
        }

        public Country GetCountryById(int countryId)
        {
            Contract.Requires(countryId > 0, "CountryId should be greater then 0");

            Country country = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<Country>("strimm.GetCountryById", new { CountryId = countryId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    country = results.Count > 0 ? results.First() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve country by Id = {0}", countryId), ex);
            }

            return country;
        }

        public List<State> GetAllStates()
        {
            var states = new List<State>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    states = this.StrimmDbConnection.Query<State>("strimm.GetAllStates", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve all states", ex);
            }

            return states;
        }

        public State GetStateById(int stateId)
        {
            Contract.Requires(stateId > 0, "StateId should be greater then 0");

            State state = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<State>("strimm.GetStateById", new { StateId = stateId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    state = results.Count > 0 ? results.First() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve state by Id = {0}", stateId), ex);
            }

            return state;
        }

        public List<State> GetStatesByCountry(string countryName)
        {
            Contract.Requires(!String.IsNullOrEmpty(countryName), "Invalid or empty country name specified");

            var states = new List<State>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    states = this.StrimmDbConnection.Query<State>("strimm.GetStatesByCountry", new { CountryName = countryName }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve states for country = {0}", countryName), ex);
            }

            return states;
        }

        public bool IsReservedName(string name)
        {
            Contract.Requires(!String.IsNullOrEmpty(name), "Invalid or empty reserved name specified");

            bool isReserved = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var p = new DynamicParameters();
                    p.Add("@ReservedName", name);
                    p.Add("@IsReserved", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    this.StrimmDbConnection.Execute("strimm.IsReservedName", p, null, 30, commandType: CommandType.StoredProcedure);

                    isReserved = p.Get<bool>("@IsReserved");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to check if name {0} was reserved", name), ex);
            }

            return isReserved;
        }

        public List<Category> GetAllCategories()
        {
            var categories = new List<Category>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    categories = this.StrimmDbConnection.Query<Category>("strimm.GetAllCategories", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve all states", ex);
            }

            return categories;
        }

        public List<Category> GetAllCategories(int videoTypeId)
        {
            var categories = new List<Category>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    //add parameter to SPROC
                    categories = this.StrimmDbConnection.Query<Category>("strimm.GetAllCategories", new { VideoTypeID = videoTypeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve all states", ex);
            }

            return categories;
        }
        public List<Language> GetAllLanguages()
        {
            var languages = new List<Language>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    languages = this.StrimmDbConnection.Query<Language>("strimm.GetAllLanguages", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve all languages", ex);
            }

            return languages;
        }

        public Category GetCategoryById(int categoryId)
        {
            Contract.Requires(categoryId > 0, "CategoryId should be greater then 0");

            Category category = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<Category>("strimm.GetStateById", new { CategoryId = categoryId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    category = results.Count > 0 ? results.First() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve category by Id = {0}", categoryId), ex);
            }

            return category;
        }

        public List<State> GetStatesByCountryId(int countryId)
        {
            Contract.Requires(countryId!=0, "Invalid or empty countryId specified");

            var states = new List<State>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    states = this.StrimmDbConnection.Query<State>("strimm.GetStatesByCountryId", new { CountryId = countryId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve states for countryId = {0}", countryId), ex);
            }

            return states;
        }

        public List<ChannelCategoryPo> GetAllCategoriesWithCurrentlyPlayingChannelsCountForBrowseChannels(DateTime clientTime)
        {
            var categories = new List<ChannelCategoryPo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    categories = this.StrimmDbConnection.Query<ChannelCategoryPo>("strimm.GetChannelCategoriesWithCurrentlyPlayingChannelCountByClientTimeForBrowseChannels", new { ClientTime = clientTime }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve categries with currently playing channels for browse channels menu at '{0}'",clientTime), ex);
            }

            return categories;
        }

        public List<ChannelCategoryPo> GetAllCategoriesWithCurrentlyPlayingChannelsCount(DateTime clientTime)
        {
            var categories = new List<ChannelCategoryPo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    categories = this.StrimmDbConnection.Query<ChannelCategoryPo>("strimm.GetChannelCategoriesWithCurrentlyPlayingChannelCountByClientTime", new { ClientTime = clientTime }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve categries with currently playing channels for client time", ex);
            }

            return categories;
        }


    }
}

using log4net;
using Strimm.Data.Repositories;
using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using Strimm.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL
{
    public class ReferenceDataManage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ReferenceDataManage));

        public static List<Country> GetCountries()
        {
            var countries = new List<Country>();

            using (var referenceDataRepository = new ReferenceDataRepository())
            {
                countries = referenceDataRepository.GetAllCountries();
            }

            return countries;
        }

        public static List<State> GetStates()
        {
            var states = new List<State>();

            using (var referenceDataRepository = new ReferenceDataRepository())
            {
                states = referenceDataRepository.GetAllStates();
            }

            return states;
        }

        public static List<State> GetStatesByCountryName(string countryName)
        {
            var states = new List<State>();

            using (var referenceDataRepository = new ReferenceDataRepository())
            {
                states = referenceDataRepository.GetStatesByCountry(countryName);
            }

            return states;
        }

        public static List<Category> GetAllCategories()
        {
            var categories = new List<Category>();

            using (var referenceDataRepository = new ReferenceDataRepository())
            {
                categories = referenceDataRepository.GetAllCategories();
            }

            return categories;
        }
        public static List<Category> GetAllCategories(int videoTypeId)
        {
            var categories = new List<Category>();

            using (var referenceDataRepository = new ReferenceDataRepository())
            {
                categories = referenceDataRepository.GetAllCategories(2);
            }

            return categories;
        }


        public static List<Language>GetAllLanguages()
        {
            var languages = new List<Language>();

            using (var referenceDataRepository = new ReferenceDataRepository())
            {
                languages = referenceDataRepository.GetAllLanguages().OrderBy(x=>x.LanguageId).ToList();
            }

            return languages;
        }

        public static List<CategoryModel> GetChannelCategoriesWithCurrentlyPlayingChannelsCountForBrowseChannels(DateTime clientTime)
        {
            var models = new List<CategoryModel>();

            using (var referenceDataRepository = new ReferenceDataRepository())
            {
                var categories = referenceDataRepository.GetAllCategoriesWithCurrentlyPlayingChannelsCountForBrowseChannels(clientTime);
                var popular = new CategoryModel();

                categories.ForEach(x =>
                {
                    if (x.Name == "All Channels")
                    {
                        popular = new CategoryModel(x)
                        {
                            ChannelCount = x.ChannelCount,
                        };
                    }
                    else
                    {
                        models.Add(new CategoryModel(x)
                        {
                            ChannelCount = x.ChannelCount
                        });
                    }
                });

                models.Insert(0, popular);
            }

            return models;
        }

        public static List<CategoryModel> GetChannelCategoriesWithCurrentlyPlayingChannelsCount(DateTime clientTime)
        {
            var models = new List<CategoryModel>();

            using (var referenceDataRepository = new ReferenceDataRepository())
            {
                var categories = referenceDataRepository.GetAllCategoriesWithCurrentlyPlayingChannelsCount(clientTime);
                categories.ForEach(x =>
                {
                    models.Add(new CategoryModel(x));
                });
            }

            return models;
        }

        public static bool IsReservedName(string name)
        {
            bool isReserved = false;

            var states = new List<State>();

            using (var referenceDataRepository = new ReferenceDataRepository())
            {
                isReserved = referenceDataRepository.IsReservedName(name) && referenceDataRepository.IsReservedName(PublicNameUtils.GetUrl(name));
            }

            return isReserved;
        }

        public static List<State> GetStatesByCountryId(int countryId)
        {
            var states = new List<State>();

            using (var referenceDataRepository = new ReferenceDataRepository())
            {
                states = referenceDataRepository.GetStatesByCountryId(countryId);
            }

            return states;
        }
    }
}

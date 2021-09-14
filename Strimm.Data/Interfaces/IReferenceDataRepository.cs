using Strimm.Model;
using Strimm.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IReferenceDataRepository
    {
        List<Country> GetAllCountries();

        Country GetCountryById(int countryId);

        List<State> GetAllStates();

        State GetStateById(int stateId);

        List<State> GetStatesByCountry(string countryName);

        bool IsReservedName(string name);

        List<Category> GetAllCategories();

        Category GetCategoryById(int categoryId);

        List<ChannelCategoryPo> GetAllCategoriesWithCurrentlyPlayingChannelsCount(DateTime clientTime);

        List<Language> GetAllLanguages();
    }
}

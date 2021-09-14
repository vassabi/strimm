using log4net;
using Strimm.Data.Repositories;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL
{
    public static class CountriesManage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CountriesManage));

       public static List<Country> GetAllCountries()
       {
           Logger.Debug("Retrieving all contries");
           var countrylist = new List<Country>();
           using( var refDataRepository = new ReferenceDataRepository())
           {
                countrylist = refDataRepository.GetAllCountries();
           }
           

           return countrylist;
       }
    }
}

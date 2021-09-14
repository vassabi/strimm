using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Strimm.Data.Interfaces;
using Strimm.Data.Repositories;
using Strimm.Model;

namespace Strimm.WebApi.Controllers
{
    public class ReferenceDataController : BaseApiController
    {
        private IReferenceDataRepository referenceDataRepository;

        public ReferenceDataController(IReferenceDataRepository referenceDataRepository)
        {
            this.referenceDataRepository = referenceDataRepository; 
        }

        /// <summary>
        /// This method will return all supported countries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/data/countries", Name = "AllCountries")]
        public IEnumerable<Country> GetAllCountries()
        {
            return this.referenceDataRepository.GetAllCountries();
        }

        /// <summary>
        /// This method will return all supported states
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/data/states", Name = "AllStates")]
        public IEnumerable<State> GetAllStates()
        {
            return this.referenceDataRepository.GetAllStates();
        }

        /// <summary>
        /// This method will return all supported categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/data/categories", Name = "AllCategories")]
        public IEnumerable<Category> GetAllCategories()
        {
            return this.referenceDataRepository.GetAllCategories();
        }
    }
}

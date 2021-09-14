using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Strimm.Data.Repositories;
using System.Linq;

namespace Strimm.Data.Tests
{
    [TestClass]
    public class VideoProviderTests
    {
        private VideoProviderRepository repository = new VideoProviderRepository();

        [TestMethod]
        public void GetAllVideoProvidersTest()
        {
            var providers = repository.GetAllVideoProviders();

            Assert.IsNotNull(providers, "No video providers retrieved");
            Assert.IsNotNull(providers.FirstOrDefault(x => x.Name == "youtube"), "Not able to retrieve youtube provider");
            Assert.IsNotNull(providers.FirstOrDefault(x => x.Name == "vimeo"), "Not able to retrieve vimeo provider");
        }

        [TestMethod]
        public void GetActiveVideoProviers()
        {
            var providers = repository.GetActiveVideoProviders();

            Assert.IsNotNull(providers, "No active video providers");
            Assert.IsTrue(providers.Count > 0, "No active providers found");
        }
    }
}

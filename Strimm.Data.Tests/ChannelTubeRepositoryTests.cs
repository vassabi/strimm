using Microsoft.VisualStudio.TestTools.UnitTesting;
using Strimm.Data.Interfaces;
using Strimm.Data.Repositories;
using Strimm.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Tests
{
    [TestClass]
    public class ChannelTubeRepositoryTests
    {
        private ChannelTubeRepository channelTubeRepository = new ChannelTubeRepository();

        [TestMethod]
        public void GetChannelTubePoByIdTest()
        {
            ChannelTubePo channelTubePo = channelTubeRepository.GetChannelTubePoById(1013);

            Assert.IsNotNull(channelTubePo, "ChannelTubePo cannot be NULL");
        }

        [TestMethod]
        public void GetChannelTubePoByKeywordsTest()
        {
            var channels = channelTubeRepository.GetChannelTubePoByKeywords(new List<string>() { "Max", "Val" }, DateTime.Now);

            Assert.IsNotNull(channels, "Failed to retrieve channel tubes by specified keywords");
            Assert.IsTrue(channels.Count > 0, "Failed to retrieve channel tubes. Collection is empty");
        }

    }
}

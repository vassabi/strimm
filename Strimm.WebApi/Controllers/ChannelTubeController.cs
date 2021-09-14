using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using Strimm.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Strimm.WebApi.Controllers
{
    public class ChannelTubeController : BaseApiController
    {
        private List<ChannelTubePo> channels;

        public ChannelTubeController()
        {
            channels = new List<ChannelTubePo>();
            channels.Add(new ChannelTubePo()
            {
                CategoryId = 1,
                CategoryName = "Sports",
                CreatedDate = DateTime.Now,
                ChannelTubeId = 1,
                Description = "Channel 1 Description",
                IsFeatured = true,
                IsLocked = false,
                IsPrivate = false,
                Name = "Channel 1",
                PictureUrl = "/images/todaysPostImg.png",
                Rating = 5,
                Url = "/images/todaysPostImg.png",
                UserId = 1058,
                VideoTubeCount = 15,
                SubscriberCount = 5678,
                ChannelViewsCount = 5468778
            });
            channels.Add(new ChannelTubePo()
            {
                CategoryId = 1,
                CategoryName = "Sports",
                CreatedDate = DateTime.Now,
                ChannelTubeId = 2,
                Description = "Channel 2 Description",
                IsFeatured = true,
                IsLocked = false,
                IsPrivate = false,
                Name = "Channel 2",
                PictureUrl = "/images/todaysPostImg.png",
                Rating = 4,
                Url = "/images/todaysPostImg.png",
                UserId = 1058,
                VideoTubeCount = 11,
                SubscriberCount = 25,
                ChannelViewsCount = 12312
            });
            channels.Add(new ChannelTubePo()
            {
                CategoryId = 1,
                CategoryName = "Sports",
                CreatedDate = DateTime.Now,
                ChannelTubeId = 3,
                Description = "Channel 3 Description",
                IsFeatured = true,
                IsLocked = false,
                IsPrivate = false,
                Name = "Channel 3",
                PictureUrl = "/images/todaysPostImg.png",
                Rating = 4,
                Url = "/images/todaysPostImg.png",
                UserId = 1058,
                VideoTubeCount = 12,
                SubscriberCount = 345345,
                ChannelViewsCount = 11123
            });
        }

        /// <summary>
        /// This method will retrieve all channel tubes for a user one page at a time
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="pageIndex">Index of page to be retrieved</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/user/{userName}/channels/page/{pageIndex:int}", Name = "UserChannelsPaged")]
        public IEnumerable<ChannelTubeModel> GetChannelTubesByUserName(String userName, int pageIndex)
        {
            var modelFactory = new ModelFactory(this.Request);
            var channelTubeModels = modelFactory.CreateChannelTubeModels(channels);

            return channelTubeModels;
        }

        /// <summary>
        /// This method will retrieve individual channel tube data by id
        /// </summary>
        /// <param name="channelTubeId">Channel Tube Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/user/channel/{channelTubeId:int}", Name = "UserChannel")]
        public ChannelTubeModel GetChannelTubeById(int channelTubeId)
        {
            var modelFactory = new ModelFactory(this.Request);
            var channelTubeModel = modelFactory.CreateChannelTubeModel(channels[0]);

            return channelTubeModel;
        }
    }
}
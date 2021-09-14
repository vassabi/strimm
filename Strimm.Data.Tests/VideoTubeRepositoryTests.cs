using Microsoft.VisualStudio.TestTools.UnitTesting;
using Strimm.Data.Interfaces;
using Strimm.Data.Repositories;
using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Tests
{
    [TestClass]
    public class VideoTubeRepositoryTests
    {
        private IVideoTubeRepository videoTubeRepository = new VideoTubeRepository();
        private IChannelTubeRepository channelTubeRepository = new ChannelTubeRepository();
        private IVideoRoomTubeRepository videoRoomTubeRepository = new VideoRoomTubeRepository();
        private IUserRepository userRepository = new UserRepository();

        private VideoTube videoTube;
        private ChannelTube channelTube;
        private VideoRoomTube videoRoomTube;
        private UserPo user;

        private List<VideoTubePo> videosInChannelTube;
        private List<VideoTubePo> videosInVideoRoomTube;

        bool isInitialized = false;

        public VideoTubeRepositoryTests()
        {

        }

        //[TestInitialize]
        //public void Setup()
        //{
        //    videoTube = videoTubeRepository.InsertVideoTubeWithGet(
        //        "Test Video Tube",
        //        "Video Tube description",
        //        "Provider Video Id",
        //        20L,
        //        1,
        //        1,
        //        false,
        //        false,
        //        false);


        //    user = userRepository.InsertUserWithGet(
        //            "testUser",
        //            AccountUtils.GenerateAccountNumber(),
        //            "::1",
        //            CryptoUtils.HashPassword(CryptoUtils.GetPasswordWithSalt("info@strimm.com", "password")),
        //            "info@strimm.com",
        //            "Test1",
        //            "Test2",
        //            DateTime.Now.AddYears(-13),
        //            "United States of America",
        //            "Test Board");

        //    videosInChannelTube = null;
        //    videosInVideoRoomTube = null;
        //}

        [TestMethod]
        public void BulkInsertVideoTubeListIntoChannelTubeById()
        {
            channelTube = channelTubeRepository.InsertChannelTubeWithGet(
                                    1,
                                    1,
                                    "Test Channel Name",
                                    "Test Channel Description",
                                    "www.pictureurl.com/test",
                                    "TestChannelName",
                                    user.UserId,
                                    false,
                                    "",
                                    false,
                                    false,
                                    "",
                                    "",
                                    false,
                                    false,
                                    false,
                                    false,
                                    false,
                                   
                                    "",
                                    false);
     
            Assert.IsNotNull(channelTube, "Failed to create channel tube");

            bool isSuccess = videoTubeRepository.BulkInsertVideoTubeListIntoChannelTubeById(channelTube.ChannelTubeId, new List<int>() { videoTube.VideoTubeId },false);

            Assert.IsTrue(isSuccess, String.Format("Failed to insert video tube into channel tube {0}", channelTube.ChannelTubeId));

            videosInChannelTube = videoTubeRepository.GetAllVideoTubeByChannelTubeId(channelTube.ChannelTubeId);

            Assert.IsNotNull(videosInChannelTube, String.Format("Failed to retrieve video tubes in category with Id={0}", channelTube.ChannelTubeId));
            Assert.AreEqual(1, videosInChannelTube.Count, String.Format("Retrieved {0} video tubes when expected 1", videosInChannelTube.Count));
        }

        [TestMethod]
        public void BulkInsertVideoTubeListIntoVideRoomTubeById()
        {
            videoRoomTube = videoRoomTubeRepository.GetVideoRoomTubeByUserId(user.UserId);
            Assert.IsNotNull(videoRoomTube, "Failed to retrieve video room tube");

            bool isSuccess = videoTubeRepository.BulkInsertVideoTubeListIntoVideoRoomTubeById(videoRoomTube.VideoRoomTubeId, new List<int>() { videoTube.VideoTubeId });

            Assert.IsTrue(isSuccess, String.Format("Failed to insert video tube into video room tube {0}", videoRoomTube.VideoRoomTubeId));

            videosInVideoRoomTube = videoTubeRepository.GetAllVideoTubeByVideoRoomTubeId(videoRoomTube.VideoRoomTubeId);

            Assert.IsNotNull(videosInVideoRoomTube, String.Format("Failed to retrieve video tubes in video room tube with Id={0}", videoRoomTube.VideoRoomTubeId));
            Assert.AreEqual(1, videosInVideoRoomTube.Count, String.Format("Retrieved {0} video tubes when expected 1", videosInVideoRoomTube.Count));
        }

        [TestMethod]
        public void GetPublicVideoTubesByPageIndexTest()
        {
            var pageCount = 0;
            var pageSize = 10;

            var firstPageOf10PublicVideos = videoTubeRepository.GetPublicVideoTubesByPageIndex(1, null, out pageCount, pageSize);

            Assert.IsNotNull(firstPageOf10PublicVideos, "Failed to retrieve fist page of public videos");
            Assert.IsTrue(firstPageOf10PublicVideos.Count > 0, "First page of public videos cannot be empty");
            Assert.AreEqual(pageSize, firstPageOf10PublicVideos.Count, String.Format("Invalid number of videos on the page. Expected {0}", pageSize));
        }

        [TestMethod]
        public void AddVideoTubeToChannelScheduleByIdTest()
        {
            var pageCount = 0;
            var pageSize = 10;

            var firstPageOf10PublicVideos = videoTubeRepository.GetPublicVideoTubesByPageIndex(1, null, out pageCount, pageSize);

            Assert.IsNotNull(firstPageOf10PublicVideos, "Failed to retrieve fist page of public videos");
            Assert.IsTrue(firstPageOf10PublicVideos.Count > 0, "First page of public videos cannot be empty");

            var videoSchedules = videoTubeRepository.AddVideoTubeToChannelScheduleById(14, firstPageOf10PublicVideos[0].VideoTubeId);

            Assert.IsNotNull(videoSchedules, "Failed to add video to schedule");
            Assert.AreEqual(firstPageOf10PublicVideos[0].VideoTubeId, videoSchedules[0].VideoTubeId, "Unable to match video ids");

            videoTubeRepository.DeleteVideoTubeFromChannelScheduleById(14, firstPageOf10PublicVideos[0].VideoTubeId);
        }

        [TestCleanup]
        public void TearDown()
        {
            if (videosInChannelTube != null)
            {
                bool isSuccessful = videoTubeRepository.DeleteAllVideoTubesFromChannelTubeByChannelTubeId(channelTube.ChannelTubeId);
                Assert.IsTrue(isSuccessful, "Failed to delete videos from channel.");

                isSuccessful = channelTubeRepository.DeleteChannelTubeById(channelTube.ChannelTubeId);
                Assert.IsTrue(isSuccessful, "Failed to delete channel tube");
            }

            if (videosInVideoRoomTube  != null)
            {
                bool isSuccessful = videoTubeRepository.DeleteAllVideoTubesFromVideoRoomTubeByVideoRoomTubeId(videoRoomTube.VideoRoomTubeId);
                Assert.IsTrue(isSuccessful, "Failed to delete videos from video room");
            }

            bool isSuccess = false;

            if (videoTube != null)
            {
                isSuccess = videoTubeRepository.DeleteVideoTubeById(videoTube.VideoTubeId);

                Assert.IsTrue(isSuccess, String.Format("Failed to delete videoTube with Id={0}", videoTube.VideoTubeId));
            }

            if (user != null)
            {
                isSuccess = userRepository.DeleteUserById(user.UserId);

                Assert.IsTrue(isSuccess, String.Format("Failed to delete user with Id={0}", user.UserId));
            }
        }
    }
}

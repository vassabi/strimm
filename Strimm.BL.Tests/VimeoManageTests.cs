using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StrimmBL;
using Strimm.Model.WebModel;
using System.Collections.Generic;
using System.Linq;

namespace Strimm.BL.Tests
{
    [TestClass]
    public class VimeoManageTests
    {
        [TestMethod]
        public void GetVideosByKeywordsTest()
        {
            VideoTubePageModel videoPage = VimeoServiceManage.GetVideosByKeywords("history", 1, false, false);

            Assert.IsNotNull(videoPage);
        }

        [TestMethod]
        public void GetVideoByUrlTest()
        {
            VideoTubeModel video = VimeoServiceManage.GetVideoByUrl("125382531", false);

            Assert.IsNotNull(video);
        }

        [TestMethod]
        public void UpdateVideoStatusTest()
        {
            List<VideoTubeModel> videos = new List<VideoTubeModel>();

            videos.Add(new VideoTubeModel() { Url = "125382531" });
            videos.Add(new VideoTubeModel() { Url = "126060304" });
            videos.Add(new VideoTubeModel() { Url = "126078148" });
            videos.Add(new VideoTubeModel() { Url = "26766999" });
            videos.Add(new VideoTubeModel() { Url = "126106748" });
            videos.Add(new VideoTubeModel() { Url = "126210095" });
            videos.Add(new VideoTubeModel() { Url = "126089225" });
            videos.Add(new VideoTubeModel() { Url = "126360743" });
            videos.Add(new VideoTubeModel() { Url = "121156252" });
            videos.Add(new VideoTubeModel() { Url = "26278283" });
            videos.Add(new VideoTubeModel() { Url = "2696386" });
            videos.Add(new VideoTubeModel() { Url = "43426940" });
            videos.Add(new VideoTubeModel() { Url = "18743950" });
            videos.Add(new VideoTubeModel() { Url = "125382531" });
            videos.Add(new VideoTubeModel() { Url = "126060304" });
            videos.Add(new VideoTubeModel() { Url = "126078148" });
            videos.Add(new VideoTubeModel() { Url = "26766999" });
            videos.Add(new VideoTubeModel() { Url = "126106748" });
            videos.Add(new VideoTubeModel() { Url = "126210095" });
            videos.Add(new VideoTubeModel() { Url = "126089225" });
            videos.Add(new VideoTubeModel() { Url = "126360743" });
            videos.Add(new VideoTubeModel() { Url = "121156252" });
            videos.Add(new VideoTubeModel() { Url = "26278283" });
            videos.Add(new VideoTubeModel() { Url = "2696386" });
            videos.Add(new VideoTubeModel() { Url = "43426940" });
            videos.Add(new VideoTubeModel() { Url = "18743950" });
            videos.Add(new VideoTubeModel() { Url = "125382531" });
            videos.Add(new VideoTubeModel() { Url = "126060304" });
            videos.Add(new VideoTubeModel() { Url = "126078148" });
            videos.Add(new VideoTubeModel() { Url = "26766999" });
            videos.Add(new VideoTubeModel() { Url = "126106748" });
            videos.Add(new VideoTubeModel() { Url = "126210095" });
            videos.Add(new VideoTubeModel() { Url = "126089225" });
            videos.Add(new VideoTubeModel() { Url = "126360743" });
            videos.Add(new VideoTubeModel() { Url = "121156252" });
            videos.Add(new VideoTubeModel() { Url = "26278283" });
            videos.Add(new VideoTubeModel() { Url = "2696386" });
            videos.Add(new VideoTubeModel() { Url = "43426940" });
            videos.Add(new VideoTubeModel() { Url = "18743950" });
            videos.Add(new VideoTubeModel() { Url = "125382531" });
            videos.Add(new VideoTubeModel() { Url = "126060304" });
            videos.Add(new VideoTubeModel() { Url = "126078148" });
            videos.Add(new VideoTubeModel() { Url = "26766999" });
            videos.Add(new VideoTubeModel() { Url = "126106748" });
            videos.Add(new VideoTubeModel() { Url = "126210095" });
            videos.Add(new VideoTubeModel() { Url = "126089225" });
            videos.Add(new VideoTubeModel() { Url = "126360743" });
            videos.Add(new VideoTubeModel() { Url = "121156252" });
            videos.Add(new VideoTubeModel() { Url = "26278283" });
            videos.Add(new VideoTubeModel() { Url = "2696386" });
            videos.Add(new VideoTubeModel() { Url = "00000000" });
            videos.Add(new VideoTubeModel() { Url = "11111111" });

            VimeoServiceManage.UpdateVideoStatus(videos);

            var restricted = videos.Where(x => x.IsRestrictedByProvider).ToList();

            Assert.IsNotNull(restricted);
            Assert.IsTrue(restricted.Count > 0);
            Assert.AreEqual(2, restricted.Count);
        }
    }
}

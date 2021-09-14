using Strimm.Model.WebModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace StrimmBL.HLS
{
    public class HLSVideoService
    {
        private List<VideoScheduleModel> _videoScheduleModels;
        private List<string> _fileContent;

        public HLSVideoService()
        {
            _fileContent = new List<string>();
            _videoScheduleModels = new List<VideoScheduleModel>();
        }

        public string GenerateM3U8Link(string channelName)
        {

            var fileUrl = string.Format("{0}/download/hls/{1}.m3u8/",
                HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority),
                channelName);
            return fileUrl;
        }

        public byte[] GenerateM3U8File(List<VideoScheduleModel> videoTubeModels)
        {
            SetVideoTubeModels(videoTubeModels);
            // fill file data
            FillContentByData();

            return Encoding.ASCII.GetBytes(string.Join("\n", _fileContent));
        }

        private void SetVideoTubeModels(List<VideoScheduleModel> videoTubeModels)
        {
            _videoScheduleModels = videoTubeModels;
        }


        private void FillContentByData()
        {
            // add head of data
            _fileContent.AddRange(new string[] { "#EXTM3U" });

            // add videos
            if (_videoScheduleModels != null && _videoScheduleModels.Count > 0)
            {
                foreach (var videoModel in _videoScheduleModels)
                {
                    // add info of link (logo, group title, description)
                    _fileContent.Add(string.Format("#EXTINF:-1 tvg-logo=\"{0}\" group-title=\"MOVIES\",{1}",
                        videoModel.ThumbnailUrl ?? "",
                        videoModel.Description));

                    // add m3u8 link of video
                    string videoUrl = videoModel.ProviderVideoId.Contains("https:") ? videoModel.ProviderVideoId : "https:" + videoModel.ProviderVideoId;
                    _fileContent.Add(videoUrl);
                }
            }
        }
    }
}
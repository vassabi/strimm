using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL
{
    public class HLSManager
    {
        public string generatedData { get; set; }

        public async Task GenerateHLSLinkForChannel(int ChannelId)
        {
            var schedules = ScheduleManage.GetChannelTubeSchedulesByDate(ChannelId, DateTime.Now);
            foreach(var s in schedules)
            {
                foreach(var x in s.VideoSchedules)
                {
                    string url = x.ProviderVideoId;
                    if (url.EndsWith(".m3u8"))
                        await getHLSDataAsync(url);
                }
            }
        }

        public void GenerateJSONLinkForChannel(int ChannelId)
        {
            var schedules = ScheduleManage.GetChannelTubeSchedulesByDate(ChannelId, DateTime.Now);
            var channel = ChannelManage.GetChannelTubeById(ChannelId);
            dynamic rokuFeedJSON = new ExpandoObject();
            rokuFeedJSON.providerName = channel.Name;
            rokuFeedJSON.lastUpdated = DateTime.Now;
            rokuFeedJSON.language = channel.LanguageId;
            int ind = 0;
            foreach (var s in schedules)
            {
                var hlsVideos = s.VideoSchedules.Where(v => v.ProviderVideoId.EndsWith(".m3u8")).ToList();
                rokuFeedJSON.liveFeeds = new dynamic[hlsVideos.Count()];
                foreach (var x in hlsVideos)
                {
                    rokuFeedJSON.liveFeeds[ind] = new ExpandoObject();
                    rokuFeedJSON.liveFeeds[ind].id = x.VideoTubeId;
                    rokuFeedJSON.liveFeeds[ind].title = x.VideoTubeTitle;
                    rokuFeedJSON.liveFeeds[ind].shortDescription = x.Description;
                    rokuFeedJSON.liveFeeds[ind].longDescription = x.Description;
                    rokuFeedJSON.liveFeeds[ind].thumbnail = x.Thumbnail;
                    rokuFeedJSON.liveFeeds[ind].genres = new dynamic[1];
                    rokuFeedJSON.liveFeeds[ind].genres[0] = "general";
                    rokuFeedJSON.liveFeeds[ind].tags = new dynamic[1];
                    rokuFeedJSON.liveFeeds[ind].tags[0] = "general";
                    rokuFeedJSON.liveFeeds[ind].releaseDate = channel.CreatedDate;
                    rokuFeedJSON.liveFeeds[ind].content = new ExpandoObject();
                    rokuFeedJSON.liveFeeds[ind].content.dateAdded = DateTime.Now;
                    rokuFeedJSON.liveFeeds[ind].content.duration = x.Duration;
                    rokuFeedJSON.liveFeeds[ind].content.videos = new dynamic[1];
                    rokuFeedJSON.liveFeeds[ind].content.videos[0] = new ExpandoObject();
                    rokuFeedJSON.liveFeeds[ind].content.videos[0].url = x.ProviderVideoId;
                    rokuFeedJSON.liveFeeds[ind].content.videos[0].quality = "HD";
                    rokuFeedJSON.liveFeeds[ind].content.videos[0].videoType = "M3U8";
                    rokuFeedJSON.liveFeeds[ind].content.videos[0].bitrate = 1000;
                    ind++;
                }
            }

            generatedData = Newtonsoft.Json.JsonConvert.SerializeObject(rokuFeedJSON);
        }

        private async Task getHLSDataAsync(string url)
        {
            if (!url.StartsWith("http"))
                url = "http:" + url;
            string url1Split = url.Substring(0, url.LastIndexOf("/") + 1);
            //Creaating HTTP Request Url
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = "GET"; // or "POST", "PUT", "PATCH", "DELETE", etc.
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                var encoding = ASCIIEncoding.ASCII;
                using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                {
                    string responseText = reader.ReadToEnd();
                    //System.Diagnostics.Debug.WriteLine(responseText);
                    string[] splitData = responseText.Split('\n');
                    System.Diagnostics.Debug.WriteLine(splitData.Length);
                    string stringUrl = "";

                    for (var i = 0; i < splitData.Length; i++)
                    {
                        System.Diagnostics.Debug.WriteLine(splitData[i]);
                        //System.Diagnostics.Debug.WriteLine(splitData[i].Contains("m3u8"));
                        if (splitData[i].Contains("m3u8"))
                        {
                            if (splitData[i].Contains("http"))
                            {
                                stringUrl = splitData[i];
                            }
                            else
                            {
                                stringUrl = url1Split + splitData[i];
                            }
                        }
                    }


                    string url11Split = stringUrl.Substring(0, stringUrl.LastIndexOf("/") + 1);
                    using (var client = new HttpClient())
                    {
                        var result = await client.GetAsync(stringUrl);
                        string subTextValue = result.Content.ReadAsStringAsync().Result;
                        string[] subTextSlitArray = subTextValue.Split('\n');
                        List<string> list = new List<string>();

                        int counter = -1;

                        for (var ii = 0; ii < subTextSlitArray.Length; ii++)
                        {
                            //console.log(splitData[i]);
                            if (subTextSlitArray[ii].Contains(".ts") || subTextSlitArray[ii].Contains(".aac") || subTextSlitArray[ii].Contains(".m4s"))
                            {
                                counter = counter + 1;
                                if (subTextSlitArray[ii].Contains("http"))
                                {
                                    list.Add(subTextSlitArray[ii]);
                                }
                                else
                                {

                                    list.Add(url11Split + subTextSlitArray[ii]);
                                }
                            }
                        }
                        for (var j = 0; j < list.Count; j++)
                        {
                            generatedData += "\n#EXTINF:10.0,\n" + list[j];
                        }
                        list.Clear();
                    }
                }
            }
        }
    }
}

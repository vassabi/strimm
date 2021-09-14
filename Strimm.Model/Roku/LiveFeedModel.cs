namespace Strimm.Model.Roku
{
    public class LiveFeedModel : BaseVideoType
    {
        /// <summary>
        /// Required
        /// The URL of the secondary thumbnail for the live stream. 
        /// This is used as a backup in the event that the primary image is not suitable. 
        /// Image dimensions must be at least 800x450 (width x height, 16x9 aspect ratio).
        /// </summary>
        public string brandedThumbnail { get; set; }  // https://example.org/cdn/thumbnails/1509428502952/1",



        //public object language { get; set; }  // {  "en"  },
        //public string[] tags { get; set; }  // [ "linear", "news", "celebrity" ]
    }
}

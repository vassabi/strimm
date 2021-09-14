using System;
using System.Collections.Generic;

namespace Strimm.Model.Roku
{
    public class RokuFeedModel
    {
        /// <summary>
        /// The name of the feed provider. E.g.: “Acme Productions”. <br />
        /// <br />
        /// Required
        /// </summary>
        public string providerName { get; set; }

        /// <summary>
        /// The language the channel uses for all its information and descriptions. <br />
        /// (e.g., “en”, “en-US”, “es”, etc.). ISO 639 alpha-2 or alpha-3 language code string. <br />
        /// <br />
        /// Required
        /// </summary>
        public string language { get; set; }

        /// <summary>
        /// The date that the feed was last modified in ISO 8601 <br />
        /// format: {YYYY}-{MM}-{DD}T{hh}:{mm}:{ss}+{TZ}. <br />
        /// For example, 2020-11-11T22:21:37+00:00 <br />
        /// <br />
        /// Required
        /// </summary>
        public string lastUpdated { get; set; }

        /// <summary>
        /// A list of one or more short-form videos. <br />
        /// Short-form videos are usually less than 20 minutes long and are not TV Shows or Movies. <br />
        /// <br />
        /// Required*
        /// </summary>
        public List<ShortFormVideoModel> shortFormVideos { get; set; }

        /// <summary>
        /// A list of one or more playlists. <br />
        /// They are useful for creating manually ordered categories inside your channel. <br />
        /// <br />
        /// Optional
        /// </summary>
        public List<PlaylistModel> playlists { get; set; }

        /// <summary>
        /// An ordered list of one or more categories that will show up in your Roku Channel. <br />
        /// Categories may also be manually specified within Direct Publisher <br />
        /// if you do not want to provide them directly in the feed. <br />
        /// Each time the feed is updated it will refresh the categories. <br />
        /// <br />
        /// Optional
        /// </summary>
        public List<FeedCategoryModel> categories { get; set; }

        /// <summary>
        /// A list of one or more movies. <br />
        /// <br />
        /// Required*
        /// </summary>
        public List<MoviesModel> movies { get; set; }

        /// <summary>
        /// A list of one or more live linear streams. <br />
        /// <br />
        /// Required*
        /// </summary>
        public List<LiveFeedModel> liveFeeds { get; set; }

        public RokuFeedModel()
        {
            language = "en-US";
            lastUpdated = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss+zz");
            shortFormVideos = new List<ShortFormVideoModel>();
            playlists = new List<PlaylistModel>();
            categories = new List<FeedCategoryModel>();
            movies = new List<MoviesModel>();
            liveFeeds = new List<LiveFeedModel>();
        }
    }
}

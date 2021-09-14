using System;
using System.Collections.Generic;

namespace Strimm.Model.Roku
{
    public class VideoContentModel
    {
        /// <summary>
        /// Required 
        /// The date the video was added to the library in the ISO 8601 format: {YYYY}-{MM}-{DD}T{hh}:{mm}:{ss}+{TZ}. 
        /// For example, 2020-11-11T22:21:37+00:00.
        /// 
        /// This information is used to generate the “Recently Added” category.
        /// </summary>
        public string dateAdded { get; set; }

        /// <summary>
        /// Required
        /// Supported formats are described in Closed Caption Support.
        /// </summary>
        public List<VideoContentCaptionModel> captions { get; set; }

        /// <summary>
        /// Required
        /// Runtime in seconds.
        /// </summary>
        public long duration { get; set; }

        /// <summary>
        /// Required
        /// One or more time codes. 
        /// Represents a time duration from the beginning of the video where an ad shows up. 
        /// Conforms to the format: {hh}:{mm}:{ss} and in the form of an SCTE-35 marker. 
        /// See each content type for its ad policy.
        /// </summary>
        public List<TimeSpan> adBreaks { get; set; }    //["00:00:00", "00:03:30"],

        /// <summary>
        /// Required
        /// One or more video files. 
        /// For non-adaptive streams, the same video may be specified with different qualities 
        /// so the Roku player can choose the best one based on bandwidth.
        /// </summary>
        public List<VideoContentVideoModel> videos { get; set; }

        /// <summary>
        /// Required
        /// The language in which the video was originally produced (e.g., “en”, “en-US”, “es”, etc). 
        /// ISO 639 alpha-2 or alpha-3 language code string.
        /// </summary>
        public string language { get; set; }    // en-us

        public VideoContentModel()
        {
            captions = new List<VideoContentCaptionModel>();
            adBreaks = new List<TimeSpan>();
            videos = new List<VideoContentVideoModel>();
        }
    }
}

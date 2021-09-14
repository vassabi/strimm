using System;
using System.Collections.Generic;

namespace Strimm.Model.Roku
{
    public abstract class BaseVideoType
    {
        /// <summary>
        /// Required
        /// An immutable string reference ID for the {liveFeed|movies|shortFormVideo} that does not exceed 50 characters. 
        /// This should serve as a unique identifier for the live feed across different locales.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// Required
        /// The title of the {liveFeed|movies|shortFormVideo} in plain text. 
        /// This field is used for matching in Roku Search. 
        /// Do not include extra information such as year, version label, and so on.
        /// </summary>
        public string title { get; set; }
        
        /// <summary>
        /// Required
        /// A {liveFeed|movies|shortFormVideo} description that does not exceed 200 characters. 
        /// The text will be clipped if longer.
        /// </summary>
        public string shortDescription { get; set; }
        
        /// <summary>
        /// Required
        /// A longer {liveFeed|movies|shortFormVideo} description that does not exceed 500 characters. 
        /// The text will be clipped if longer. 
        /// Must be different from shortDescription.
        /// </summary>
        public string longDescription { get; set; }

        // Thumbnail must be 16x9 aspect ratio, at least 800w x 450h, and either in JPG or PNG format.
        /// <summary>
        /// Required
        /// The URL of the primary thumbnail for the live stream. 
        /// This is used within the channel and in search results. 
        /// Image dimensions must be at least 800x450 (width x height, 16x9 aspect ratio).
        /// </summary>
        public string thumbnail { get; set; }   // https://blog.roku.com/developer/files/2016/10/twitch-poster-artwork.png"

        /// <summary>
        /// Optional
        /// The genre(s) of the content. Must be one of the values listed in Genres.
        /// </summary>
        public List<string> genres { get; set; }    // ["entertainment"]

        /// <summary>
        /// Optional
        /// One or more tags (for example, “dramas”, “korean”, and so on). 
        /// Each tag is a string and is limited to 20 characters. 
        /// Tags are used to define what content will be shown within a category and to find content for content curation purposes.
        /// </summary>
        public List<string> tags { get; set; } // ["broadcasts", "live", "technology"]

        /// <summary>
        /// Required
        /// The actual video content, such as the URL of the live stream.
        /// </summary>
        public VideoContentModel content { get; set; }
    }
}


#region Genres
/*
 * The following genres are supported:
 * 
 *  action
 *  adventure
 *  animals
 *  animated
 *  anime
 *  children
 *  comedy
 *  crime
 *  documentary
 *  drama
 *  educational
 *  fantasy
 *  faith
 *  food
 *  fashion
 *  gaming
 *  health
 *  history
 *  horror
 *  miniseries
 *  mystery
 *  nature
 *  news
 *  reality
 *  romance
 *  science
 *  science fiction
 *  sitcom
 *  special
 *  sports
 *  thriller
 *  technology
 */
#endregion
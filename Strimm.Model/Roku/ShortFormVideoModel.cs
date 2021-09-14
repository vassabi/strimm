using System;
using System.Collections.Generic;

namespace Strimm.Model.Roku
{
    public class ShortFormVideoModel : BaseVideoType
	{
        /// <summary>
        /// Required
        /// The date the video first became available. 
        /// This field is used to sort programs chronologically and group related content in Roku Search. 
        /// Conforms to ISO 8601 format: {YYYY}-{MM}-{DD}. For example, 2020-11-11
        /// </summary>
        public string releaseDate { get; set; }

        public ShortFormVideoModel()
        {
			genres = new List<string>();
			tags = new List<string>();
        }
	}
}

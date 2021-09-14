using System;
using System.Collections.Generic;

namespace Strimm.Model.Roku
{
    public class MoviesModel : BaseVideoType
    {
        /// <summary>
        /// The date the movie was initially released or first aired. <br />
        /// This field is used to sort programs chronologically and group related content in Roku Search. <br />
        /// Conforms to the ISO 8601 format: {YYYY}-{MM}-{DD}. <br />
        /// For example, 2020-11-11 <br />
        /// <br />
        /// Required
        /// </summary>
        public string releaseDate { get; set; }       // 2019-06-11"

        public MoviesModel()
        {
            genres = new List<string>();
            tags = new List<string>();
        }
    }
}

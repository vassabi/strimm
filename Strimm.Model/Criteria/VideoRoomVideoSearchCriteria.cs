using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Criteria
{
    public class VideoRoomVideoSearchCriteria : VideoSearchCriteria
    {
        public bool RetrieveMyVideos { get; set; }
        public bool RetrieveLicensedVideos { get; set; }
        public bool RetrieveExternalVideos { get; set; }
    }
}

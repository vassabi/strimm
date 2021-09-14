using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Criteria
{
    public class VideoStoreVideoSearchCriteria
    {
        public int PageIndex { get; set; }
        public int CategoryId { get; set; }
        public string OwnerUsernameKeyword { get; set; }
        public string VideoContentKeywords { get; set; }
    }
}

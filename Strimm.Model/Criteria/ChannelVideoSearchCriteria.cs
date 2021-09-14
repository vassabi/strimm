using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Criteria
{
    public class ChannelVideoSearchCriteria
    {
        public int PageIndex { get; set; }
        public int CategoryId { get; set; }
        public int ChannelTubeId { get; set; }
        public string Keywords { get; set; }
    }
}

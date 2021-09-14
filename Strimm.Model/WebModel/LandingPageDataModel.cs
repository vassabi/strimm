using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.WebModel
{
    public class LandingPageDataModel : BaseModel
    {
        private List<ChannelTubeModel> channelGroup;
        private string groupName;

        public string GroupName
        {
            get
            {
                return this.groupName;
            }
            set
            {
                this.groupName = value;
            }
        }
        
        public List<ChannelTubeModel> ChannelGroup
        {
            get
            {
                return this.channelGroup;
            }
            set
            {
                this.channelGroup = value;
            }
        }
    }
}

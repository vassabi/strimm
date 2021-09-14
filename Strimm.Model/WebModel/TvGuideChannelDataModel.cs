using Strimm.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.WebModel
{
    [DataContract]
    public class TvGuideChannelDataModel : BaseModel
    {
        private ChannelTubeModel channel;

        private IList<VideoScheduleModel> videoSchedules;

        private UserPo user;

        public TvGuideChannelDataModel()
        {
            videoSchedules = new List<VideoScheduleModel>();
        }

        [DataMember]
        public ChannelTubeModel Channel
        {
            get
            {
                return this.channel;
            }
            set
            {
                this.channel = value;
            }
        }

        [DataMember]
        public IList<VideoScheduleModel> VideoSchedules
        {
            get
            {
                return videoSchedules;
            }
            set
            {
                this.videoSchedules = value;
            }
        }

        [DataMember]
        public UserPo User
        {
            get
            {
                return this.user;
            }
            set
            {
                this.user = value;
            }
        }
    }
}

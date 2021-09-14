using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.WebModel
{
    [DataContract]
    public class TvGuideDataModel : BaseModel
    {
        private IList<TvGuideChannelDataModel> activeChannels;
        private int pageIndex;
        private int pageSize;
        private int pageCount;

        public TvGuideDataModel()
        {
            this.activeChannels = new List<TvGuideChannelDataModel>();
        }

        [DataMember]
        public IList<TvGuideChannelDataModel> ActiveChannels
        {
            get
            {
                return this.activeChannels;
            }
        }

        [DataMember]
        public int PageIndex
        {
            get
            {
                return this.pageIndex;
            }
            set
            {
                this.pageIndex = value;
            }
        }

        [DataMember]
        public int PageSize
        {
            get
            {
                return this.pageSize;
            }
            set
            {
                this.pageSize = value;
            }
        }

        [DataMember]
        public int PageCount
        {
            get
            {
                return this.pageCount;
            }
            set
            {
                this.pageCount = value;
            }
        }
    }
}

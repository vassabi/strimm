using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Projections
{
    [DataContract]
    public class VideoTubePoPage
    {
        private int pageIndex;
        private int prevPageIndex;
        private int nextPageIndex;
        private int pageSize;
        private int pageCount;
        private List<VideoTubePo> videos;

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
        public int PrevPageIndex
        {
            get
            {
                return this.prevPageIndex;
            }
            set
            {
                this.prevPageIndex = value;
            }
        }

        [DataMember]
        public int NextPageIndex
        {
            get
            {
                return this.nextPageIndex;
            }
            set
            {
                this.nextPageIndex = value;
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

        [DataMember]
        public List<VideoTubePo> Videos
        {
            get
            {
                return this.videos;
            }
            set
            {
                this.videos = value;
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    [DataContract]
    public class VideoProviderComment : ModelBase
    {
        private int videoProviderCommentId;
        private int videoProviderId;
        private int adminUserId;
        private string comment;
        private DateTime createDateTime;

        [DataMember]
        public int VideoProviderCommentId
        {
            get
            {
                return this.videoProviderCommentId;
            }
            set
            {
                this.videoProviderCommentId = value;
            }
        }

        [DataMember]
        public int VideoProviderId
        {
            get
            {
                return this.videoProviderId;
            }
            set
            {
                this.videoProviderId = value;
            }
        }

        [DataMember]
        public int AdminUserId
        {
            get
            {
                return this.adminUserId;
            }
            set
            {
                this.adminUserId = value;
            }
        }

        [DataMember]
        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                this.comment = value;
            }
        }
    }
}

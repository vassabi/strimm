using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Projections
{
    [DataContract]
    public class CustomVideoTubeUploadPo : VideoTube
    {
        private string categoryName;
        private bool isScheduled;
        private int useCounter;
        private int viewCounter;
        private int ownerUserId;
        private string ownerUserName;
        private string videoPreviewKey;
        private string firstThumbnailKey;
        private string secondThumbnailKey;
        private string thirdThumbnailKey;
        private string activeThumbnailKey;
        private double rentalFee;

        [DataMember]
        public String CategoryName
        {
            get
            {
                return this.categoryName;
            }
            set
            {
                this.categoryName = value;
            }
        }

        [DataMember]
        public bool IsScheduled
        {
            get
            {
                return this.isScheduled;
            }
            set
            {
                this.isScheduled = value;
            }
        }

        [DataMember]
        public int UseCounter
        {
            get
            {
                return this.useCounter;
            }
            set
            {
                this.useCounter = value;
            }
        }

        [DataMember]
        public int ViewCounter
        {
            get
            {
                return this.viewCounter;
            }
            set
            {
                this.viewCounter = value;
            }
        }

        [DataMember]
        public int OwnerUserId
        {
            get
            {
                return this.ownerUserId;
            }
            set
            {
                this.ownerUserId = value;
            }
        }

        [DataMember]
        public String OwnerUserName
        {
            get
            {
                return this.ownerUserName;
            }
            set
            {
                this.ownerUserName = value;
            }
        }

        [DataMember]
        public String VideoPreviewKey
        {
            get
            {
                return this.videoPreviewKey;
            }
            set
            {
                this.videoPreviewKey = value;
            }
        }

        [DataMember]
        public String FirstThumbnailKey
        {
            get
            {
                return this.firstThumbnailKey;
            }
            set
            {
                this.firstThumbnailKey = value;
            }
        }

        [DataMember]
        public String SecondThumbnailKey
        {
            get
            {
                return this.secondThumbnailKey;
            }
            set
            {
                this.secondThumbnailKey = value;
            }
        }

        [DataMember]
        public String ThirdThumbnailKey
        {
            get
            {
                return this.thirdThumbnailKey;
            }
            set
            {
                this.thirdThumbnailKey = value;
            }
        }

        [DataMember]
        public String ActiveThumbnailKey
        {
            get
            {
                return this.activeThumbnailKey;
            }
            set
            {
                this.activeThumbnailKey = value;
            }
        }

        [DataMember]
        public double RentalFee
        {
            get
            {
                return this.rentalFee;
            }
            set
            {
                this.rentalFee = value;
            }
        }
    }
}

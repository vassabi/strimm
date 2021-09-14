using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    [DataContract]
    public class VideoTubeRentalDetail
    {
        private int videoTubeRentalDetailId;
        private int videoTubeId;
        private double? rentalFee;
        private DateTime createdDateTime;
        private DateTime? modifiedDateTime;

        [DataMember]
        public int VideoTubeRentalDetailId
        {
            get
            {
                return this.videoTubeRentalDetailId;
            }
            set
            {
                this.videoTubeRentalDetailId = value;
            }
        }

        [DataMember]
        public int VideoTubeId
        {
            get
            {
                return this.videoTubeId;
            }
            set
            {
                this.videoTubeId = value;
            }
        }

        [DataMember]
        public double? RentalFee
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

        [DataMember]
        public DateTime CreatedDateTime
        {
            get
            {
                return this.createdDateTime;
            }
            set
            {
                this.createdDateTime = value;
            }
        }

        [DataMember]
        public DateTime? ModifiedDateTime
        {
            get
            {
                return this.modifiedDateTime;
            }
            set
            {
                this.modifiedDateTime = value;
            }
        }

    }
}

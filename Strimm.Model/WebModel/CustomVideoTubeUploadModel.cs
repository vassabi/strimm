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
    public class CustomVideoTubeUploadModel : BaseModel
    {
        private int videoTubeId;
        private String title;
        private String description;
        private String keywords;
        private long duration;
        private int categoryId;
        private bool isRRated;
        private bool isRemovedByProvider;
        private bool isRestrictedByProvider;
        private bool isInPublicLibrary;
        private bool isPrivate;
        private String videoStatus;
        private String videoStatusMessage;
        private String videoKey;
        private string categoryName;
        private bool isScheduled;
        private int useCounter;
        private int viewCounter;
        private int ownerUserId;
        private string ownerUserName;
        private string videoPreviewKey;
        private long videoPreviewDuration;
        private string firstThumbnailKey;
        private string secondThumbnailKey;
        private string thirdThumbnailKey;
        private string activeThumbnailKey;
        private double rentalFee;
        private string s3Domain;

        public CustomVideoTubeUploadModel()
        {

        }

        public CustomVideoTubeUploadModel(string s3Domain, CustomVideoTubeUploadPo model)
        {
            this.S3Domain = s3Domain;

            if (model != null)
            {
                this.FirstThumbnailKey = model.FirstThumbnailKey;
                this.SecondThumbnailKey = model.SecondThumbnailKey;
                this.ThirdThumbnailKey = model.ThirdThumbnailKey;
                this.Title = model.Title;
                this.Url = model.Uri;
                this.UseCounter = model.UseCounter;
                this.VideoPreviewKey = model.VideoPreviewKey;
                this.VideoKey = model.VideoKey;
                this.VideoStatus = model.VideoStatus;
                this.VideoTubeId = model.VideoTubeId;
                this.ViewCounter = model.ViewCounter;
                this.ActiveThumbnailKey = model.ActiveThumbnailKey;
                this.CategoryName = model.CategoryName;
                this.CategoryId = model.CategoryId;
                this.Description = model.Description;
                this.IsPrivate = model.IsPrivate;
                this.IsRemovedByProvider = model.IsRemovedByProvider;
                this.IsRestrictedByProvider = model.IsRestrictedByProvider;
                this.IsRRated = model.IsRRated;
                this.IsScheduled = model.IsScheduled;
                this.Keywords = model.Keywords;
                this.OwnerUserId = model.OwnerUserId;
                this.OwnerUserName = model.OwnerUserName;
                this.RentalFee = model.RentalFee;
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
        public String Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }


        [DataMember]
        public String Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

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
        public String VideoKey
        {
            get
            {
                return this.videoKey;
            }
            set
            {
                this.videoKey = value;
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
        public String S3Domain
        {
            get
            {
                return this.s3Domain;
            }
            set
            {
                this.s3Domain = value;
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

        [DataMember]
        public String VideoStatus
        {
            get
            {
                return this.videoStatus;
            }
            set
            {
                this.videoStatus = value;
            }
        }

        [DataMember]
        public int CategoryId
        {
            get
            {
                return this.categoryId;
            }
            set
            {
                this.categoryId = value;
            }
        }

        [DataMember]
        public bool IsRemovedByProvider
        {
            get
            {
                return this.isRemovedByProvider;
            }
            set
            {
                this.isRemovedByProvider = value;
            }
        }

        [DataMember]
        public bool IsRestrictedByProvider
        {
            get
            {
                return this.isRestrictedByProvider;
            }
            set
            {
                this.isRestrictedByProvider = value;
            }
        }

        [DataMember]
        public bool IsRRated
        {
            get
            {
                return this.isRRated;
            }
            set
            {
                this.isRRated = value;
            }
        }

        [DataMember]
        public bool IsInPublicLibrary
        {
            get
            {
                return this.isInPublicLibrary;
            }
            set
            {
                this.isInPublicLibrary = value;
            }
        }

        [DataMember]
        public bool IsPrivate
        {
            get
            {
                return this.isPrivate;
            }
            set
            {
                this.isPrivate = value;
            }
        }

        [DataMember]
        public String Keywords
        {
            get
            {
                return this.keywords;
            }
            set
            {
                this.keywords = value;
            }
        }
    }
}

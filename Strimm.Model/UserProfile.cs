using System;
using System.Runtime.Serialization;

namespace Strimm.Model
{
    [DataContract]
    public class UserProfile : ModelBase
    {
        private int userProfileId;
        private int userId;
        private String firstName;
        private String lastName;
        private DateTime birthdate;
        private String address;
        private String city;
        private String stateOrProvince;
        private String zipCode;
        private String country;
        private String gender;
        private String userStory;
        private String company;
        private DateTime termsAndConditionsAcceptanceDate;
        private String profileImageUrl;
        private String phoneNumber;
        private String userIp;
        private String boardName;
        private String backgroundImageUrl;
        private bool privateVideoModeEnabled;
        private bool matureContentAllowed;
        private string stripeCustomerId;

        [DataMember]
        public int UserProfileId
        {
            get
            {
                return this.userProfileId;
            }
            set
            {
                this.userProfileId = value;
            }
        }

        [DataMember]
        public int UserId
        {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = value;
            }
        }

        [DataMember]
        public String FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                this.firstName = value;
            }
        }

        [DataMember]
        public String LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                this.lastName = value;
            }
        }

        [DataMember]
        public DateTime BirthDate
        {
            get
            {
                return this.birthdate;
            }
            set
            {
                this.birthdate = value;
            }
        }

        [DataMember]
        public String Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
            }
        }

        [DataMember]
        public String City
        {
            get
            {
                return this.city;
            }
            set
            {
                this.city = value;
            }
        }

        [DataMember]
        public String StateOrProvince
        {
            get
            {
                return this.stateOrProvince;
            }
            set
            {
                this.stateOrProvince = value;
            }
        }

        [DataMember]
        public String ZipCode
        {
            get
            {
                return this.zipCode;
            }
            set
            {
                this.zipCode = value;
            }
        }

        [DataMember]
        public String Country
        {
            get
            {
                return this.country;
            }
            set
            {
                this.country = value;
            }
        }

        [DataMember]
        public String Gender
        {
            get
            {
                return this.gender;
            }
            set
            {
                this.gender= value;
            }
        }

        [DataMember]
        public String UserStory
        {
            get
            {
                return this.userStory;
            }
            set
            {
                this.userStory = value;
            }
        }

        [DataMember]
        public String Company
        {
            get
            {
                return this.company;
            }
            set
            {
                this.company = value;
            }
        }

        [DataMember]
        public DateTime TermsAndConditionsAcceptanceDate
        {
            get
            {
                return this.termsAndConditionsAcceptanceDate;
            }
            set
            {
                this.termsAndConditionsAcceptanceDate = value;
            }
        }

        [DataMember]
        public String @ProfileImageUrl
        {
            get
            {
                return this.profileImageUrl;
            }
            set
            {
                this.profileImageUrl = value;
            }
        }

        [DataMember]
        public String PhoneNumber
        {
            get
            {
                return this.phoneNumber;
            }
            set
            {
                this.phoneNumber = value;
            }
        }

        [DataMember]
        public string UserIp
        {
            get
            {
                return this.userIp;
            }
            set
            {
                this.userIp = value;
            }
        }

        [DataMember]
        public string BoardName
        {
            get
            {
                return this.boardName;
            }
            set
            {
                this.boardName = value;
            }
        }

        [DataMember]
        public bool PrivateVideoModeEnabled
        {
            get
            {
                return this.privateVideoModeEnabled;
            }
            set
            {
                this.privateVideoModeEnabled = value;
            }
        }

        [DataMember]
        public string BackgroundImageUrl
        {
            get
            {
                return this.backgroundImageUrl;
            }
            set
            {
                this.backgroundImageUrl = value;
            }
        }

        [DataMember]
        public bool MatureContentAllowed
        {
            get
            {
                return this.matureContentAllowed;
            }
            set
            {
                this.matureContentAllowed = value;
            }
        }

        [DataMember]
        public string StripeCustomerId
        {
            get { return this.stripeCustomerId; }
            set { this.stripeCustomerId = value; }
        }
    }
}

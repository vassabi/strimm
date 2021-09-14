using System;
using System.Runtime.Serialization;
using Strimm.Model.Projections;
using Dapper.Contrib.Extensions;
using Strimm.Shared;

namespace Strimm.Model.Projections
{
    [DataContract]
    public class UserPo : User
    {
        private int userId;
        private string userName;
        private DateTime lastUpdateDate;
        private Guid externalUserId;
        private string accountNumber;
        private bool isDeleted;
        private int userMembershipId;
        private string password;
        private string email;
        private string recoveryEmail;
        private bool isLockedOut;
        private DateTime activationEmailSEndDate;
        private int activationEmailRetryCount;
        private bool optOutFromEmailActivation;
        private DateTime lastLoginDate;
        private DateTime emailActivationOptOutDate;
        private DateTime lastPasswordChangeDate;
        private int failedPasswordAttemptCount;
        private bool emailVerified;
        private bool isTempUser;
        private int userProfileId;
        private string firstName;
        private string lastName;
        private DateTime birthDate;
        private string address;
        private string city;
        private string stateOrProvince;
        private string country;
        private string zipCode;
        private string gender;
        private string userStory;
        private string company;
        private DateTime termsAndConditionsAcceptanceDate;
        private string profileImageUrl;
        private string userIp;
        private string phoneNumber;
        private DateTime userMembershipCreatedDate;
        private DateTime userProfileCreatedDate;
        private int countryId;
        private int stateId;
        private string boardName;
        private string backgroundImageUrl;
        private bool isExternalUser;
        private string publicUrl;
        private bool isAdmin;
        private bool isProEnabled;
        private bool isSubscriber;
        private int? subscriberDomainCount;

        private string signupLocationCountry;
        private string signupLocationZipCode;
        private string signupLocationCity;
        private string signupLocationStateOrProvince;   
        private string lastLocationCountry;
        private string lastLocationZipCode;
        private string lastLocationCity;
        private string lastLocationStateOrProvince;

        private bool isFilmMaker;
        private bool hasInterests;

        private bool hasMatureContent;
        private bool matureContentAllowed;
        private bool privateVideoModeEnabled;
        private string stripeCustomerId;

        [DataMember]
        public string LastLocationStateOrProvince
        {
            get
            {
                return this.lastLocationStateOrProvince;
            }
            set
            {
                this.lastLocationStateOrProvince = value;
            }
        }

      
        [DataMember]
        public string LastLocationCity
        {
            get
            {
                return this.lastLocationCity;
            }
            set
            {
                this.lastLocationCity = value;
            }
        }

        [DataMember]
        public string LastLocationCountry
        {
            get
            {
                return this.lastLocationCountry;
            }
            set
            {
                this.lastLocationCountry = value;
            }
        }


        [DataMember]
        public string LastLocationZipCode
        {
            get
            {
                return this.lastLocationZipCode;
            }
            set
            {
                this.lastLocationZipCode = value;
            }
        }

      
        [DataMember]
        public string SignupLocationStateOrProvince
        {
            get
            {
                return this.signupLocationStateOrProvince;
            }
            set
            {
                this.signupLocationStateOrProvince = value;
            }
        }

        [DataMember]
        public string SignupLocationCity
        {
            get
            {
                return this.signupLocationCity;
            }
            set
            {
                this.signupLocationCity = value;
            }
        }

        [DataMember]
        public string SignupLocationCountry
        {
            get
            {
                return this.signupLocationCountry;
            }
            set
            {
                this.signupLocationCountry = value;
            }
        }

        [DataMember]
        public string SignupLocationZipCode
        {
            get
            {
                return this.signupLocationZipCode;
            }
            set
            {
                this.signupLocationZipCode = value;
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
        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }

        [DataMember]
        public String PublicUrl
        {
            get
            {
                return this.publicUrl;
            }
            set
            {
                this.publicUrl = value;
            }
        }

        [DataMember]
        public int UserMembershipId
        {
            get
            {
                return this.userMembershipId;
            }
            set
            {
                this.userMembershipId = value;
            }
        }

        [DataMember]
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        [DataMember]
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
            }
        }

        [DataMember]
        public string RecoveryEmail
        {
            get
            {
                return this.recoveryEmail;
            }
            set
            {
                this.recoveryEmail = value;
            }
        }

        [DataMember]
        public bool IsLockedOut
        {
            get
            {
                return this.isLockedOut;
            }
            set
            {
                this.isLockedOut = value;
            }
        }

        [DataMember]
        public DateTime ActivationEmailSendDate
        {
            get
            {
                return this.activationEmailSEndDate;
            }
            set
            {
                this.activationEmailSEndDate = value;
            }
        }

        [DataMember]
        public int ActivationEmailRetryCount
        {
            get
            {
                return this.activationEmailRetryCount;
            }
            set
            {
                this.activationEmailRetryCount = value;
            }
        }

        [DataMember]
        public bool OptOutFromEmailActivation
        {
            get
            {
                return this.optOutFromEmailActivation;
            }
            set
            {
                this.optOutFromEmailActivation = value;
            }
        }

        [DataMember]
        public DateTime LastLoginDate
        {
            get
            {
                return this.lastLoginDate;
            }
            set
            {
                this.lastLoginDate = value;
            }
        }

        [DataMember]
        public DateTime EmailActivationOptOutDate
        {
            get
            {
                return this.emailActivationOptOutDate;
            }
            set
            {
                this.emailActivationOptOutDate = value;
            }
        }

        [DataMember]
        public DateTime LastPasswordChangeDate
        {
            get
            {
                return this.lastPasswordChangeDate;
            }
            set
            {
                this.lastPasswordChangeDate = value;
            }
        }

        [DataMember]
        public int FailedPasswordAttemptCount
        {
            get
            {
                return this.failedPasswordAttemptCount;
            }
            set
            {
                this.failedPasswordAttemptCount = value;
            }
        }

        [DataMember]
        public bool EmailVerified
        {
            get
            {
                return this.emailVerified;
            }
            set
            {
                this.emailVerified = value;
            }
        }

        [DataMember]
        public bool IsTempUser
        {
            get
            {
                return this.isTempUser;
            }
            set
            {
                this.isTempUser = value;
            }
        }

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
        public string FirstName
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
        public string LastName
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
                return this.birthDate;
            }
            set
            {
                this.birthDate = value;
            }
        }

        [DataMember]
        public string Address
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
        public string City
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
        public string StateOrProvince
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
        public string Country
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
        public string ZipCode
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
        public string Gender
        {
            get
            {
                return this.gender;
            }
            set
            {
                this.gender = value;
            }
        }

        [DataMember]
        public string UserStory
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
        public string Company
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
        public string ProfileImageUrl
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
        public string PhoneNumber
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
        public DateTime UserMembershipCreatedDate
        {
            get
            {
                return this.userMembershipCreatedDate;
            }
            set
            {
                this.userMembershipCreatedDate = value;
            }
        }

        [DataMember]
        public DateTime UserProfileCreatedDate
        {
            get
            {
                return this.userProfileCreatedDate;
            }
            set
            {
                this.userProfileCreatedDate = value;
            }
        }

        [DataMember]
        public int CountryId
        {
            get
            {
                return this.countryId;
            }
            set
            {
                this.countryId = value;
            }
        }

        [DataMember]
        public int StateId
        {
            get
            {
                return this.stateId;
            }
            set
            {
                this.stateId = value;
            }
        }

        [DataMember]
        public String BoardName
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
        public bool IsExternalUser
        {
            get
            {
                return this.isExternalUser;
            }
            set
            {
                this.isExternalUser = value;
            }
        }

        [DataMember]
        public bool IsAdmin
        {
            get
            {
                return this.isAdmin;
            }
            set
            {
                this.isAdmin = value;
            }
        }

        [DataMember]
        public bool IsProEnabled
        {
            get
            {
                return this.isProEnabled;
            }
            set
            {
                this.isProEnabled = value;
            }
        }

        [DataMember]
        public bool IsSubscriber
        {
            get
            {
                return this.isSubscriber;
            }
            set
            {
                this.isSubscriber = value;
            }
        }

        [DataMember]
        public int? SubscriberDomainCount
        {
            get
            {
                return this.subscriberDomainCount;
            }
            set
            {
                this.subscriberDomainCount = value;
            }
        }

        [DataMember]
        public bool IsFilmMaker
        {
            get
            {
                return this.isFilmMaker;
            }
            set
            {
                this.isFilmMaker = value;
            }
        }

        [DataMember]
        public bool HasInterests
        {
            get
            {
                return this.hasInterests;
            }
            set
            {
                this.hasInterests = value;
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
        public bool HasMatureContent
        {
            get { return this.hasMatureContent; }
            set { this.hasMatureContent = value; }
        }

        [DataMember]
        public string StripeCustomerId
        {
            get { return this.stripeCustomerId; }
            set { this.stripeCustomerId = value; }
        }
    }
}

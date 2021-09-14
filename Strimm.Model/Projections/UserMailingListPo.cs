using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Projections
{
    [DataContract]
    public class UserMailingListPo : ModelBase
    {
        private int userId;
        private string firstName;
        private string lastName;
        private string userName;
        private string publicUrl;
        private DateTime? birthday;
        private bool isDeleted;
        private DateTime signupDate;
        private bool? isRegistered;
        private DateTime? registrationDate;
        private int? firstChannelTubeId;
        private string firstChannelName;
        private DateTime? firstChannelTubeCreatdeDate;
        private int? lastChannelTubeId;
        private string lastChannelName;
        private DateTime? lastChannelTubeCreatedDate;
        private bool? missingUserAvatarOrBio;
        private bool? missingChannelAvatar;
        private bool? missingNextDaySchedule;
        private bool? shouldSendChannelTips;
        private bool? hasNewChannelLikes;
        private bool? hasNewFans;
        private DateTime? siteNewsDate;
        private DateTime? siteUpdateDate;
        private DateTime? pressReleaseDate;
        private DateTime? industryNewsDate;
        private bool unsubscribeFromAllEmail;
        private bool unsubscribeFromBirthdayEmail;
        private bool unsubscribeFromAvatarBioReminderEmail;
        private bool unsubscribeFromChannelReminderEmail;
        private bool unsubscribeFromFirstChannelEmail;
        private bool unsubscribeFromNewChannelEmail;
        private bool unsubscribeFromChannelAvatarReminderEmail;
        private bool unsubscribeFromChannelScheduleReminderEmail;
        private bool unsubscribeFromChannelTipsInfoEmail;
        private bool unsubscribeFromChannelLikedNotificationEmail;
        private bool unsubscribeFromNewChannelFanNotificationEmail;
        private bool unsubscribeFromSiteNewsNotificationEmail;
        private bool unsubscribeFromSiteUpdateNotificationEmail;
        private bool unsubscribeFromNewPressReleaseEmail;
        private bool unsubscribeFromIndustryNewsEmail;
        private DateTime? industryNewsAddedDate;
        private string industryNewsDescription;
        private string industryNewsUrl;
        private int? industryNewsId;
        private DateTime? pressReleaseAddedDate;
        private string pressReleaseDescription;
        private string pressReleaseUrl;
        private int? pressReleaseId;
        private string siteUpdateDescription;
        private DateTime? siteUpdateEndDate;
        private DateTime? siteUpdateStartDate;
        private int? siteUpdateId;
        private DateTime? siteNewsAddedDate;
        private string siteNewsDescription;
        private string siteNewsUrl;
        private int? siteNewsId;
        private DateTime? pressReleaseTargetDate;
        private DateTime? siteNewsTargetDate;
        private DateTime? industryNewsTargetDate;
        private string email;
        private DateTime? siteUpdateAddedDate;
        private string mailChimpEmailId;

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
        public bool IsDeleted {
            get
            {
                return this.isDeleted;
            }
            set
            {
                this.isDeleted = value;
            }
        }
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
        public string PublicUrl
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
        public DateTime? BirthDate
        {
            get
            {
                return this.birthday;
            }
            set
            {
                this.birthday = value;
            }
        }       
        public DateTime SignUpDate
        {
            get
            {
                return this.signupDate;
            }
            set
            {
                this.signupDate = value;
            }
        }
        public bool? IsRegistered
        {
            get
            {
                return this.isRegistered;
            }
            set
            {
                this.isRegistered = value;
            }
        }
        public DateTime? RegistrationDate
        {
            get
            {
                return this.registrationDate;
            }
            set
            {
                this.registrationDate = value;
            }
        }
        public int? FirstChannelTubeId
        {
            get
            {
                return this.firstChannelTubeId;
            }
            set
            {
                this.firstChannelTubeId = value;
            }
        }
        public string FirstChannelName
        {
            get
            {
                return this.firstChannelName;
            }
            set
            {
                this.firstChannelName = value;
            }
        }
        public DateTime? FirstChannelTubeCreatedDate
        {
            get
            {
                return this.firstChannelTubeCreatdeDate;
            }
            set
            {
                this.firstChannelTubeCreatdeDate = value;
            }
        }
        public int? LastChannelTubeId
        {
            get
            {
                return this.lastChannelTubeId;
            }
            set
            {
                this.lastChannelTubeId = value;
            }
        }
        public string LastChannelName
        {
            get
            {
                return this.lastChannelName;
            }
            set
            {
                this.lastChannelName = value;
            }
        }
        public DateTime? LastChannelTubeCreatedDate
        {
            get
            {
                return this.lastChannelTubeCreatedDate;
            }
            set
            {
                this.lastChannelTubeCreatedDate = value;
            }
        }
        public bool? MissingUserAvatarOrBio
        {
            get
            {
                return this.missingUserAvatarOrBio;
            }
            set
            {
                this.missingUserAvatarOrBio = value;
            }
        }
        public bool? MissingChannelAvatar
        {
            get
            {
                return this.missingChannelAvatar;
            }
            set
            {
                this.missingChannelAvatar = value;
            }
        }
        public bool? MissingNextDaySchedule
        {
            get
            {
                return this.missingNextDaySchedule;
            }
            set
            {
                this.missingNextDaySchedule = value;
            }
        }
        public bool? ShouldSendChannelTips
        {
            get
            {
                return this.shouldSendChannelTips;
            }
            set
            {
                this.shouldSendChannelTips = value;
            }
        }
        public bool? HasNewChannelLikes
        {
            get
            {
                return this.hasNewChannelLikes;
            }
            set
            {
                this.hasNewChannelLikes = value;
            }
        }
        public bool? HasNewFans
        {
            get
            {
                return this.hasNewFans;
            }
            set
            {
                this.hasNewFans = value;
            }
        }

        public bool UnsubscribeFromAllEmail
        {
            get
            {
                return this.unsubscribeFromAllEmail;
            }
            set
            {
                this.unsubscribeFromAllEmail = value;
            }
        }
        public bool UnsubscribeFromBirthdayEmail
        {
            get
            {
                return this.unsubscribeFromBirthdayEmail;
            }
            set
            {
                this.unsubscribeFromBirthdayEmail = value;
            }
        }
        public bool UnsubscribeFromAvatarBioReminderEmail
        {
            get
            {
                return this.unsubscribeFromAvatarBioReminderEmail;
            }
            set
            {
                this.unsubscribeFromAvatarBioReminderEmail = value;
            }
        }
        public bool UnsubscribeFromChannelReminderEmail
        {
            get
            {
                return this.unsubscribeFromChannelReminderEmail;
            }
            set
            {
                this.unsubscribeFromChannelReminderEmail = value;
            }
        }
        public bool UnsubscribeFromFirstChannelEmail
        {
            get
            {
                return this.unsubscribeFromFirstChannelEmail;
            }
            set
            {
                this.unsubscribeFromFirstChannelEmail = value;
            }
        }
        public bool UnsubscribeFromNewChannelEmail
        {
            get
            {
                return this.unsubscribeFromNewChannelEmail;
            }
            set
            {
                this.unsubscribeFromNewChannelEmail = value;
            }
        }
        public bool UnsubscribeFromChannelAvatarReminderEmail
        {
            get
            {
                return this.unsubscribeFromChannelAvatarReminderEmail;
            }
            set
            {
                this.unsubscribeFromChannelAvatarReminderEmail = value;
            }
        }
        public bool UnsubscribeFromChannelScheduleReminderEmail
        {
            get
            {
                return this.unsubscribeFromChannelScheduleReminderEmail;
            }
            set
            {
                this.unsubscribeFromChannelScheduleReminderEmail = value;
            }
        }
        public bool UnsubscribeFromChannelTipsInfoEmail
        {
            get
            {
                return this.unsubscribeFromChannelTipsInfoEmail;
            }
            set
            {
                this.unsubscribeFromChannelTipsInfoEmail = value;
            }
        }
        public bool UnsubscribeFromChannelLikedNotificationEmail
        {
            get
            {
                return this.unsubscribeFromChannelLikedNotificationEmail;
            }
            set
            {
                this.unsubscribeFromChannelLikedNotificationEmail = value;
            }
        }
        public bool UnsubscribeFromNewChannelFanNotificationEmail
        {
            get
            {
                return this.unsubscribeFromNewChannelFanNotificationEmail;
            }
            set
            {
                this.unsubscribeFromNewChannelFanNotificationEmail = value;
            }
        }
        public bool UnsubscribeFromSiteNewsNotificationEmail
        {
            get
            {
                return this.unsubscribeFromSiteNewsNotificationEmail;
            }
            set
            {
                this.unsubscribeFromSiteNewsNotificationEmail = value;
            }
        }
        public bool UnsubscribeFromSiteUpdateNotificationEmail
        {
            get
            {
                return this.unsubscribeFromSiteUpdateNotificationEmail;
            }
            set
            {
                this.unsubscribeFromSiteUpdateNotificationEmail = value;
            }
        }
        public bool UnsubscribeFromNewPressReleaseEmail
        {
            get
            {
                return this.unsubscribeFromNewPressReleaseEmail;
            }
            set
            {
                this.unsubscribeFromNewPressReleaseEmail = value;
            }
        }
        public bool UnsubscribeFromIndustryNewsEmail
        {
            get
            {
                return this.unsubscribeFromIndustryNewsEmail;
            }
            set
            {
                this.unsubscribeFromIndustryNewsEmail = value;
            }
        }
		public int? SiteNewsId 
        {
            get 
            {
                return this.siteNewsId;
            }
            set 
            {
                this.siteNewsId = value;
            }
        }
		public string SiteNewsUrl 
        {
            get 
            {
                return this.siteNewsUrl;
            }
            set 
            {
                this.siteNewsUrl = value;
            }
        }
		public string SiteNewsDescription 
        {
            get 
            {
                return this.siteNewsDescription;
            }
            set 
            {
                this.siteNewsDescription = value;
            }
        }
        public DateTime? SiteNewsTargetDate
        {
            get
            {
                return this.siteNewsTargetDate;
            }
            set
            {
                this.siteNewsTargetDate = value;
            }
        }
        public DateTime? SiteNewsAddedDate 
        {
            get 
            {
                return this.siteNewsAddedDate;
            }
            set 
            {
                this.siteNewsAddedDate = value;
            }
        }
		public int? SiteUpdateId 
        {
            get 
            {
                return this.siteUpdateId;
            }
            set 
            {
                this.siteUpdateId = value;
            }
        }
		public DateTime? SiteUpdateStartDate 
        {
            get 
            {
                return this.siteUpdateStartDate;
            }
            set 
            {
                this.siteUpdateStartDate = value;
            }
        }
		public DateTime? SiteUpdateEndDate 
        {
            get 
            {
                return this.siteUpdateEndDate;
            }
            set 
            {
                this.siteUpdateEndDate = value;
            }
        }
		public string SiteUpdateDescription 
        {
            get 
            {
                return this.siteUpdateDescription;
            }
            set 
            {
                this.siteUpdateDescription = value;
            }
        }
		public DateTime? SiteUpdateAddedDate 
        {
            get 
            {
                return this.siteUpdateAddedDate;
            }
            set 
            {
                this.siteUpdateAddedDate = value;
            }
        }
		public int? PressReleaseId 
        {
            get 
            {
                return this.pressReleaseId;
            }
            set 
            {
                this.pressReleaseId = value;
            }
        }
		public string PressReleaseUrl 
        {
            get 
            {
                return this.pressReleaseUrl;
            }
            set 
            {
                this.pressReleaseUrl = value;
            }
        }
		public string PressReleaseDescription 
        {
            get 
            {
                return this.pressReleaseDescription;
            }
            set 
            {
                this.pressReleaseDescription = value;
            }
        }
        public DateTime? PressReleaseTargetDate
        {
            get
            {
                return this.pressReleaseTargetDate;
            }
            set
            {
                this.pressReleaseTargetDate = value;
            }
        }
        public DateTime? PressReleaseAddedDate 
        {
            get 
            {
                return this.pressReleaseAddedDate;
            }
            set 
            {
                this.pressReleaseAddedDate = value;
            }
        }
		public int? IndustryNewsId 
        {
            get 
            {
                return this.industryNewsId;
            }
            set 
            {
                this.industryNewsId = value;
            }
        }
		public string IndustryNewsUrl 
        {
            get 
            {
                return this.industryNewsUrl;
            }
            set 
            {
                this.industryNewsUrl = value;
            }
        }
		public string IndustryNewsDescription 
        {
            get 
            {
                return this.industryNewsDescription;
            }
            set 
            {
                this.industryNewsDescription = value;
            }
        }
        public DateTime? IndustryNewsTargetDate
        {
            get
            {
                return this.industryNewsTargetDate;
            }
            set
            {
                this.industryNewsTargetDate = value;
            }
        }
        public DateTime? IndustryNewsAddedDate 
        {
            get 
            {
                return this.industryNewsAddedDate;
            }
            set 
            {
                this.industryNewsAddedDate = value;
            }
        }

        public string MailChimpEmailId
        {
            get
            {
                return this.mailChimpEmailId;
            }
            set
            {
                this.mailChimpEmailId = value;
            }
        }
    }
}

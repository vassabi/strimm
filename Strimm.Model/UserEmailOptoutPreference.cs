using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    [DataContract]
    public class UserEmailOptoutPreference
    {
        private int userEmailOptOutPreferenceId;
        private int userId;
        private bool isCompleteUnsubscrib;
        private bool isBirthdayEmail;
        private bool isAvatarBioReminder;
        private bool isChannelReminder;
        private bool isOnFirstChannel;
        private bool isOnNewChannel;
        private bool isChannelAvatarReminder;
        private bool isChannelScheduleAutoPilotReminder;
        private bool isCreatingGoodChannelTipsInfo;
        private bool isChannelLikedNotification;
        private bool isNewChannelFanNotification;
        private bool isSiteNewsNewChannelNotification;
        private bool isSiteNewsnewSiteUpdateNotification;
        private bool isSiteNewsNewPressRelease;
        private bool isIndustryNews;
        private DateTime asOfDate;

        [DataMember]
        public int UserEmailOptOutPreferenceId
        {
            get
            {
                return this.userEmailOptOutPreferenceId;
            }
            set
            {
                this.userEmailOptOutPreferenceId = value;
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
        public bool IsCompleteUnsubscrib
        {
            get
            {
                return this.isCompleteUnsubscrib;
            }
            set
            {
                this.isCompleteUnsubscrib = value;
            }
        }

        [DataMember]
        public bool IsBirthdayEmail
        {
            get
            {
                return this.isBirthdayEmail;
            }
            set
            {
                this.isBirthdayEmail = value;
            }
        }
        
        [DataMember]
        public bool IsAvatarBioReminder
        {
            get
            {
                return this.isAvatarBioReminder;
            }
            set
            {
                this.isAvatarBioReminder = value;
            }
        }

        [DataMember]
        public bool IsChannelReminder
        {
            get
            {
                return this.isChannelReminder;
            }
            set
            {
                this.isChannelReminder = value;
            }
        }
        
        [DataMember]
        public bool IsOnFirstChannel
        {
            get
            {
                return this.isOnFirstChannel;
            }
            set
            {
                this.isOnFirstChannel = value;
            }
        }

        [DataMember]
        public bool IsOnNewChannel
        {
            get
            {
                return this.isOnNewChannel;
            }
            set
            {
                this.isOnNewChannel = value;
            }
        }

        [DataMember]
        public bool IsChannelAvatarReminder
        {
            get
            {
                return this.isChannelAvatarReminder;
            }
            set
            {
                this.isChannelAvatarReminder = value;
            }
        }

        [DataMember]
        public bool IsChannelScheduleAutoPilotReminder
        {
            get
            {
                return this.isChannelScheduleAutoPilotReminder;
            }
            set
            {
                this.isChannelScheduleAutoPilotReminder = value;
            }
        }

        [DataMember]
        public bool IsCreatingGoodChannelTipsInfo
        {
            get
            {
                return this.isCreatingGoodChannelTipsInfo;
            }
            set
            {
                this.isCreatingGoodChannelTipsInfo = value;
            }
        }

        [DataMember]
        public bool IsChannelLikedNotification
        {
            get
            {
                return this.isChannelLikedNotification;
            }
            set
            {
                this.isChannelLikedNotification = value;
            }
        }

        [DataMember]
        public bool IsNewChannelFanNotification
        {
            get
            {
                return this.isNewChannelFanNotification;
            }
            set
            {
                this.isNewChannelFanNotification = value;
            }
        }

        [DataMember]
        public bool IsSiteNewsNewChannelNotification
        {
            get
            {
                return this.isSiteNewsNewChannelNotification;
            }
            set
            {
                this.isSiteNewsNewChannelNotification = value;
            }
        }
        
        [DataMember]
        public bool IsSiteNewsnewSiteUpdateNotification
        {
            get
            {
                return this.isSiteNewsnewSiteUpdateNotification;
            }
            set
            {
                this.isSiteNewsnewSiteUpdateNotification = value;
            }
        }

        [DataMember]
        public bool IsSiteNewsNewPressRelease
        {
            get
            {
                return this.isSiteNewsNewPressRelease;
            }
            set
            {
                this.isSiteNewsNewPressRelease = value;
            }
        }

        [DataMember]
        public bool IsIndustryNews
        {
            get
            {
                return this.isIndustryNews;
            }
            set
            {
                this.isIndustryNews = value;
            }
        }

        [DataMember]
        public DateTime AsOfDate
        {
            get
            {
                return this.asOfDate;
            }
            set
            {
                this.asOfDate = value;
            }
        }
    }
}

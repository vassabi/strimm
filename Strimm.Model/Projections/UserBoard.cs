using Strimm.Model.WebModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Strimm.Shared;

namespace Strimm.Model.Projections
{
    [DataContract]
    public class UserBoard
    {
        private String boardName;
        private int userId;
        private String userName;
        private String publicUrl;
        private String profileImageUrl;
        private String backgroundImageUrl;
        private int userFollowersCount;
        private int userSubscriptionsCount;
        private int channelCount;
        private int boardVisitorsCount;
        private String userStory;
        private String firstName;
        private String lastName;
        private String address;
        private String city;
        private String zipCode;
        private String stateOrProvince;
        private String country;
        private String company;
        private String gender;

        private List<ChannelTubeModel> myChannels;
        private List<ChannelTubePo> featuredChannels;
        private List<ChannelTubePo> subscribedChannels;

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
        public String UserName
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
        public String ProfileImageUrl
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
        public String BackgroundImageUrl
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
        public int UserFollowersCount
        {
            get
            {
                return this.userFollowersCount;
            }
            set
            {
                this.userFollowersCount = value;
            }
        }

        [DataMember]
        public int UserSubscriptionsCount
        {
            get
            {
                return this.userSubscriptionsCount;
            }
            set
            {
                this.userSubscriptionsCount = value;
            }
        }

        [DataMember]
        public int ChannelCount
        {
            get
            {
                return this.channelCount;
            }
            set
            {
                this.channelCount = value;
            }
        }

        [DataMember]
        public int BoardVisitorsCount
        {
            get
            {
                return this.boardVisitorsCount;
            }
            set
            {
                this.boardVisitorsCount = value;
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
        public String Gender
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
        public List<ChannelTubeModel> MyChannels
        {
            get
            {
                return this.myChannels;
            }
            set
            {
                this.myChannels = value;
            }
        }

        [DataMember]
        public List<ChannelTubePo> FeaturedChannels
        {
            get
            {
                return this.featuredChannels;
            }
            set
            {
                this.featuredChannels = value;
            }
        }

        [DataMember]
        public List<ChannelTubePo> SubscribedChannels
        {
            get
            {
                return this.subscribedChannels;
            }
            set
            {
                this.subscribedChannels = value;
            }
        }
    }
}

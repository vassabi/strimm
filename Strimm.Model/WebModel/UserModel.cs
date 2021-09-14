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
    public class UserModel : BaseModel
    {
        private Guid authorizationToken;
        private int userId;
        private string userName;
        private string accountNumber;
        private bool isDeleted;
        private string email;
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
        private string profileImageUrl;
        private string phoneNumber;
        private string backgroundImageUrl;
        private List<ChannelTubePo> channelList;
        private bool isExternalUser;
        private string publicUrl;
        private bool isAdmin;

       
        [DataMember]
        public List<ChannelTubePo> ChannelList
        {
            get
            {
                return this.channelList;
            }
            set
            {
                this.channelList = value;
            }
        }

        [DataMember]
        public Guid AuthorizationToken
        {
            get
            {
                return this.authorizationToken;
            }
            set
            {
                this.authorizationToken = value;
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

        [DataMember]
        public string AccountNumber
        {
            get
            {
                return this.accountNumber;
            }
            set
            {
                this.accountNumber = value;
            }
        }

        [DataMember]
        public bool IsDeleted
        {
            get
            {
                return this.isDeleted;
            }
            set
            {
                this.isDeleted = value;
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

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.WebModel
{
    [DataContract]
    public class UserAuthModel : BaseModel
    {
        private Guid authorizationToken;
        private int userId;
        private string email;
        private string username;

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
        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
            }
        }
    }
}

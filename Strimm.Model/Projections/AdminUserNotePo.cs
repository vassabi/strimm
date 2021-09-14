using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Projections
{
    [DataContract]
    public class AdminUserNotePo : AdminUserNote
    {
        private string adminUserName;
        private string userName;
       

        [DataMember]
        public string AdminUserName 
        {
            get
            {
                return this.adminUserName;
            }
            set
            {
                this.adminUserName = value;
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
    }
}

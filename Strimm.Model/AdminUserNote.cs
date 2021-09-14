using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    [DataContract]
    public class AdminUserNote : ModelBase
    {
        private int adminNoteId;
        private int adminUserId;
        private int userId;
        private string notes;
        private string action;
       
        [DataMember]
        public int AdminNoteId
        {
            get
            {
                return this.adminNoteId;
            }
            set
            {
                this.adminNoteId = value;
            }
        }

        [DataMember]
        public int AdminUserId
        {
            get
            {
                return this.adminUserId;
            }
            set
            {
                this.adminUserId = value;
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
        public string Notes
        {
            get
            {
                return this.notes;
            }
            set
            {
                this.notes = value;
            }
        }

        [DataMember]
        public string Action
        {
            get
            {
                return this.action;
            }
            set
            {
                this.action = value;
            }
        }
    }
}

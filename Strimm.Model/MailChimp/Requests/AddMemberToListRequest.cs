using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.MailChimp.Requests
{
    public class AddMemberToListRequest
    {
        public string email_address { get; set; }
        public string unique_email_id { get; set; }
        public string email_type { get; set; }
        public string status { get; set; }
        public string status_if_new { get; set; }
        public MergeFields merge_fields { get; set; }
        public MemberInterests interests { get; set; }
        public MemberStats stats { get; set; }
        public string ip_signup { get; set; }
        public string timestamp_signup { get; set; }
        public string ip_opt { get; set; }
        public string timestamp_opt { get; set; }
        public string member_rating { get; set; }
        public string last_changed { get; set; }
        public string language { get; set; }
        public string vip { get; set; }
        public string email_client { get; set; }
        public Location location { get; set; }
        public LastNote last_note { get; set; }
        public string list_id { get; set; }
        public string _links { get; set; }
    }
}

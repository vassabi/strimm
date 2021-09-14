﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.MailChimp
{
    public class Member
    {
        public string id { get; set; }
        public string email_address { get; set; }
        public string unique_email_id { get; set; }
        public string email_type { get; set; }
        public string status { get; set; }
        public MergeFields merge_fields { get; set; }
        public MemberStats stats { get; set; }
        public string ip_signup { get; set; }
        public string timestamp_signup { get; set; }
        public string ip_opt { get; set; }
        public string timestamp_opt { get; set; }
        public int member_rating { get; set; }
        public string last_changed { get; set; }
        public string language { get; set; }
        public bool vip { get; set; }
        public string email_client { get; set; }
        public Location location { get; set; }
        public string list_id { get; set; }
        public Links[] _links { get; set; }
    }
}

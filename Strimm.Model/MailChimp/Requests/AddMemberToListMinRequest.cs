using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.MailChimp.Requests
{
    public class AddMemberToListMinRequest
    {
        public string email_address { get; set; }
        public string status_if_new { get; set; }
        public MergeFields merge_fields { get; set; }
        public string email_type { get; set; }

    }
}

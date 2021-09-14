using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.MailChimp.Responses
{
    public class MailingListsResponse
    {
        public MailingList[] lists { get; set; }
        public Links[] _links { get; set; }
        public int total_items { get; set; }
    }

}

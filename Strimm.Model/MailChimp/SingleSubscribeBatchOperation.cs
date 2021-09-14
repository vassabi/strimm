using Strimm.Model.MailChimp.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.MailChimp
{
    public class SingleSubscribeBatchOperation
    {
        public string method { get; set; }
        public string path { get; set; }
        public AddMemberToListRequest body { get; set; } 
    }
}

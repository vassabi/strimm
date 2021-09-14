using Strimm.Model.MailChimp.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.MailChimp
{
    public class BatchOperation
    {
        public string method { get; set; }
        public string path { get; set; }
        public AddMemberToListMinRequest body { get; set; }
    }
}

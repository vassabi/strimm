using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.MailChimp.Requests
{
    public class SubscriberBatchRequest
    {
        public List<BatchOperation> operations { get; set; }
    }
}

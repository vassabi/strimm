using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.WebModel
{
    public class OrderConfirmation
    {
        public string OrderNumber { get; set; }

        public string PublicUrl { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public string ProductName { get; set; }

        public bool IsSubscription { get; set; }

        public string OrderStatus { get; set; }

        public bool IsAnnual { get; set; }

        public bool TrialAllowed { get; set; }
    }
}

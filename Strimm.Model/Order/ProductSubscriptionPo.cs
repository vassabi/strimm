using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public class ProductSubscriptionPo : ProductSubscription
    {
        public string OrderNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UserId { get; set; }
               
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductSubscriptionStatus { get; set; }

        public decimal AmountPaid { get; set; }

        public int ChannelCount { get; set; }

        public bool IsAnnual { get; set; }

        public bool IsUpgrade { get; set; }

        public string UnSubscrButtonId { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public class OrderSubscriptionProfile
    {
        public int OrderSubscriptionProfileId { get; set; }

        public int OrderId { get; set; }

        public int PayingSubscriberId { get; set; }

        public string SubscriptionPlan { get; set; }

        public string PayPalSubscriptionId { get; set; }

        public string ButtonId { get; set; }

        public decimal TrialPaymentAmount { get; set; }

        public decimal CycleBillingAmount { get; set; }

        public string TrialPeriod { get; set; }

        public string BillingCycle { get; set; }

        public string Currency { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}

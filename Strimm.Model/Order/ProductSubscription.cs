using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public class ProductSubscription
    {
        public int ProductSubscriptionId { get; set; }

        public int OrderId { get; set; }

        public int ProductSubscriptionStatusId { get; set; }

        public DateTime? TrialPeriodStartDate { get; set; }

        public DateTime SubscriptionStartDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public class OrderStatistics
    {
        public int OrderId { get; set; }

        public string SubscriberName { get; set; }

        public string Country { get; set; }

        public string SubscriberEmail { get; set; }

        public string PublicName { get; set; }

        public string OrderNumber { get; set; }

        public string OrderStatus { get; set; }

        public DateTime TrialEndDate { get; set; }

        public string ProductName { get; set; }

        public decimal PricePerMonth { get; set; }

        public int PaymentsCount { get; set; }

        public decimal GrossPaymentAmount { get; set; }

        public DateTime OrderDate { get; set; }
    }
}

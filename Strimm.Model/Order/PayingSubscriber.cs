using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public class PayingSubscriber
    {
        public int PayingSubscriberId { get; set; }

        public int OrderId { get; set; }

        public int IpnMessageId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PayPalSubscriberId { get; set; }

        public string PayPalPayerId { get; set; }

        public string PayPalPayerStatus { get; set; }

        public string PayPalBusinessEmail { get; set; }

        public string PayPalPayerEmail { get; set; }

        public string PayPalReceiverEmail { get; set; }

        public string PayPalResidenceCountry { get; set; }

        public string PayPalSubscriptionDate { get; set; }
    }
}

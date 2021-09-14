using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public class Order
    {
        public int OrderId { get; set; }

        public string OrderNumber { get; set; }

        public int OrderStatusId { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int ChannelCount { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Street { get; set; }

        public string State { get; set; }

        public string CountryCode { get; set; }

        public string Description { get; set; }

        public string CreatedDate { get; set; }

        public DateTime? OrderExpirationDate { get; set; }

        public bool TrialAllowed { get; set; }

        public bool IsAnnual { get; set; }

        public bool IsUpgrade { get; set; }
    }
}

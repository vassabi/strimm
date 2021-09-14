using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public class OrderPo : Order
    {
        public string OrderStatus { get; set; }

        public string UserName { get; set; }

        public string ProductName { get; set; }

        public decimal? Price { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public string TransactionNumber { get; set; }

        public int ProductSubscriptionStatusId { get; set; }
        public string Status { get; set; }

    }
}

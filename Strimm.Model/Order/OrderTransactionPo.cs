using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public class OrderTransactionPo : OrderTransaction
    {
        public string OrderTransactionType { get; set; }

        public string OrderTransactionStatus { get; set; }

        public string TransactionPaymentMethod { get; set; }
    }
}

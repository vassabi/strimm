using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public class OrderTransaction
    {
        public int OrderTransactionId { get; set; }

        public int OrderId { get; set; }

        public int OrderTransactionTypeId { get; set; }

        public int OrderTransactionStatusId { get; set; }

        public int TransactionPaymentMethodId { get; set; }

        public int IpnMessageId { get; set; }

        public string TxnId { get; set; }

        public string PaymentStatus { get; set; }

        public string PaymentDate { get; set; }

        public int PaymentNumber { get; set; }

        public decimal? PaymentGross { get; set; }

        public string McCurrency { get; set; }

        public string PaymentType { get; set; }

        public decimal? PaymentFee { get; set; }

        public string PayerStatus { get; set; }

        public decimal? McFee { get; set; }

        public decimal? McGross { get; set; }

        public string ProtectionEligibility { get; set; }

        public string TransactionSubject { get; set; }
    }
}

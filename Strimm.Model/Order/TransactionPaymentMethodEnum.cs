using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public enum TransactionPaymentMethodEnum
    {
        PayPal_Subscription = 1,
        PayPal_DoDirectPayment = 2,
        PayPal_Credit = 3,
        PayPal_Instant = 4,
        PayPal_eCheck = 5
    }
}

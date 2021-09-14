using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public enum OrderTransactionTypeEnum
    {
        ReoccuringPayment = 1,
        OneTimePurchase = 2,
        Refund = 3,
        Adjustment = 4
    }
}

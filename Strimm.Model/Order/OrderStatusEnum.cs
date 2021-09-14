using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public enum OrderStatusEnum
    {
        New = 1,
        InTrial = 2,
        Active = 3,
        PendingPaymentResolution = 4,
        Canceled = 5,
        Expired = 6
    }
}

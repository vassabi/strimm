using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public enum ProductSubscriptionStatusEnum
    {
        InTrial = 1,
        Active = 2,
        OnHold = 3,
        Canceled = 4,
        Expired = 5,
        Pending=6
    }
}

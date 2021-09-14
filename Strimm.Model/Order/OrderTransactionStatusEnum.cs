using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public enum OrderTransactionStatusEnum
    {
        New = 1,
        Pending = 2,
        Completed = 3,
        Rejected = 4,
        Canceled = 5
    }
}

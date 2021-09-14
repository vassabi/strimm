using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public class OrderCancellation
    {
        public string OrderNumber { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}

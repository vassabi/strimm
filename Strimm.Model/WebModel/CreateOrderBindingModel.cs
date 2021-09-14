using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.WebModel
{
    public class CreateOrderBindingModel
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        public bool IsAnnual { get; set; }

        public bool IsUpgrade { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    public interface IVersion
    {
        DateTime? CreatedDate { get; set; }
    }
}

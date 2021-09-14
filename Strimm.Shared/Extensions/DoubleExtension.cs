using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Shared.Extensions
{
    public static class DoubleExtension
    {
        public static string ToFixed(this double number, uint decimals)
        {
            return number.ToString("N" + decimals);
        }
    }
}

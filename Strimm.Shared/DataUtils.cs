using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Shared
{
    public static class DataUtils
    {
        public static T? GetValueOrNull<T>(this string valueAsString) where T : struct
        {
            if (string.IsNullOrEmpty(valueAsString))
            {
                return null;
            }

            return (T)Convert.ChangeType(valueAsString, typeof(T));
        }
    }
}

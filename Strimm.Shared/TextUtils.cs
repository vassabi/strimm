using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Shared
{
    public static class TextUtils
    {
        public static string ReplaceNonPrintableCharacters(string s)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                byte b = (byte)c;
                if (b < 32)
                    result.Append("");
                else
                    result.Append(c);
            }
            return result.ToString();
        }
    }
}

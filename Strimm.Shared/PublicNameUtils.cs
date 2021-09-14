using RestSharp.Contrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Strimm.Shared
{
    public static class PublicNameUtils
    {
        public static string GetUrl(string publicName)
        {
            string url = String.Empty;

            if (!String.IsNullOrEmpty(publicName))
            {
                url = publicName.Replace(" ", String.Empty).Replace("-", String.Empty).Replace("'", String.Empty).Replace("&", String.Empty);
            }

            return url;
        }

        public static bool IsValidPublicName(string publicName)
        {
            string regex = @"^([a-zA-Z0-9_\-\'\&\ ]+)$";

            return Regex.IsMatch(publicName, regex);
        }

        public static string UrlDecodePublicName(string publicName)
        {
            return !String.IsNullOrEmpty(publicName) ? HttpUtility.UrlDecode(publicName.Replace("%27", "'")) : String.Empty;
        }

        public static string EncodeApostropheInPublicName(string publicName)
        {
            return HttpUtility.HtmlAttributeEncode(publicName); // !String.IsNullOrEmpty(publicName) ? publicName.Replace("'", "%27") : String.Empty;
        }
    }
}

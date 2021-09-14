using Strimm.Model;
using Strimm.Shared;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace StrimmTube.Utils
{
    public static class WebUtils
    {
        private static readonly string CLIENT_TIME_COOKIE = "ClientTime";
        private static readonly string USER_COOKIE = "userId";

        public static int? GetUserIdFromCookie(HttpCookieCollection cookies)
        {
            int? userId = null;

            if (cookies != null && cookies[USER_COOKIE] != null)
            {
                int id = 0;
                if (Int32.TryParse(cookies[USER_COOKIE].Value, out id)) {
                    userId = id;
                }
            }

            return userId;
        }

        public static UserLocation GetUserLocationFromCookie()
        {
            UserLocation loc = null;

            if (HttpContext.Current != null && HttpContext.Current.Request.Cookies["init"] != null)
            {
                // MST, per discussion with Dima, we will be storing "Last Known User Location" and
                // it may not necessarily be his country of origin that we are trying to capture on the SignUp UI.
                //
                string user = HttpContext.Current.Request.Cookies["init"].Value;
                var decoded = HttpUtility.UrlDecode(user);
                loc = new JavaScriptSerializer().Deserialize<UserLocation>(decoded);

                if (loc != null)
                {
                    var allstates = ReferenceDataManage.GetStatesByCountryName(loc.Country.Trim());
                    var user_state = allstates.FirstOrDefault(x => x.Code_2 == loc.State || x.Code_3 == loc.State);

                    if (user_state != null)
                    {
                        loc.State = user_state.Name;
                    }
                }
            }

            return loc;
        }


        public static Nullable<DateTime> GetClientTimeFromCookie(HttpCookieCollection cookies)
        {
            Nullable<DateTime> clientTime = new Nullable<DateTime>();

            if (cookies != null && cookies[CLIENT_TIME_COOKIE] != null)
            {
                string strClientTime = cookies[CLIENT_TIME_COOKIE].Value;

                clientTime = DateTimeUtils.GetClientTime(strClientTime);
            }

            return clientTime;
        }

        

        public static void DeleteUserIdCookie(HttpCookieCollection cookies)
        {
            if (cookies != null && cookies[USER_COOKIE] != null)
            {
                cookies.Remove(USER_COOKIE);
            }
        }

        internal static string GetChannelPassword()
        {
            throw new NotImplementedException();
        }

        internal static string GetChannelPassword(string channelUrl, string salt)
        {
            string pass = string.Empty;

            if (HttpContext.Current != null && HttpContext.Current.Request.Cookies[channelUrl] != null)
            {
                var channelPass = HttpContext.Current.Request.Cookies[channelUrl].Value;
                pass = System.Net.WebUtility.UrlDecode(channelPass);
               
                
              
        }
            return pass;
        }
    }
}
using StrimmBL;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace StrimmTube.WebServices
{
    /// <summary>
    /// Summary description for CalendarWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CalendarWebService : System.Web.Services.WebService
    {
        public class CalendarInfo
        {
            public int id;
            public string ack;
            public string time;
            public string date;

        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(EnableSession=true)]
        public string LoadData(string start, string end, double offset)
        {
            string output = "";
            int channelId = 0;
            JavaScriptSerializer js = new JavaScriptSerializer();
            if (HttpContext.Current.Session["channelTubeId"] != null)
            {
                channelId = int.Parse(HttpContext.Current.Session["channelTubeId"].ToString());
            }

            DateTime dateTime = DateTime.Now;
            DateTime clientTime = dateTime;

            List<ChannelSchedule> ChannelScheduleList = new List<ChannelSchedule>();//ScheduleManage.GetChannelScheduleListByChannelId(channelId);
           DateTime startTime = DateTime.ParseExact(start,"dd-MM-yyyy", CultureInfo.InvariantCulture);
           DateTime endTime = DateTime.ParseExact(end, "dd-MM-yyyy", CultureInfo.InvariantCulture);
           List<ChannelSchedule> filteredChannelScheduleList = new List<ChannelSchedule>();
           if (ChannelScheduleList.Count != 0)
           {
               foreach (var c in ChannelScheduleList)
               {
                   if ((c.StartTime.Date >= startTime.Date) && (c.StartTime.Date <= endTime.Date))
                   {
                       filteredChannelScheduleList.Add(c);
                   }
               }
           }
           if (filteredChannelScheduleList.Count != 0)
           {
               foreach (var f in filteredChannelScheduleList)
               {
                   CalendarInfo info = new CalendarInfo();
                  
                   string dateFormat = f.StartTime.ToString("dd/MM/yyyy");
                   info.id=f.ChannelScheduleId;
                   //TODO get endtime
                  // info.ack = "scheduled at " + f.StartTime.ToShortTimeString() + "-" + f.endTime.ToShortTimeString();
                   info.time=f.StartTime.ToShortTimeString();
                   info.date = dateFormat;
                   output+=js.Serialize(info)+",";

               }

               output=output.TrimEnd(',');
               output="["+output+"]";
           }
            return output;
        }
    }
}

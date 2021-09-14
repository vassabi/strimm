using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Shared
{
    public static class DateTimeUtils
    {
       public static string PrintTimeSpan(double timespanInSeconds)
       {
           var timespan = TimeSpan.FromSeconds(timespanInSeconds);
           string duration = String.Empty;

           if (timespan.TotalMinutes < 1.0)
           {
               duration = String.Format("{0}sec", timespan.Seconds);
           }
           else if (timespan.TotalHours < 1.0)
           {
               duration = String.Format("{0:D2}min:{1:D2}sec", timespan.Minutes, timespan.Seconds);
           }
           else
           {
               duration = string.Format("{0:D2}h:{1:D2}min:{2:D2}sec", timespan.Hours, timespan.Minutes, timespan.Seconds);
           }

           return duration;
        }

       public static string PrintPlaytimeDuration(DateTime? startTime, DateTime? endTime)
       {
            string start = startTime != null ? PrintAirTime(startTime.Value) : String.Empty;
           string end = endTime != null ? PrintAirTime(endTime.Value) : String.Empty;

           string message = String.Empty;

           if (startTime != null)
           {
               message = String.Format("{0} - {1}", start, end);
           }

           return message;
       }
       
       public static ArrayList BindDays(int year, int month)
       {
           int i;
           ArrayList aday = new ArrayList();
           switch (month)
           {
               case 1:
               case 3:
               case 5:
               case 7:
               case 8:
               case 10:
               case 12:

                   for (i = 1; i <= 31; i++)
                   {
                       aday.Add(i);
                   }

                   break;
               case 2:
                   if (CheckLeapYear(year))
                   {
                       for (i = 1; i <= 29; i++)
                           aday.Add(i);
                   }
                   else
                   {
                       for (i = 1; i <= 28; i++)
                           aday.Add(i);
                   }
                   break;
               case 4:
               case 6:
               case 9:
               case 11:
                   for (i = 1; i <= 30; i++)
                       aday.Add(i);
                   break;
           }
           return aday;

           
       }
       
       public static bool CheckLeapYear(int year)
       {
           if ((year % 4 == 0) && (year % 100 != 0) || (year % 400 == 0))

               return true;

           else return false;
       }

       public static Int32 GetAge(this DateTime dateOfBirth)
       {
           var today = DateTime.Today;

           var a = (today.Year * 100 + today.Month) * 100 + today.Day;
           var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

           return (a - b) / 10000;
       }

       public static bool IsPriorTo(this DateTime date, DateTime comparisonDate)
       {
           return date.Year <= comparisonDate.Year && date.Month <= comparisonDate.Month && date.Day < comparisonDate.Day;
       }

       public static string PrintAirTime(DateTime time)
       {
           return time.ToString("hh:mm tt");
       }

       /// <summary>
       /// {"Message":"Method not found: \u0027System.Nullable`1\u003cSystem.DateTime\u003e Strimm.Shared.DateTimeUtils.GetClientTime(System.String)\u0027.",
       /// "StackTrace":"   at StrimmTube.WebServices.ChannelWebService.GetCurrentlyPlayingChannelData(String clientTime, Int32 channelId, Int32 userId)",
       /// "ExceptionType":"System.MissingMethodException"}
       /// </summary>
       /// <param name="timestr"></param>
       /// <returns></returns>
       public static Nullable<DateTime> GetClientTime(string timestr)
       {
           if (String.IsNullOrEmpty(timestr) || timestr == "undefined")
           {
               return null;
           }

            var dateTimeArray = timestr.Split('-');
            string dateTime = dateTimeArray[0] + "/" + dateTimeArray[1] + "/" + dateTimeArray[2] + " " + dateTimeArray[3] + ":" + dateTimeArray[4];

            Nullable<DateTime> time = new Nullable<DateTime>();
            DateTime parseTime;
            if (DateTime.TryParse(dateTime, out parseTime))
            {
                time = parseTime;
            }

            return time;
       }

       public static DateTime GetLastDayOfTheMonth()
       {
           return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
       }
    }
}

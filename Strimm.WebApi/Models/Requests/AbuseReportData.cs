using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Strimm.WebApi.Models.Requests
{
    public class AbuseReportData
    {
        public string SelectedOption { get; set; }
        public string VideoTitle { get; set; }
        public string SenderDateTime { get; set; }
        public int ChannelScheduleId { get; set; }
        public int VideoTubeId { get; set; }
        public int SenderUserId { get; set; }
        public int ChannelId { get; set; }
    }
}
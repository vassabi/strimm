using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.MailChimp
{
    public class MailingListStats
    {
        public int member_count { get; set; }
        public int unsubscribe_count { get; set; }
        public int cleaned_count { get; set; }
        public int member_count_since_send { get; set; }
        public int unsubscribe_count_since_send { get; set; }
        public int cleaned_count_since_send { get; set; }
        public int campaign_count { get; set; }
        public DateTime? campaign_last_sent { get; set; }
        public int merge_field_count { get; set; }
        public int avg_sub_rate { get; set; }
        public int avg_unsub_rate { get; set; }
        public int target_sub_rate { get; set; }
        public float open_rate { get; set; }
        public float click_rate { get; set; }
        public DateTime? last_sub_date { get; set; }
        public DateTime? last_unsub_date { get; set; }
    }

}

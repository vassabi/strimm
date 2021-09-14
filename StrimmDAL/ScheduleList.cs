using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StrimmDAL
{
    public class ScheduleList
    {
        [Key]
        public int scheduleId { get; set; }
        [ForeignKey("userId")]
        public User user { get; set; }
        public int userId { get; set; }      
        public string videoProvider { get; set; }
        public string thumbnailUrl { get; set; }
        public string title { get; set; }
        public double duration { get; set; }
        public long videoViews { get; set; }
        public string description { get; set; }
        public string videoId { get; set; }
        public bool isInVideoRoom { get; set; }
        public int videoUploadId { get; set; }
        public int channelId { get; set; }
    }
}

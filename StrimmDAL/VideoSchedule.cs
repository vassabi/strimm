using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StrimmDAL
{
  public  class VideoSchedule
    {
       [Key]
        public int videoScheduleId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
       // [ForeignKey("videoUploadId")]
       // public VideoTube? videoTube { get; set; }
       // public int? videoUploadId { get; set; }
        [ForeignKey("channelScheduleId")]
        public ChannelSchedule channelSchedule { get; set; }
        public int channelScheduleId { get; set; }
        public double duration { get; set; }
        public int tempId { get; set; }
        public string provider { get; set; }
        public string videoPath { get; set; }
        public string title { get; set; }
        public string videoThumb { get; set; }
        public int videoRoomId { get; set; }       
        public int videoUploadId { get; set; }
    }
}

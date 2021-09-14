using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StrimmDAL
{
   public class ChannelSchedule
    {
        [Key]
        public int channelScheduleId { get; set; }
        public DateTime startTime { get; set; }
        public int channelTubeId { get; set; }
       // public int channelTubeId { get; set; }
        public DateTime endTime { get; set; }
        //  public virtual ICollection<VideoSchedule> videoScedules { get; set; }
        public bool isActive { get; set; }
    }
}

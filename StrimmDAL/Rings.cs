using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmDAL
{
   public class Rings
    {
       [Key]
       public int ringId { get; set; }
       [ForeignKey("channelScheduleId")]
       public ChannelSchedule channelSchedule { get; set; }
       public int channelScheduleId { get; set; }       
       public int followId { get; set; }
       public DateTime time { get; set; }
       public DateTime endOfSchedule { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmDAL
{
   public class PublicLib
    {
      
           [Key]
           public int videoUploadId { get; set; }
           public string title { get; set; }
           public string description { get; set; }
           public string videoPath { get; set; }
           public double duration { get; set; }//decimal type
           public string videoThumbnail { get; set; }//change to string (path to video on server)      
           public long videoCount { get; set; }
           public bool isScheduled { get; set; }
           public int useCount { get; set; }
           [ForeignKey("categoryId")]
           public ChannelCategory category { get; set; }
           public int categoryId { get; set; }
           public string provider { get; set; }
           public DateTime addedDate { get; set; }
           public bool r_rated { get; set; }
       
    }
}

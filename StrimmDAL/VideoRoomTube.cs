using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StrimmDAL
{
   public class VideoRoomTube
    {
        [Key]
        public int videoRoomId { get; set; }
       [ForeignKey("userId")]
        public User user { get; set; }
       public int userId { get; set; }
       public string url { get; set; }
    }
}

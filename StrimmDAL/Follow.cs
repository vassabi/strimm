using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmDAL
{
   public class Follow
    {
       [Key]
       public int followId { get; set; }
       [ForeignKey("userId")]
       public User user { get; set; }
       public int userId { get; set; }
       public DateTime followDate { get; set; }
       public int followerUserId { get; set; }
       public string userName { get; set; }
       

    }
}

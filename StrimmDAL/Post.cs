using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StrimmDAL
{
  public  class Post
    {
        [Key]
        public int postId { get; set; }
        [ForeignKey("userId")]
        public User user { get; set; }
        public int userId { get; set; }
        public string imageUrl { get; set; }
        public string post { get; set; }
        public DateTime postedDate { get; set; }
    }
}

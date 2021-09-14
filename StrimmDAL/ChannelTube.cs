using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StrimmDAL
{
    public class ChannelTube
    {
        [Key]
        public int channelID { get; set; }
        public string channelName { get; set; }
        [ForeignKey("categoryId")]
        public ChannelCategory category { get; set; }
        public int categoryId { get; set; }
        public long channelCounter { get; set; }
        public string channelDescription { get; set; }
        public bool termsConditions { get; set; }
        [ForeignKey("userID")]
        public User user { get; set; }
        public int userID { get; set; }
        public string channelPictureUrl { get; set; }
        public string channelUrl { get; set; }
        public DateTime creationDate { get; set; }
        public float rating { get; set; }
        public long subscriberCount { get; set; }
        public bool feturedChannel { get; set; }
    }
}

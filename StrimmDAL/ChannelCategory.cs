using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StrimmDAL
{
    public class ChannelCategory
    {
        [Key]
        public int channelCategoryId { get; set; }
        public string category { get; set; }
        //1 Mixed
        //2 Animals & Nature
        //3 Business
        //4 Education
        //5 Entertainment
        //6 Food
        //7 Games
        //8 Health & Sport
        //9 History
        //10 Home & Family
        //11 Music & Art
        //12 News
        //13 Other
        //14 Politics
        //15 Science & Technology
        //16 Technical&Industrial
        //17 Travel
    }
}

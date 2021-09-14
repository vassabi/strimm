using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmDAL
{
   public class VideoTubeInfo:VideoTube
    {
       public bool isRestricted { get; set; }
       public bool isRemoved { get; set; }
    }
}

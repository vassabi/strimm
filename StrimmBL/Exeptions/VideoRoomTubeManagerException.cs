using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL.Exeptions
{
    public class VideoRoomTubeManagerException : Exception
    {
        public VideoRoomTubeManagerException()
            : base()
        {

        }

        public VideoRoomTubeManagerException(string message) 
            : base(message)
        {

        }

        public VideoRoomTubeManagerException(string message, Exception innerException) 
            : base(message, innerException)
        {

        }
    }
}

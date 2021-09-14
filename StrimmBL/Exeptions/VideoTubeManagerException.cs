using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL.Exeptions
{
    public class VideoTubeManagerException : Exception
    {
        public VideoTubeManagerException()
            : base()
        {

        }

        public VideoTubeManagerException(string message) 
            : base(message)
        {

        }

        public VideoTubeManagerException(string message, Exception innerException) 
            : base(message, innerException)
        {

        }
    }
}

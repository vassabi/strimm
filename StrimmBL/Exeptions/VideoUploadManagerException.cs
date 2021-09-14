using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL.Exeptions
{
    public class VideoUploadManagerException : Exception
    {
        public VideoUploadManagerException()
            : base()
        {

        }

        public VideoUploadManagerException(string message) 
            : base(message)
        {

        }

        public VideoUploadManagerException(string message, Exception innerException) 
            : base(message, innerException)
        {

        }
    }
}

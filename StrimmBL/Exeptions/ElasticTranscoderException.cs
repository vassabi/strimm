using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL.Exeptions
{
    public class ElasticTranscoderException : Exception
    {
        public ElasticTranscoderException()
            : base()
        {

        }

        public ElasticTranscoderException(string message) 
            : base(message)
        {

        }

        public ElasticTranscoderException(string message, Exception innerException) 
            : base(message, innerException)
        {

        }
    }
}

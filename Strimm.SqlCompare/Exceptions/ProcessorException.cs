using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.SqlCompare.Exceptions
{
    public class ProcessorException : Exception
    {
        public ProcessorException()
            : base ()
        {

        }

        public ProcessorException(string message)
            : base (message)
        {

        }

        public ProcessorException(string message, Exception inner)
            : base (message, inner)
        {

        }
    }
}

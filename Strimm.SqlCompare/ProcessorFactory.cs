using Strimm.SqlCompare.Processor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.SqlCompare
{
    public class ProcessorFactory
    {
        private static string PROCESSOR_TYPE = "processorType";
        private IDbProcessor processor;

        private static ProcessorFactory instance = new ProcessorFactory();

        private ProcessorFactory()
        {
            var type = ConfigurationManager.AppSettings[PROCESSOR_TYPE];
            if (type == "RedGate")
            {
                processor = new RedGateDbProcessor();
            }
            else
            {
                processor = new xSqlDbProcessor();
            }
        }

        public static ProcessorFactory GetInstance()
        {
            return instance;
        }

        public IDbProcessor GetProcessor()
        {
            return this.processor;
        }
    }
}

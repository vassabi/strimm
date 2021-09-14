using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Projections
{
    public class ChannelSchedulePo : ChannelSchedule
    {
        private DateTime endTime;


        public DateTime EndTime
        {
            get
            {
                return this.endTime;
            }
            set
            {
                this.endTime = value;
            }
        }
    }
}

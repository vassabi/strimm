using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Projections
{
    public class ChannelCategoryPo : Category
    {
        private int channelCount;

        public int ChannelCount
        {
            get
            {
                return this.channelCount;
            }
            set
            {
                this.channelCount = value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Projections
{
    public class UserChannelEntitlements
    {
        public int ChannelsPurchasedCount { get; set; }

        public int ChannelsEmbeddedCount { get; set; }

        public int ChannelsAvailableToEmbedCount { get; set; }

        public int WhiteLabeledChannelsPurchasedCount { get; set; }

        public int WhiteLabeledChannelCount { get; set; }

        public int ChannelsAvailableToWhiteLabelCount { get; set; }

        public int PasswordProtectedChannelsPurchaseCount { get; set; }

        public int PasswordProtectedChannelCount { get; set; }

        public int ChannelsAvailableToPasswordProtectCount { get; set; }

        public int CustomLabelChannelsPurchaseCount { get; set; }

        public int CustomLabeledChannelCount { get; set; }

        public int ChannelsAvailableToCustomLabelCount { get; set; }

      
    }
}

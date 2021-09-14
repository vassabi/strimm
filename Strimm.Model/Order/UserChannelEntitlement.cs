using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public class UserChannelEntitlement
    {
        public int ChannelsPurchasedCount { get; set; }

        public int ChannelsEmbeddedCount { get; set; }

        public int ChannelsAvailableToEmbedCount { get; set; }

        public int WhiteLabeledChannelsPurchasedCount { get; set; }

        public int WhiteLabeledChannelsCount { get; set; }

        public int ChannelsAvailableToWhiteLabelCount { get; set; }

        public int PasswordProtectedChannelsPuchaseCount { get; set; }

        public int PasswordProtectedChannelCount { get; set; }

        public int ChannelsAvailableToPasswordProtectCount { get; set; }

        public int CustomLabelChannelsPurchaseCount { get; set; }

        public int CustomLabeledChannelCount { get; set; }

        public int ChannelsAvailableToCustomLabelCount { get; set; }

        public int MutedChannelsPurchasedCount { get; set; }

        public int MutedChannelCount { get; set; }

        public int ChannelsAvailableToMute { get; set; }

        public int MatureContentChannelCount { get; set; }

        public int ChannelsAvailableToMatureContentCount { get; set; }
        public int PlayerControlsChannelCount { get; set; }

        public int ChannelsAvailableToPlayerControlsCount { get; set; }

        public int PrivateVideosAllowedCount { get; set; }

        public int PrivateVideosForChannelsAvailableCount { get; set; }
    }
}

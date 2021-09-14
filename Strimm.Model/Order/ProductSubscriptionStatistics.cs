using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Order
{
    public class ProductSubscriptionStatistics
    {
        public int TotalSubscriberCount { get; set; }

        public int TotalSubscriptionsCount { get; set; }

        public int TotalSubscribedChannelCount { get; set; }

        public int ActiveSubscriptionsCount { get; set; }

        public int CanceledSubscriptionsCount { get; set; }

        public int InTrialSubscriptionsCount { get; set; }

        public int EmbeddedChannelsCount { get; set; }

        public int WhiteLabeledChannelsCount { get; set; }

        public int CustomLabeledChannelsCount { get; set; }

        public int MuteOnStartupChannelsCount { get; set; }

        public int PasswordProtectedChannelsCount { get; set; }
    }
}

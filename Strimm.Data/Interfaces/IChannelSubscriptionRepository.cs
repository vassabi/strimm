using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IChannelSubscriptionRepository
    {
        List<ChannelSubscription> GetAllChannelSubscriptionsByChannelTubeId(int channelTubeId);

        ChannelSubscription GetChannelSubscriptionByChannelTubeIdAndUserId(int channelTubeId, int userId);

        bool InsertChannelSubscription(ChannelSubscription subscription);

        bool DeleteChannelSubscriptionByChannelTubeIdAndUserId(int channelTubeId, int userId);

        bool DeleteChannelSubscriptionById(int channelSubscriptionId);
    }
}

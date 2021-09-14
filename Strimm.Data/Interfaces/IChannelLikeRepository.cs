using Strimm.Model;
using Strimm.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IChannelLikeRepository
    {
        int InsertChannelLike(ChannelLike like);
        int DeleteChannelLikeByChannelTubeIdAndUserId(int channelTubeId, int userId, DateTime likeEndDate);
    }
}

using Strimm.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
   public interface IEmbeddedHostChannelLoadRepository
    {

       bool InsertEmbeddedHostChannelLoad(int channelTubeId, DateTime clientTime, string embeddedHostUrl, string accountNumber, bool isSingleChannelView, bool IsSubscribedDomain);
       List<EmbeddedChannelPo> GetAllEmbeddedChannels();

       List<EmbeddedChannelPo> GetEmbeddedChannelsByDate(DateTime clientTime);

       int InsertEmbeddedHostChannelLoadWithGet(int channelTubeId, DateTime clientTime, string embeddedHostUrl, string accountNumber, bool isSingleChannelView, bool IsSubscribedDomain);

       bool UpdateEmbeddedHostChannelLoadById(int embeddedChannelHostLoadId, double visitTime, DateTime loadEndTime);
    }
}

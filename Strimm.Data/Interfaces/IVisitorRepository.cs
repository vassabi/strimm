using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IVisitorRepository
    {
        int InsertVisitor(Visitor visitor);

        void UpdateVisitor(Visitor visitor);

        void DeleteVisitorById(int visitorId);

        List<Visitor> GetAllChannelVisitorsByChannelTubeId(int channelTubeId);

        List<Visitor> GetAllBoardVisitorsByUserId(int userId);

        Visitor GetVisitorByVisitorId(int visitorId);

        void UpdateVisitDurationByVisitorId(float duration, int visitorId);

    }
}

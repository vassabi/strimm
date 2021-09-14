using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IRingRepository
    {
        void InsertRing(Ring ring);

        List<Ring> GetRingsByUserId(int userId);

        List<Ring> GetRingsByFollowerUserId(int followerUserId);

        void DeleteRingById(int ringId);
    }
}

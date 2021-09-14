using Strimm.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Repositories
{
    public class RingRepository : RepositoryBase, IRingRepository
    {
        public RingRepository()
            : base()
        {

        }

        public void InsertRing(Model.Ring ring)
        {
            throw new NotImplementedException();
        }

        public List<Model.Ring> GetRingsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public List<Model.Ring> GetRingsByFollowerUserId(int followerUserId)
        {
            throw new NotImplementedException();
        }

        public void DeleteRingById(int ringId)
        {
            throw new NotImplementedException();
        }
    }
}

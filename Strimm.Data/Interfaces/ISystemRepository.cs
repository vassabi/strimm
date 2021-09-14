using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface ISystemRepository
    {
        bool RebuildAllIndexesOnDatabase();
    }
}

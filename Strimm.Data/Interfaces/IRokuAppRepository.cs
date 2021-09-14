using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IRokuAppRepository
    {
        RokuApp GetUserApp(int UserId);
        Guid UpsertUserApp(RokuApp app);
    }
}

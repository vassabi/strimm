using Strimm.Data.Repositories;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL
{
    public class RokuAppManager
    {
        public static RokuApp GetUserApp(int UserId)
        {
            var app = new RokuApp();
            using (var repository = new RokuAppRepository())
            {
                app = repository.GetUserApp(UserId);
            }
            return app;
        }

        public static Guid UpsertUserApp(RokuApp app)
        {
            Guid id;
            using (var repository = new RokuAppRepository())
            {
                id = repository.UpsertUserApp(app);
            }
            return id;
        }
    }
}

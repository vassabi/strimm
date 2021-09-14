using Strimm.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL
{
    public static class SystemManage
    {
        public static bool RebuildAllIndexesOnDatabase()
        {
            bool isSuccess = false;

            using (var systemRepository = new SystemRepository())
            {
                isSuccess = systemRepository.RebuildAllIndexesOnDatabase();
            }

            return isSuccess;
        }
    }
}

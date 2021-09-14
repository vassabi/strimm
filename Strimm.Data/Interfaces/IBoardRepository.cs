using Strimm.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IBoardRepository
    {
        String GetUserBoardNameByUserId(int userId);

        UserBoard GetUserBoardDataByUserId(int userId);

        UserBoard GetUserBoardDataByUserName(String userName, DateTime clientTime);

        UserBoard GetUserBoardDataByPublicUrl(String publicUrl, DateTime clientTime);

        long GetNumberOfBoardVisitorsByUserId(int userId);
    }
}

using Strimm.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface INotesRepository
    {
        List<AdminUserNotePo> GetAdminUserNotesByUserId(int userId);

        bool UpdateAdminUserNote(int adminNoteId, string notes, string action);

        bool InsertAdminUserNote(int adminUserId, int userId, string notes, string action);
    }
}

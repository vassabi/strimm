using log4net;
using Strimm.Data.Repositories;
using Strimm.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL
{
   public static class AdminManage
    {

       private static readonly ILog Logger = LogManager.GetLogger(typeof(AdminManage));
        public static List<AdminUserNotePo> GetAdminUserNotesForUserByUserId(int userId)
        {
            Logger.Info(String.Format("Retrieving all admin user notes created for user with id={0}", userId));

            List<AdminUserNotePo> notes = new List<AdminUserNotePo>();

            using (var notesRepository = new NotesRepository())
            {
                notes = notesRepository.GetAdminUserNotesByUserId(userId);
            }

            return notes;
        }

        public static List<AdminUserNotePo> InsertAdminUserNote(int adminUserId, int userId, string note, string action)
        {
            Logger.Info(String.Format("Adding new admin note for admin user id={0}, user id={1}, note={2}", adminUserId, userId, note));

            var notes = new List<AdminUserNotePo>();

            using (var notesRepository = new NotesRepository())
            {
                if (notesRepository.InsertAdminUserNote(adminUserId, userId, note, action))
                {
                    notes = notesRepository.GetAdminUserNotesByUserId(userId);
                }
            }

            return notes;
        }

        public static List<AdminUserNotePo> UpdateAdminUserNote(int noteId, int userId, string note, string action)
        {
            Logger.Info(String.Format("Updating existing admin note with id={0}", noteId));

            var notes = new List<AdminUserNotePo>();

            using (var notesRepository = new NotesRepository())
            {
                if (notesRepository.UpdateAdminUserNote(noteId, note, action))
                {
                    notes = notesRepository.GetAdminUserNotesByUserId(userId);
                }
            }

            return notes;
        }
    }
}

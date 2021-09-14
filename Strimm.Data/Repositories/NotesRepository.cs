using Strimm.Data.Interfaces;
using Strimm.Model.Projections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using log4net;

namespace Strimm.Data.Repositories
{
    public class NotesRepository : RepositoryBase, INotesRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(NotesRepository));

        public List<AdminUserNotePo> GetAdminUserNotesByUserId(int userId)
        {
            List<AdminUserNotePo> notes = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    notes = this.StrimmDbConnection.Query<AdminUserNotePo>("strimm.GetAllAdminUserNotesByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve user notes for user id={0}", ex);
            }

            return notes;
        }

        public bool UpdateAdminUserNote(int adminNoteId, string notes, string action)
        {
            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.UpdateAdminUserNote", new
                    {
                        AdminNoteId = adminNoteId,
                        Notes = notes,
                        Action = action
                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update admin note with id='{0}', notes={1}", adminNoteId, notes), ex);
            }

            return isSuccess;
        }

        public bool InsertAdminUserNote(int adminUserId, int userId, string notes, string action)
        {
            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.InsertAdminUserNote", new
                    {
                        AdminUserId = adminUserId,
                        UserId = userId,
                        Notes = notes,
                        Action =action
                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update new user admin note for admin user id='{0}', user id={1}, notes={2}", adminUserId, userId, notes), ex);
            }

            return isSuccess;
        }
    }
}

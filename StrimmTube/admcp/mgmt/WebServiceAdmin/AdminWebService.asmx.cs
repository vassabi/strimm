using Strimm.Model.Projections;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace StrimmTube.admcp.mgmt.WebServiceAdmin
{
    /// <summary>
    /// Summary description for AdminWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class AdminWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<AdminUserNotePo> GetAdminUserNotesForUserByUserId(int userId)
        {
            return AdminManage.GetAdminUserNotesForUserByUserId(userId);
        }

        [WebMethod]
        public List<AdminUserNotePo> SaveAdminActivity (string notes, int userId, int adminUserId, string action)
        {
            return AdminManage.InsertAdminUserNote(adminUserId, userId, notes, action);
        }
    }
}

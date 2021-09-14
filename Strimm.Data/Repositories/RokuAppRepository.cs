using Dapper;
using Strimm.Data.Interfaces;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Repositories
{
    public class RokuAppRepository : RepositoryBase, IRokuAppRepository
    {
        public RokuAppRepository()
            : base()
        {

        }

        public RokuApp GetUserApp(int UserId)
        {
            string query = "Select * from strimm.UserRokuApps where UserId = " + UserId;
            var app = new RokuApp();
            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                app = this.StrimmDbConnection.Query<RokuApp>(query).FirstOrDefault();
            }
            return app;
        }

        public Guid UpsertUserApp(RokuApp app)
        {
            string updquery = "Update strimm.UserRokuApps set AppName='" + app.AppName + "', About='" + app.About + "',  AdLink='" + app.AdLink + "', PrivacyPolicyLink='" + app.PrivacyPolicyLink + "', ImageHD=@ImageHD, ImageSD=@ImageSD where UserId = " + app.UserID;
            string insquery = "Insert Into strimm.UserRokuApps (AppName, About, AdLink, PrivacyPolicyLink, ImageHD, ImageSD, UserId) Values('" + app.AppName + "', '" + app.About + "', '" + app.AdLink + "', '" + app.PrivacyPolicyLink + "', @ImageHD, @ImageSD, " + app.UserID + ")";
            var existingApp = GetUserApp(app.UserID);

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var command = this.StrimmDbConnection.CreateCommand();
                command.CommandText = existingApp == null ? insquery : updquery;
                var imageHdParam = new SqlParameter("@ImageHD", SqlDbType.Image, app.ImageHD.Length);
                var imageSdParam = new SqlParameter("@ImageSD", SqlDbType.Image, app.ImageSD.Length);
                imageHdParam.Value = app.ImageHD;
                imageSdParam.Value = app.ImageSD;
                command.Parameters.Add(imageHdParam);
                command.Parameters.Add(imageSdParam);
                command.ExecuteNonQuery();
            }

            if(existingApp == null)
            {
                existingApp = GetUserApp(app.UserID);
                return existingApp.ApiKey;
            }
            return existingApp.ApiKey;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.SqlCompare.Model
{
    public class DbConnectionProperties
    {
        public DbConnectionProperties(string dbServer, string dbName, string dbUser, string dbUserPassword)
        {
            this.DatabaseName = dbName;
            this.DatabaseServer = dbServer;
            this.UserName = dbUser;
            this.Password = dbUserPassword;
        }

        public string DatabaseServer { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string GetConnectionString()
        {
            return String.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", DatabaseServer, DatabaseName, UserName, Password);
        }
    }
}

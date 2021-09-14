using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Strimm.Data.Repositories
{
    public class RepositoryBase : IDisposable
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RepositoryBase));
        private bool disposed = false;

        private IDbConnection strimmDbConnection;

        public RepositoryBase()
        {
            InitializeDatabaseConnection();
        }

        public IDbConnection StrimmDbConnection
        {
            get
            {
                return this.strimmDbConnection;
            }
            private set
            {
                this.strimmDbConnection = value;
            }
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = String.Empty;

            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["strimm_monox"].ConnectionString;

                if (!String.IsNullOrEmpty(connectionString))
                {
                    this.StrimmDbConnection = new SqlConnection(connectionString);
                    this.StrimmDbConnection.Open();                    
                }                
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to initialize SQL connection", ex);
            }
        }

        // Public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            //Logger.Info("Disposing repository");

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State != System.Data.ConnectionState.Closed)
                {
                    try
                    {
                        //Logger.Debug("Closing database connection while disposing repository");

                        this.StrimmDbConnection.Close();
                        this.StrimmDbConnection = null;
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }
                }
            }

            // Free any unmanaged objects here. 
            //
            disposed = true;
        }
    }
}

using Strimm.Data.Interfaces;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using log4net;
using System.Diagnostics.Contracts;

namespace Strimm.Data.Repositories
{
    public class VisitorRepository : RepositoryBase, IVisitorRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VisitorRepository));
        
        public VisitorRepository()
            : base()
        {

        }

        public int InsertVisitor(Visitor visitor)
        {
            Contract.Requires(visitor != null, "Visitor data was not specified or is invalid");
            //Contract.Requires(visitor.UserId > 0, "UserId should be greater then 0");

         if(visitor.UserId==0)
         {
             visitor.UserId = null;
         }
            if(visitor.ChannelTubeId==0)
            {
                visitor.ChannelTubeId = null;
            }
           int visitorId =0;
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    //rowCount = this.StrimmDbConnection.Execute("strimm.InsertVisitor", visitor, null, 30, commandType: CommandType.StoredProcedure);
                    if (visitor.ChannelTubeId != null && visitor.ChannelTubeId > 0)
                    {
                        var result = this.StrimmDbConnection.Query<int>("strimm.InsertVisitor", new
                        {
                            UserId = visitor.UserId,
                            IpAddress = visitor.IpAddress,
                            VisitDate = visitor.VisitDate,
                            VisitDuration = visitor.VisitDuration,
                            Destination = visitor.Destination,
                            ChannelTubeId = visitor.ChannelTubeId,
                            VisitedUri = visitor.Uri,
                            VisitorUserId = visitor.VisitorUserId

                        }, null, false, 30, commandType: CommandType.StoredProcedure);
                        visitorId = result.FirstOrDefault();
                    }
                   
                    }
                   
            }
            catch (SqlException ex)
            {
                Logger.Error(String.Format("Failed to insert visitor record for UserId={0}, Ip={1}", visitor.UserId, visitor.IpAddress), ex);
            }

            if (visitorId == 0)
            {
                Logger.Warn("Failed to add new visitor");
            }

            return visitorId;
        }

        public void UpdateVisitor(Visitor visitor)
        {
            Contract.Requires(visitor != null, "Visitor data was not specified or is invalid");
            Contract.Requires(visitor.UserId > 0, "UserId should be greater then 0");
            Contract.Requires(visitor.VisitorId > 0, "Visitor should be greater then 0");

            int rowCount = 0;
            bool isSuccess = false;
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                   // rowCount = this.StrimmDbConnection.Execute("strimm.UpdateVisitor", visitor, null, 30, commandType: CommandType.StoredProcedure);
                
                   rowCount = this.StrimmDbConnection.Execute("strimm.UpdateVisitor", new
                    {
                         VisitorId = visitor.UserId,
                         UserId = visitor.UserId,
                         IpAddress = visitor.IpAddress,
                         VisitDate = visitor.VisitDate,
                         VisitDuration = visitor.VisitDuration,
                         Destination = visitor.Destination,
                         ChannelTubeId = visitor.ChannelTubeId
                       
                       

                    }, null, 30, commandType: CommandType.StoredProcedure);
                   isSuccess = true;
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(String.Format("Failed to update visitor record with Visitor Id = {0}", visitor.VisitorId), ex);
                throw;
            }

            if (!isSuccess)
            {
                throw new Exception(String.Format("Failed to update existing visitor with Id={0}", visitor.VisitorId));
            }
        }

        public void UpdateVisitDurationByVisitorId(float duration, int visitorId)
        {
           
           
            Contract.Requires(visitorId > 0, "Visitor should be greater then 0");

            int rowCount = 0;
            bool isSuccess = false;
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    // rowCount = this.StrimmDbConnection.Execute("strimm.UpdateVisitor", visitor, null, 30, commandType: CommandType.StoredProcedure);

                    rowCount = this.StrimmDbConnection.Execute("strimm.UpdateVisitDurationByVisitorId", new
                    {
                        VisitorId =visitorId,
                       
                        VisitDuration =duration
                      



                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(String.Format("Failed to update visitor record with Visitor Id = {0}", visitorId), ex);
                throw;
            }

            if (!isSuccess)
            {
                throw new Exception(String.Format("Failed to update existing visitor with Id={0}", visitorId));
            }
        }

        public void DeleteVisitorById(int visitorId)
        {
            Contract.Requires(visitorId > 0, "VisitorId should be greater then 0");

            int rowCount = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    rowCount = this.StrimmDbConnection.Execute("strimm.DeleteVisitorById", new { VisitorId = visitorId }, null, 30, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(String.Format("Failed to delete visitor record with Visitor Id = {0}", visitorId), ex);
                throw;
            }

            if (rowCount == 0)
            {
                throw new Exception(String.Format("Failed to delete existing visitor with Id={0}", visitorId));
            }
        }

        public List<Visitor> GetAllChannelVisitorsByChannelTubeId(int channelTubeId)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater 0");

            List<Visitor> visitors = new List<Visitor>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    visitors = this.StrimmDbConnection.Query<Visitor>("strimm.GetAllChannelVisitorsByChannelTubeId", new { ChannelTubeId = channelTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch(Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve channel visitors using channelTubeId = {0}", channelTubeId), ex);
            }

            return visitors;
        }

        public List<Visitor> GetAllBoardVisitorsByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            List<Visitor> visitors = new List<Visitor>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    visitors = this.StrimmDbConnection.Query<Visitor>("strimm.GetAllBoardVisitorsByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch(Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve board visitors for user id = {0}", userId), ex);
            }

            return visitors;
        }

        public Visitor GetVisitorByVisitorId(int visitorId)
        {
            Contract.Requires(visitorId > 0, "UserId should be greater then 0");

            Visitor visitor = new Visitor();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var result = this.StrimmDbConnection.Query<Visitor>("strimm.GetVisitorByVisitorId", new { VisitorId = visitorId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    visitor = result.FirstOrDefault();
                }
                
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve board visitors for user id = {0}", visitorId), ex);
            }

            return visitor;
        }

        public List<Visitor> GetAllVisitors()
        {
           

            List<Visitor> visitors = new List<Visitor>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    visitors = this.StrimmDbConnection.Query<Visitor>("strimm.GetAllVisitors", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve all visitors"), ex);
            }

            return visitors;
        }

    }
}

using log4net;
using Strimm.Data.Repositories;
using Strimm.Model;
using Strimm.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL
{
   public static class VisitorManage
    {
       private static readonly ILog Logger = LogManager.GetLogger(typeof(ChannelManage));

       public static int InsertVisitor(int visitorUserId, int channelTubeId, string clientTime, string visitorIp, string destination, string uri)
       {
           DateTime likeEndTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;
           int userId = 0;
           if(channelTubeId!=0)
           {
               userId =  UserManage.GetUserByChannelName(ChannelManage.GetChannelTubeById(channelTubeId).Name).UserId;
           }
           
            
            
           Logger.Info(String.Format("insert visitor to channel with userid={0}, channelTubeId={1}, visitorIp={2} and client time={3} ", userId, channelTubeId, visitorIp, clientTime));
           int visitorID = 0;
           using (var visitorRepository = new VisitorRepository())
           {
               DateTime clientTimeOfVisit = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;
               Visitor visitor = new Visitor();
               visitor.ChannelTubeId=channelTubeId;
                visitor.UserId = userId;
               visitor.Destination=destination;
               visitor.IpAddress=visitorIp;
               visitor.Uri=uri;
               visitor.VisitDate=clientTimeOfVisit;
               visitor.VisitDuration=0;
               visitor.VisitorUserId = visitorUserId;


               visitorID = visitorRepository.InsertVisitor(visitor);
           }

           return visitorID;
       }

       public static void UpdateVisitor(int visitorId, float durationCount)
       {
           Logger.Info(String.Format("update visitor duration on channel with visitorId={0}, durationCount={1} ", visitorId, durationCount));
          
           using (var visitorRepository = new VisitorRepository())
           {
              
               Visitor visitor = visitorRepository.GetVisitorByVisitorId(visitorId);
              
               visitor.VisitDuration = durationCount;


               visitorRepository.UpdateVisitor(visitor);
           }

        
       }

       public static void UpdateVisitDurationByVisitorId(int visitorId, float durationCount)
       {
           Logger.Info(String.Format("update visitor duration on channel with visitorId={0}, durationCount={1} ", visitorId, durationCount));

           using (var visitorRepository = new VisitorRepository())
           {


               visitorRepository.UpdateVisitDurationByVisitorId(durationCount, visitorId);
           }


       }
       
       public static List<Visitor>GetAllVisitors()
       {
           Logger.Info(String.Format("Get all visitors"));
           List<Visitor> visitorList = new List<Visitor>();
           using (var visitorRepository = new VisitorRepository())
           {


               visitorList =visitorRepository.GetAllVisitors();
           }
           return visitorList;
       }

    }
}

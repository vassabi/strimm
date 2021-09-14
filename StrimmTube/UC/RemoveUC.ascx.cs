using StrimmBL;
using Strimm.Model;
using StrimmTube.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.UC
{
    public partial class RemoveUC : System.Web.UI.UserControl
    {
        public int userId { get; set; }
        public int vrId { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //TODO
        //protected void removeRestrictedVideos_Click(object sender, EventArgs e)
        //{
        //  //if(Session["userId"]!=null)
        //  //{
        //  //    userId = int.Parse(Session["userId"].ToString());
        //  //    vrId = VideoRoomTubeManage.GetVideoRoomTubeIdByUserId(userId);
        //  //    List<VideoTube> videoTubeList = VideoRoomTubeManage.GetAllVideoTubesForVideoRoom(vrId);
        //  //    List<ScheduleList> scheduleList = ScheduleManage.GetAllSchedulelistsByUserId(userId);
        //  //    YouTubeWebService proxy = new YouTubeWebService();
        //  //    List<VideoTube> BatchedList = proxy.MakeBatchRequest(videoTubeList);
        //  //    List<ChannelTube> channelList = ChannelManage.GetChannelTubesForUser(userId);
        //  //   if(BatchedList.Count!=0)
        //  //   {
        //  //       foreach(var v in BatchedList)
        //  //       {
                    
        //  //           VideoTube tube = videoTubeList.Where(x => x.videoUploadId == v.videoUploadId).FirstOrDefault();
        //  //           bool inSchedule = false;
        //  //           if(channelList.Count!=0)
        //  //           {
        //  //               foreach (var c in channelList)
        //  //               {
        //  //                   inSchedule = ScheduleManage.isInSchedule(tube.videoUploadId, userId, c.channelID);
        //  //                   if (inSchedule == true) break;
        //  //               }
        //  //           }
        //             //if(!inSchedule)
        //             //{
        //             //    if (v.isRestricted==true || v.isRemoved==true)
        //             //    {
        //             //        ScheduleList sch = scheduleList.Where(x => x.videoUploadId == tube.videoUploadId).FirstOrDefault();
        //             //        if (sch != null)
        //             //        {
        //             //            ScheduleManage.DeleteScheduleListById(sch.scheduleId);
        //             //        }
                             
        //             //        VideoRoomTubeManage.RemoveVideoTubeFromVideoRoomTube(tube.videoUploadId, userId);
        //             //    }
        //             //}
                    
        //         }
        //     }
        //  }
        //}
    }
}
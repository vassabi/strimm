using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.UC
{
    public partial class PublicLibraryAdmin : System.Web.UI.UserControl
    {
       public string  boxContentId { get; set; }
        public string playId { get; set; }
        public string srcImage { get; set; }
        public string originalTitle { get; set; }
        public long views { get; set; }
        public string duration { get; set; }
        public string videoId { get; set; }
        public string actionId { get; set; }
        public string  editId { get; set; }
        public string addToScheduleId { get; set; }
        public string txtCustomTitle { get; set; }
        public string txtCustomizeDescription { get; set; }
        public string selectId { get; set; }
        public string closeId { get; set; }
        public string remove { get; set; }
        public string upadte { get; set; }
        public string inSchedule { get; set; }
        public string originalDescription { get; set; }
        public bool isMyVr { get; set; }
        public string addToVideoRoomOrSchedule { get; set; }
        public string addToScheduleOrVRFunc { get; set; }
        public bool rrated { get; set; }
        public bool isVideoRemoved { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
           
          if(isVideoRemoved==true)
          {
              if (isVideoRemoved == true)
              {
                  customizedDivHolder.Visible = false;
                  removedVideo.Visible = true;
                 
                  divEdit.Visible = false;
              }
          }
        }
    }
}
using StrimmBL;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace StrimmTube.UC
{
    public partial class BackEndMenu : System.Web.UI.UserControl
    {
        List<ChannelTube> channelTubeList;
        public int userId { get; set; }
        int selectedIndex;
        public int vrId { get; set; }
        public int channelPickedId { get; set; }
        public string channelUrl { get; set; }
        public bool is10Channels { get; set; }
        public bool isZeroChannels { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (Page is VideoSearchTube)
            //{
            //    btnCreateNew.Visible = false;

            //}
            if (Page is Schedule)
            {
                btnCreateNew.Visible = false;

            }
            //if (Page is VideoRoom)
            //{
            //    btnCreateNew.Visible = false;

            //}
            //if (Page is TimeTable)
            //{
            //    btnCreateNew.Visible = false;

            //}
            if (Page is Guides)
            {
                btnCreateNew.Visible = false;

            }
            if (Session["channelTubeId"] != null)
            {
             int channelId = int.Parse(Session["channelTubeId"].ToString());

            }
            if (Session["userId"] != null)
            {

                userId = int.Parse(Session["userId"].ToString());
                vrId = VideoRoomTubeManage.GetVideoRoomTubeIdByUserId(userId);
            }

            else
            {
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
            }
           
           // CheckList list = UserManage.GetCheckListByUserId(userId);
            channelTubeList = ChannelManage.GetChannelTubeByUserIdForAdmin(userId);
           if(channelTubeList==null)
           {
               Session["selectedIndex"] = null;
           }
            if (Session["selectedIndex"] != null)
            {
                selectedIndex = int.Parse(Session["selectedIndex"].ToString());
                ddlChannels.SelectedIndex = selectedIndex;
            }
            if (Session["channelTubeId"] == null)
            {
                channelPickedId = 0;
            }
            else
            {
                int channelID = int.Parse(Session["channelTubeId"].ToString());
                if(channelID!=0)
                {
                    ChannelTube channel = ChannelManage.GetChannelTubeById(channelID);
                channelUrl = channel.Url;
                channelPickedId = 1;
                }
                else
                {
                    channelPickedId = 0;
                }
            }


            //if (list.isHasProfile == false)
            //{
            //    Response.Redirect("profile");
            //}
            
            if ((channelTubeList != null) && (channelTubeList.Count != 0) && (!Page.IsPostBack))
            {
                //no new channel if user already have 10 channels
                if (channelTubeList.Count < 10)
                {
                    is10Channels = false;
                    ListItem itemNew = new ListItem();
                    itemNew.Value = "0";
                    itemNew.Text = "create channel";
                    ddlChannels.Items.Add(itemNew);
                   // ddlModal.Items.Add(itemNew);
                }
               
                foreach (var c in channelTubeList)
                {
                    ListItem item = new ListItem();
                    item.Text = c.Name;
                    item.Value = c.ChannelTubeId.ToString();
                    ddlChannels.Items.Add(item);
                   // ddlModal.Items.Add(item);
                }
                if (channelTubeList.Count == 10)
                {
                    bool firstLoad = true;
                    is10Channels = true;
                    if(Session["firstLoad"]!=null)
                    {
                        firstLoad =Boolean.Parse(Session["firstLoad"].ToString());
                    }
                    int selectedIndex = 0;
                    int selectedChannelId = 0;
                    if (Session["selectedIndex"]!=null)
                    {
                        selectedIndex = int.Parse(Session["selectedIndex"].ToString());
                    }
                    if(Session["channelTubeId"]!=null)
                    {
                        selectedChannelId = int.Parse(Session["channelTubeId"].ToString());
                    }
                    if ((selectedIndex == 0)&&(firstLoad==true))
                    {
                        ddlChannels.SelectedIndex = 0;
                        Session["selectedIndex"] = ddlChannels.SelectedIndex;
                        Session["channelTubeId"] = ddlChannels.SelectedValue;
                       
                    }
                    btnCreateNew.Enabled = false;
                   
                   
                }
                if((channelTubeList.Count>0)&&(channelTubeList.Count<10))
                {
                    bool firstLoad = true;
                    if (Session["firstLoad"] != null)
                    {
                        firstLoad = Boolean.Parse(Session["firstLoad"].ToString());
                    }
                    int selectedIndex = 0;
                    int selectedChannelId = 0;
                    if (Session["selectedIndex"] != null)
                    {
                        selectedIndex = int.Parse(Session["selectedIndex"].ToString());
                    }
                    if (Session["channelTubeId"] != null)
                    {
                        selectedChannelId = int.Parse(Session["channelTubeId"].ToString());
                    }
                    if ((selectedIndex == 0) && (firstLoad == true))
                    {
                        ddlChannels.SelectedIndex = 1;
                        Session["selectedIndex"] = ddlChannels.SelectedIndex;
                        Session["channelTubeId"] = ddlChannels.SelectedValue;
                    }
                   

                    
                }
            }
            if((channelTubeList==null)||(channelTubeList.Count==0))
            {
                ListItem itemNew = new ListItem();
                itemNew.Value = "0";
                itemNew.Text = "create channel";
                ddlChannels.Items.Add(itemNew);
              //  ddlModal.Items.Add(itemNew);
                Session["channelTubeId"] = "0";
                btnCreateNew.Enabled = true;
                isZeroChannels = true;
            }
            else
            {
                isZeroChannels = false;
            }
            if (Session["selectedIndex"] != null)
            {
                int selectedIndex = int.Parse(Session["selectedIndex"].ToString());
                ddlChannels.SelectedIndex = selectedIndex;
              //  ddlModal.SelectedIndex = selectedIndex;
            }
            //if (Session["channelTubeId"] == null)
            //{
            //    ScriptManager.RegisterStartupScript(modalDDL, modalDDL.GetType(), "ShowModal()", "ShowModal()", true);
            //}
        }
        protected void ddlChannels_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["firstLoad"] = false;
            Session["channelTubeId"] = null;
            Session["selectedIndex"] = null;
            Session["selectedIndex"] = ddlChannels.SelectedIndex;
            Session["channelTubeId"] = ddlChannels.SelectedValue;
            //if (Page is CreateChannel)
            //{
            //    Response.Redirect("create-channel", false);
            //    Context.ApplicationInstance.CompleteRequest();
            //}
            //if (Page is VideoSearchTube)
            //{
            //    Response.Redirect("add-videos", false);
            //    Context.ApplicationInstance.CompleteRequest();
              
            //}
            if (Page is Schedule)
            {
                Response.Redirect("schedule", false);
                Context.ApplicationInstance.CompleteRequest();
                
            }
            //if(Page is VideoRoom)
            //{
            //    Response.Redirect("video-room?id=" + vrId, false);
            //    Context.ApplicationInstance.CompleteRequest();
               
            //}
            //if(Page is TimeTable)
            //{
            //    Response.Redirect("timetable", false);
            //    Context.ApplicationInstance.CompleteRequest();
                
            //}
           
        }

        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            Session["firstLoad"] = false;
            Session["channelTubeId"] = null;
            Session["selectedIndex"] = null;
            ddlChannels.SelectedIndex = 0;
            Session["selectedIndex"] = ddlChannels.SelectedIndex;
            Session["channelTubeId"] = ddlChannels.SelectedValue;
            Response.Redirect("create-channel", false);
            Context.ApplicationInstance.CompleteRequest();
           
        }



        //protected void ddlModal_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Session["channelTubeId"] = null;
        //    Session["selectedIndex"] = null;
        //    Session["selectedIndex"] = ddlChannels.SelectedIndex;
        //    Session["channelTubeId"] = ddlChannels.SelectedValue;
        //    if (Page is CreateChannel)
        //    {
        //        Response.Redirect("create-channel");
        //    }
        //    if (Page is VideoSearchTube)
        //    {
        //        Response.Redirect("search");
        //    }
        //    if (Page is Schedule)
        //    {
        //        Response.Redirect("schedule");
        //    }
        //    if (Page is VideoRoom)
        //    {
        //        Response.Redirect("video-room?id" + vrId);
        //    }
        //}
    }
}
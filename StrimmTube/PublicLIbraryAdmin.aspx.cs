using StrimmBL;
using Strimm.Model;
using StrimmTube.UC;
using StrimmTube.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Strimm.Model.Projections;

namespace StrimmTube
{
    public partial class PublicLIbraryAdmin : BasePage
    {
        List<VideoTubePo> tubeList { get; set; }
        string provider = "youtube";
        List<VideoTubePo> videoTubeList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsProd)
            {
                TurnOffCrawling();
            }

            if (Session["publib"] == null)
            {
                Response.Redirect("PubLibLogin.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                bool isAutheticated = bool.Parse(Session["pubLib"].ToString());

                if(isAutheticated==false)
                {
                    Response.Redirect("PubLibLogin.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    //All crazy things here
                    tubeList = PublicLibraryManage.GetAllVideoTubesByProvider(provider);
                  //  Session["videoTubeListNew"] = null; 
                    Session["videoTubeList"] = null;
                  videoTubeList = new List<VideoTubePo>();
                    if(tubeList.Count!=0||tubeList!=null)
                    {
                        foreach (var t in tubeList)
                        {
                            //VideoTube tube = new VideoTube();
                            //tube.addedDate = t.addedDate;
                            //tube.category = t.category;
                            //tube.categoryId = t.categoryId;
                            //tube.description = t.description;
                            //tube.duration = t.duration;
                            //tube.isRemoved = true;
                            //tube.isRestricted = true;
                            //tube.isScheduled = false;
                            //tube.provider = t.provider;
                            //tube.r_rated = t.r_rated;
                            //tube.title = t.title;
                            //tube.useCount = t.useCount;
                            //tube.videoCount = t.videoCount;
                            //tube.videoPath = t.videoPath;
                            //tube.videoThumbnail = t.videoThumbnail;
                            //tube.videoUploadId = t.videoUploadId;
                            //videoTubeList.Add(tube);

                        }
                    }
                   
                    YouTubeWebService service = new YouTubeWebService();
                    List<VideoTubePo> newVideoTubeList = null; // service.MakeBatchRequest(videoTubeList);
                    Session["videoTubeList"] = newVideoTubeList;
                    BuildVideoBox(newVideoTubeList);
                    if (!IsPostBack)
                    {
                        PopulateCategoryDropDown();

                    }
                }
            }
        }

        private void PopulateCategoryDropDown()
        {

            var categoryList = ReferenceDataManage.GetAllCategories();
            var videoTubeCounters = PublicLibraryManage.CountVideoTubesByCategory();
            var allItem = new ListItem();

            try
            {
                allItem.Text = "all";
                allItem.Value = "0";

                ddlCategory.Items.Add(allItem);

                categoryList.ForEach(c =>
                    {
                        var counter = videoTubeCounters.FirstOrDefault(x => x.EntityId == c.CategoryId);
                        int videoCounter = counter != null ? counter.VideoTubeCounter : 0;
                        var item = new ListItem()
                            {
                                Value = c.CategoryId.ToString(),
                                Text = String.Format("{0} ({1})", c.Name, videoCounter)
                            };
                        ddlCategory.Items.Add(item);
                    });
            }
            catch (Exception err)
            {
                //error
            }
        }

        private void BuildVideoBox(List<VideoTubePo> list)
        {
            YouTubeWebService youtubeService = new YouTubeWebService();

            int count = 0;
            divResulSearchURL.Controls.Clear();
            if (list.Count != 0)
            {

                foreach (var t in list)
                {
                    //if(!t.isRestricted||!t.isRemoved)
                    //{
                    //    bool isVideoRemoved = false; //make check removed or restricted
                    //    string videoUploadId = t.videoUploadId.ToString();
                    //    PublicLibraryAdmin ctrl = (PublicLibraryAdmin)LoadControl("~/UC/PublicLibraryAdmin.ascx");

                    //    // int vrId = int.Parse(Request.QueryString["id"]);
                    //    //bool isMyVr = VideoRoomTubeManage.IsMyVr(vrId, userId);

                    //    ctrl.boxContentId = "boxContentId_" + videoUploadId;
                    //    ctrl.playId = t.videoPath;
                    //    ctrl.srcImage = t.videoThumbnail;
                    //    ctrl.originalTitle = t.title;
                    //    ctrl.views = t.videoCount;
                    //    ctrl.duration = ScheduleManage.PrintTimeSpan(t.duration);
                    //    ctrl.videoId = t.videoPath;
                    //    ctrl.originalDescription = t.description;
                    //    //ctrl.actionId = "action_" + videoUploadId;

                    //    ctrl.editId = "edit_" + videoUploadId;
                    //    // ctrl.txtCustomTitle = "customTitle_" + videoUploadId;
                    //    //ctrl.txtCustomizeDescription = "CustomDescription_" + videoUploadId;
                    //    ctrl.selectId = "select_" + videoUploadId;
                    //    ctrl.closeId = "close_" + videoUploadId;
                    //    ctrl.remove = "remove_" + videoUploadId;
                    //    ctrl.upadte = "update_" + videoUploadId;
                    //    ctrl.rrated = t.r_rated;
                    //    ctrl.isVideoRemoved = isVideoRemoved;
                    //    divResulSearchURL.Controls.Add(ctrl);
                    //    count++;

                    //}
                    
                }
                lblVideoCount.Text = count.ToString();

            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedValue = int.Parse(ddlCategory.SelectedValue);


            if (selectedValue == 0)
            {
                List<VideoTubePo> AllList = videoTubeList;
                //build VRTubeBox User control
                Session["videoTubeListNew"] = AllList;
                ddlSortVideo.SelectedIndex = 0;
                BuildVideoBox(AllList);

            }
            else
            {

                List<VideoTube> listByCategoryValue = new List<VideoTube>();
                if (Session["videoTubeList"] != null)
                {
                //    var list = (List<VideoTube>)Session["videoTubeList"];
                //    foreach (var l in list)
                //    {
                //        if (l.categoryId == selectedValue)
                //        {
                //            listByCategoryValue.Add(l);
                //        }
                //    }
                //    Session["videoTubeListNew"] = null;
                //    Session["videoTubeListNew"] = listByCategoryValue;
                //    BuildVideoBox(listByCategoryValue);
                }
                else
                {
                  //  tubeList = PublicLibraryManage.GetTubeListByProvider(provider);
                    //foreach (var l in videoTubeList)
                    //{
                    //    if (l.categoryId == selectedValue)
                    //    {
                    //        listByCategoryValue.Add(l);
                    //    }
                    //}
                    //Session["videoTubeListNew"] = null;
                    //Session["videoTubeListNew"] = listByCategoryValue;
                    //BuildVideoBox(listByCategoryValue);
                }

            }
        }

        protected void ddlSortVideo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["videoTubeListNew"] == null)
            {
                Session["videoTubeListNew"] = Session["videoTubeList"];
            }
            int selectedIndex = ddlSortVideo.SelectedIndex;
            ScriptManager.RegisterStartupScript(divResulSearchURL, divResulSearchURL.GetType(), "ScrollerUp()", "ScrollerUp()", true);
            //switch (selectedIndex)
            //{

            //    case 0: //sort by will bring all videos

            //        break;
            //    case 1: // newest
            //        var list1 = (List<VideoTube>)Session["videoTubeListNew"];
            //        var newestList = list1.OrderBy(x => x.addedDate).ToList();
            //        BuildVideoBox(newestList);
            //        break;
            //    case 2://oldest
            //        var list2 = (List<VideoTube>)Session["videoTubeListNew"];
            //        var oldestList = list2.OrderByDescending(x => x.addedDate).ToList();
            //        BuildVideoBox(oldestList);
            //        break;
            //    case 3: //duration a-z
            //        var list3 = (List<VideoTube>)Session["videoTubeListNew"];
            //        var durationAZList = list3.OrderBy(x => x.duration).ToList();
            //        BuildVideoBox(durationAZList);
            //        break;
            //    case 4://duration z-a
            //        var list4 = (List<VideoTube>)Session["videoTubeListNew"];
            //        var durationZAList = list4.OrderByDescending(x => x.duration).ToList();
            //        BuildVideoBox(durationZAList);
            //        break;
            //    case 5://most viewed
            //        var list5 = (List<VideoTube>)Session["videoTubeListNew"];
            //        var mostViewedList = list5.OrderByDescending(x => x.videoCount).ToList();
            //        BuildVideoBox(mostViewedList);
            //        break;

            //}
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session["publib"] = null;
            Response.Redirect("PubLibLogin.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
        
        
    }
}
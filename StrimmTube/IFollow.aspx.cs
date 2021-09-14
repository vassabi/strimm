using StrimmBL;
using Strimm.Model;
using StrimmTube.UC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class IFollow : BasePage
    {
        public bool isMyfollwers { get; set; }
        int userId { get; set; }
        User user { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsProd)
            {
                TurnOffCrawling();
            }

            if (Session["userId"] != null)
            {
                userId = int.Parse(Session["userId"].ToString());
                if (Request.QueryString["id"] != null)
                {
                    
                    int followUserId = int.Parse(Request.QueryString["id"].ToString());
                  //  user = UserManage.GetUserById(followUserId);
                    if (userId == followUserId)
                    {
                        isMyfollwers = true;
                    }
                    else
                    {
                        isMyfollwers = false;
                    }

                }
                else
                {
                    Response.Redirect("control-panel", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                //List<Follow> followList = UserManage.GetAllFollowersByUserId(user.userId);
               
                //    GetUserFollowers(user, followList);
                
               
                   
               
            }
            else
            {
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void ddlSortFollowers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ddlSortFollowers.SelectedIndex;
            ScriptManager.RegisterStartupScript(followerHolder, followerHolder.GetType(), "ScrollerUp()", "ScrollerUp()", true);
            //switch (selectedIndex)
            //{

            //    case 0: //sort by will bring all videos
            //        break;
            //    case 1: // userName
            //        var list1 = UserManage.GetAllFollowersByUserId(user.userId);;
            //        var userName = list1.OrderBy(x => x.userName).ToList();
            //      //  Session["followersList"] = userName;
            //        GetUserFollowers(user,userName);
            //        break;
            //    case 2://date
            //        var list2 = UserManage.GetAllFollowersByUserId(user.userId);
            //        var date = list2.OrderByDescending(x => x.followDate).ToList();
            //      //  Session["followersList"] = date;
            //        GetUserFollowers(user,date);
            //        break;
               

            // }
        }
        //public void GetUserFollowers(User user, List<Follow>followList)
        //{
        //    followerHolder.Controls.Clear();
            
        //    if (followList.Count != 0)
        //    {
        //        foreach (var f in followList)
        //        {
        //            FollowBoxUC followCtrl = (FollowBoxUC)LoadControl("~/UC/FollowBoxUC.ascx");
                   
        //            User userFollower = UserManage.GetUserById(f.userId);
        //            followCtrl.userBoardUrl = userFollower.userName;
        //            followCtrl.userName = userFollower.userName;
        //            followCtrl.followerId = f.followerUserId;
        //            if (!String.IsNullOrEmpty(userFollower.profileImageUrl))
        //            {
        //                followCtrl.avatarUrl = userFollower.profileImageUrl;
        //            }
        //            else
        //            {
        //                if (userFollower.gender == "Male")
        //                {
        //                    followCtrl.avatarUrl = "/images/imgUserAvatarMale.jpg";
        //                }
        //                else
        //                {
        //                    followCtrl.avatarUrl = "/images/imgUserAvatarFemale.jpg";
        //                }
        //            }
        //            followCtrl.isMyFollowers = isMyfollwers;
        //            followerHolder.Controls.Add(followCtrl);


        //            //get posts of all followers
        //        }

        //    }
        //    else
        //    {
        //        ddlSortFollowers.Visible = false;
        //        followerHolder.Controls.Clear();
        //        Label lblMessage = new Label();
        //        lblMessage.ID = "lblMsg";
        //        lblMessage.ClientIDMode = ClientIDMode.Static;
        //        lblMessage.Text = "You are not following anyone yet. It is time to connect!";
        //        followerHolder.Controls.Add(lblMessage);
        //    }

        //    Session["followersList"] = null;


        //}
    }
}
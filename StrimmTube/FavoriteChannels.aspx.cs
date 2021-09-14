using StrimmBL;
using Strimm.Model;
using StrimmTube.UC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Strimm.Model.WebModel;
using Strimm.Model.Projections;
using Strimm.Shared;
using StrimmTube.Utils;

namespace StrimmTube
{
    public partial class FavoriteChannels : BasePage
    {
        public int userId { get; set; }

        public List<ChannelTubeModel> faveChannelList { get; set; }

        protected override void OnPreRender(EventArgs e)
        {
            if (!IsPostBack)
            {
                TurnOffCrawling();
            }

            base.OnPreRender(e);
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //get user id
            if (Session["userId"] != null)
            {
                userId = int.Parse(Session["userId"].ToString());
            }
            else
            {
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            var userName = Page.RouteData.Values["UserName"].ToString();
            UserPo user = UserManage.GetUserByPublicUrl(userName);

            if (user == null || userId != user.UserId)
            {
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                AddTitle(String.Format("{0} Favorite Videos", user.UserName), false);

                var clientTime = WebUtils.GetClientTimeFromCookie(Request.Cookies) ?? DateTime.Now;

                faveChannelList = ChannelManage.GetAllFavoriteChannelsForUserByUserIdAndClientTime(userId, clientTime);

                if (faveChannelList.Count != 0)
                {
                    Session["faveChannelList"] = faveChannelList;
                }
                else
                {
                    lblMessage.Visible = true;
                }
            }
        }
    }
}
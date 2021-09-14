using Strimm.Model.WebModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.UC
{
    public partial class ChannelsHolderCategoryUC : System.Web.UI.UserControl
    {
        public List<ChannelTubeModel> channelList { get; set; }

        public string categoryName { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (channelList.Count > 0)
            {
                int number = 1;
                channelList.ForEach(c =>
                {
                    var bctrl = LoadControl("~/UC/LandingPageChannelBox.ascx") as LandingPageChannelBox;

                    bctrl.channelId = c.ChannelTubeId;
                    bctrl.channelHref = String.Format("/{0}/{1}", c.ChannelOwnerUserName.Replace(" ",string.Empty), c.ChannelUrl);
                    bctrl.channelName = c.Name;
                    bctrl.channelImage = (c.PictureUrl != null && !String.IsNullOrEmpty(c.PictureUrl)) ? c.PictureUrl : "/images/comingSoonBG.jpg";
                    bctrl.IsOnAir = c.PlayingVideoTubeId > 0;
                    bctrl.IsLink = false;
                    bctrl.channelNumber = number++;

                    channelHolder.Controls.Add(bctrl);
                });

                
            }
        }
    }
}
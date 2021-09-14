using StrimmBL;
using Strimm.Model;
using StrimmTube.UC;
using StrimmTube.WebServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Strimm.Model.Projections;
using System.Web.UI.HtmlControls;
using Strimm.Shared;

namespace StrimmTube
{
    public partial class Channel : BasePage
    {
        public string channelUrl { get; set; }
       public int channelId { get; set; }
        int offset { get; set; }
       public int channelScheduleId {get;set; }
       public bool isSubscribed { get; set; }
       public bool isMyChannel { get; set; }
       //List<Rings> ringsList { get; set; }
       //List<Rings> followRingList { get; set; }
       public string fasebookUrl { get; set; }
       public string channelImageUrl { get; set; }
       public string channelDescription { get; set; }
       public string description { get; set; }
       public string channelName { get; set; }
       public int userId { get; set; }

       List<ChannelTubePo> channelList = new List<ChannelTubePo>();

       private string defaultChannelPictureUrl;

       public List<ChannelTubePo> topChannels { get; set; }

       public List<ChannelTubePo> favoriteChannels { get; set; }

       public List<ChannelTubePo> myChannels { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.defaultChannelPictureUrl = ConfigurationManager.AppSettings["DefaultChannelPictureUrl"];

            if (!IsProd)
            {
                TurnOffCrawling();
            }
        }


        private void BuildSliderChannels(List<ChannelTubePo> channels)
        {
            ulSliderChannels.Controls.Clear();

            if (channels != null && channels.Count > 0)
            {
                channels.ForEach(t =>
                {

                    HtmlGenericControl liadd = new HtmlGenericControl("li");

                    var bctrl = LoadControl("~/UC/BoardChannelBox.ascx") as BoardChannelBox;

                    bctrl.isMyChannel = false;
                    bctrl.editChannelUrl = String.Format("/schedule/{0}", t.Url);
                    bctrl.channelHref = String.Format("/channel/{0}", t.Url);
                    bctrl.channelName = t.Name;
                    bctrl.channelImage = ImageUtils.GetChannelImageUrl(t.PictureUrl);
                    bctrl.rating = t.Rating.ToString();
                    bctrl.views = t.ChannelViewsCount.ToString();
                    bctrl.subscribers = t.SubscriberCount.ToString();
                    bctrl.channelCategory = t.CategoryName;
                    bctrl.channelId = t.ChannelTubeId;

                    liadd.Controls.Add(bctrl);

                    ulSliderChannels.Controls.Add(liadd);
                });
            }
        }

        protected void chkbxTop_CheckedChanged(object sender, EventArgs e)
        {
            AggregateSliderChannels(chkbxTop.Checked, chkbxFave.Checked);
        }

        protected void chkbxFave_CheckedChanged(object sender, EventArgs e)
        {
            AggregateSliderChannels(chkbxTop.Checked, chkbxFave.Checked);
        }

        protected void chkbxMyChannels_CheckedChanged(object sender, EventArgs e)
        {
            AggregateSliderChannels(chkbxTop.Checked, chkbxFave.Checked);
        }

        private void AggregateSliderChannels(bool displayTopChannels, bool displayFavoriteChannels)
        {
            if (displayFavoriteChannels && displayTopChannels)
            {
                BuildSliderChannels(this.channelList);
            }
            else if (displayFavoriteChannels)
            {
                BuildSliderChannels(this.favoriteChannels);
            }
            else
            {
                chkbxTop.Checked = true;
                BuildSliderChannels(this.topChannels);
            }
        }

       
    }
}
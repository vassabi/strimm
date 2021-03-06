using StrimmBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace StrimmTube.UC
{
    public partial class TopMenuBeforeLogin : System.Web.UI.UserControl
    {
        public string browseMenuHtml;

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RetrieveBrowseCategoriesWithCounts();
        }

        protected void RetrieveBrowseCategoriesWithCounts()
        {
            DateTime clientTime = DateTime.Now;

            if (Session["ClientTime"] != null)
            {
                string strClientTime = Session["ClientTime"].ToString();

                DateTime time;
                if (DateTime.TryParse(strClientTime, out time))
                {
                    clientTime = time > DateTime.MinValue ? time : clientTime;
                }
            }

            var categories = ReferenceDataManage.GetChannelCategoriesWithCurrentlyPlayingChannelsCountForBrowseChannels(clientTime);

            var currentColumn = 0;
            var currentRow = 0;
            var maxColumnCount = 6;
            var maxRowCount = 2;

            if (categories != null)
            {
                string html = String.Empty;

                categories.ForEach(x =>
                {
                    html += "<li class='";

                    if (currentRow < maxRowCount)
                    {
                        if (currentColumn == 0)
                        {
                            html += "first firstColumn";
                        }
                        else if (currentColumn == maxColumnCount)
                        {
                            html += "first lastColumn";
                        }
                        else
                        {
                            html += "first";
                        }
                    }
                    else
                    {
                        if (currentColumn == 0)
                        {
                            html += "last firstColumn";
                        }
                        else if (currentColumn == maxColumnCount)
                        {
                            html += "last lastColumn";
                        }
                        else
                        {
                            html += "last";
                        }
                    }

                    html += "'><a href='/browse-channel?category=" + x.Name + "'>" + x.Name;
                    html += "<div id='ch" + x.CategoryId + "' class='channelCount'>" + (x.ChannelCount == 0 ? String.Empty : x.ChannelCount.ToString()) + "</div></a></li>";

                    currentColumn += 1;

                    if (currentColumn > maxColumnCount)
                    {
                        currentColumn = 0;
                        currentRow += 1;
                    }
                });

                browseMenuHtml = html;
            }
        }

    }
}
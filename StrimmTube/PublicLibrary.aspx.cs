using Strimm.Model;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class PublicLibrary : BasePage
    {
        public int channelTubeId { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (!IsProd)
                {
                    TurnOffCrawling();
                }
            }
        }
        private void PopulateCategoryDropDown()
        {
            // ddlCategory.Items.Clear();
            if(Session["ChannelTubeId"]!=null)
            {
                channelTubeId = int.Parse(Session["ChannelTubeId"].ToString());
            }
            List<Category> categoryList = ReferenceDataManage.GetAllCategories();

            try
            {
                ListItem allItem = new ListItem();
                allItem.Text = "all";
                allItem.Value = "0";
                ListItem firstItem = new ListItem();
                firstItem.Text = "select category";
                firstItem.Value = "0";
                ddlCategory.Items.Add(allItem);
                

                var videoTubeCounters = VideoTubeManage.CountVideoTubesInPublicLibraryByCategory();

                categoryList.ForEach(c =>
                {
                    var counter = videoTubeCounters.FirstOrDefault(x => x.EntityId == c.CategoryId);
                    if (counter != null)
                    {
                        var item = new ListItem()
                        {
                            Value = c.CategoryId.ToString(),
                            Text = String.Format("{0} ({1})", c.Name, counter.VideoTubeCounter)
                        };

                        ddlCategory.Items.Add(item);
                        //ddlKeyWord.Items.Add(item);
                        //ddlUrl.Items.Add(item);
                    }
                });
            }
            catch (Exception err)
            {
                //error
            }
        }
    }
}
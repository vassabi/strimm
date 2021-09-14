using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace StrimmTube.UC
{
    public partial class SearchedVideoBoxUC : System.Web.UI.UserControl
    {
        public string srcImage { get; set; }
        public string originalTitle { get; set; }
        public string originalDescriptiom { get; set; }
        public string views { get; set; }
        public string duration { get; set; }
        public string txtCustomTitle { get; set; }
        public string closeId { get; set; }
        public string boxContentId { get; set; }
        public string actionId { get; set; }
        public string txtCustomizeDescription { get; set; }
        public string addToSchedule { get; set; }
        public string addToVr { get; set; }
        public string countTitle { get; set; }
        public string countDesc { get; set; }
        public string playId { get; set; }
        public string selectId { get; set; }
        public string addId { get; set; }
        public string side { get; set; }
        public double doubleDuration { get; set; }
        public bool isRestricted { get; set; }
        public int durInt { get; set; }
        public long viewsInt { get; set; }
        public bool isInVr { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

       
    }
}
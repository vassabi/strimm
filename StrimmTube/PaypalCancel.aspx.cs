using Strimm.Model.Order;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class PaypalCancel : System.Web.UI.Page
    {
        public string boardUrl { get; set; }

        public string userUrl { get; set; }

        public int userId { get; set; }

        public bool canceled { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var orderCookie = Request.Cookies["OrderDetails"];
                var orderNumber = orderCookie != null ? orderCookie.Value : String.Empty;

                var order = OrderManager.GetOrderByOrderNumber(orderNumber);

                if (order != null)
                {
                    var user = UserManage.GetUserPoByUserId(order.UserId);

                    var confirmation = OrderManager.CancelOrder(orderNumber);

                    if (confirmation != null)
                    {
                        this.canceled = confirmation.IsSuccess;
                    }

                    this.userUrl = user.PublicUrl;
                    this.boardUrl = user.PublicUrl;

                    Request.Cookies.Remove("OrderDetails");
                }
                else
                {
                    Response.Redirect("/home", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
            }
        }
    }
}
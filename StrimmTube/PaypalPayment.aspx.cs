using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class PaypalPayment : System.Web.UI.Page
    {

        public string userId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    int userId = Convert.ToInt32(Session["UserId"].ToString());
                }
            }
//            if(!IsPostBack)
//            {
               
//                    Form.Attributes.Add("action", "https://www.sandbox.paypal.com/cgi-bin/webscr");
                
//            string innerHtml =@"<input type='hidden' name='cmd' value='_s-xclick'> 
//                                <input type='hidden' name='hosted_button_id' value='Y9VN7R2JC2M2W'>
//                                <table>
//                                <tr>
//                                <td>
//                                <input type='hidden' name='on0' value='Select Amount Of Channels'>Select Amount Of Channels
//                                </td>
//                                </tr>
//                                <tr>
//                                <td>
//                                <select name='os0'>
//                                <option value='Single (1) Channel'>Single (1) Channel : $9.99 USD - monthly</option>
//                                <option value='Two (2) Channels'>Two (2) Channels : $19.98 USD - monthly</option>
//                                <option value='Three (3) Channels'>Three (3) Channels : $29.97 USD - monthly</option>
//                                <option value='Four (4) Channels'>Four (4) Channels : $39.96 USD - monthly</option>
//                                <option value='Five (5) Channels'>Five (5) Channels : $49.95 USD - monthly</option>
//                                <option value='Six (6) Channels'>Six (6) Channels : $59.94 USD - monthly</option>
//                                <option value='Seven (7) Channels'>Seven (7) Channels : $69.93 USD - monthly</option>
//                                <option value='Eight (8) hannels'>Eight (8) Channels : $79.92 USD - monthly</option>
//                                <option value='Night (9) Channels'>Night (9) Channels : $89.91 USD - monthly</option>
//                                <option value='Ten (10) Channels'>Ten (10) hannels : $99.90 USD - monthly</option>
//                                </select>
//                                </td>
//                                </tr>
//                                <tr>
//                                <td><input type='hidden' name='on1' value='Enter Your Strimm Public Name'>Enter Your Strimm Public Name</td>
//                                </tr>
//                                <tr>
//                                <td><input type='text' name='os1' maxlength='200'>
//                                </td>
//                                </tr>
//                                </table>
//                                <input type='hidden' name='currency_code' value='USD'><input type='image' src='https://www.paypalobjects.com/en_US/i/btn/btn_subscribeCC_LG.gif' border='0' name='submit' alt='PayPal - The safer, easier way to pay online!'><img alt='' border='0' src='https://www.paypalobjects.com/en_US/i/scr/pixel.gif' width='1' height='1'>";

//            Form.InnerHtml = innerHtml;


//            }
        }
    }
}
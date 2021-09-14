using Strimm.Model.Projections;
using StrimmBL;
using StrimmTube.Utils;
using Stripe;
using Stripe.BillingPortal;
using System;

namespace StrimmTube
{
    public partial class StripeCustomerPortal : BasePage
    {
        private StripeOptions options;
        protected void Page_Load(object sender, EventArgs e)
        {
            options = new StripeOptions();
            StripeConfiguration.ApiKey = options.SecretKey;
            CustomerPortal();
        }

        public void CustomerPortal()
        {
            if(Session["userId"] == null)
            {
                return;
            }

            var user = UserManage.GetUserPoByUserId(int.Parse(Session["userId"].ToString()));

            if (string.IsNullOrEmpty(user.StripeCustomerId))
            {
                string id = CreateStripeCustomer(user);
                user.StripeCustomerId = id;
                UserManage.UpdateUserProfile(user);
            }

            var options = new SessionCreateOptions
            {
                Customer = user.StripeCustomerId
            };

            var service = new SessionService();
            var session = service.Create(options);

            Response.Redirect(session.Url);
        }

        private string CreateStripeCustomer(UserPo user)
        {
            var options = new CustomerCreateOptions
            {
                Email = user.Email, Name = user.FirstName + " " + user.LastName
            };
            var service = new CustomerService();
            Customer customer = service.Create(options);
            return customer.Id;
        }
    }
}
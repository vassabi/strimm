using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Stripe;
using Stripe.Checkout;
using System.Web.Services;
using StrimmTube.Utils;
using StrimmBL;
using Strimm.Model.Projections;

namespace StrimmTube.WebServices
{
    /// <summary>
    /// Summary description for StripePaymentController
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class StripePaymentController : System.Web.Services.WebService
    {
        public StripeOptions options;
        private IStripeClient client;

        [WebMethod(EnableSession = true)]
        public void Hello()
        {
            return;
        }


        [WebMethod]
        public string CreateCheckoutSession(string PriceId, int UserId)
        {
            this.options = new StripeOptions();
            this.client = new StripeClient(this.options.SecretKey);
            var user = UserManage.GetUserPoByUserId(UserId);

            if (string.IsNullOrEmpty(user.StripeCustomerId))
            {
                string id = CreateStripeCustomer(user);
                user.StripeCustomerId = id;
                UserManage.UpdateUserProfile(user);
            }
            options = new StripeOptions();
            var sessionOptions = new SessionCreateOptions
            {
                SuccessUrl = options.DomainToRedirect + options.PaymentSuccessUrl + "?session_id={CHECKOUT_SESSION_ID}",
                Customer = user.StripeCustomerId,
                CancelUrl = options.DomainToRedirect + options.PaymentFailUrl,
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                Mode = "subscription",
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = PriceId,
                        Quantity = 1
                    },
                },
            };
            var service = new SessionService(this.client);
            try
            {
                var session = service.CreateAsync(sessionOptions);
                return session.Result.Id;
            }
            catch (StripeException e)
            {
                return null;
            }
        }

        private string CreateStripeCustomer(UserPo user)
        {
            var options = new CustomerCreateOptions
            {
                Email = user.Email,
                Name = user.FirstName + " " + user.LastName
            };
            var service = new CustomerService();
            Customer customer = service.Create(options);
            return customer.Id;
        }
    }
}

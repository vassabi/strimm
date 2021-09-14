using log4net;
using Strimm.Model;
using Strimm.Model.Order;
using Strimm.Model.Projections;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class PaymentSuccess : System.Web.UI.Page
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PaymentSuccess));

        public string boardUrl { get; set; }

        public string userUrl { get; set; }

        public int userId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                Logger.Debug("Processing page request which is not a postback");

                if (Session["userId"] != null)
                {
                    Logger.Debug("Processing page request which is not a postback for active user");
                    
                    int userId = Convert.ToInt32(Session["userId"].ToString());

                    var user = UserManage.GetUserPoByUserId(userId);

                    string paypalTx = Request.QueryString["tx"];

                    Logger.Debug("Page request came from PayPal PDT");

                    if (!String.IsNullOrEmpty(paypalTx))
                    {
                        Logger.Debug("Processing Payment Data Transfer request from PayPal");

                        string payPalUrl = ConfigurationManager.AppSettings["PayPalUrl"];
                        string pdtToken = ConfigurationManager.AppSettings["PayPalPdtToken"];

                        //read in txn token from querystring
                        //string query = string.Format("cmd=_notify-synch&tx={0}&at={1}", paypalTx, pdtToken);
                        string[] parts = paypalTx.ToUpper().Split(',');
                        string query = string.Format("cmd=_notify-synch&tx={0}&at={1}", parts[0], pdtToken);

                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        ServicePointManager.DefaultConnectionLimit = 9999;

                        Logger.Error(String.Format("Verifying Payment Data Transfer: {0}", payPalUrl));
                        Logger.Error(String.Format("Payload for Payment Data Transfer: {0}", query));

                        // Create the request back
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(payPalUrl);

                        // Set values for the request back
                        req.Method = "POST";
                        req.ContentType = "application/x-www-form-urlencoded";
                        req.ContentLength = query.Length;

                        // Write the request back IPN strings
                        StreamWriter stOut = new StreamWriter(req.GetRequestStream(),
                        System.Text.Encoding.ASCII);
                        stOut.Write(query);
                        stOut.Close();

                        // Do the request to PayPal and get the response
                        StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
                        string strResponse = stIn.ReadToEnd();
                        stIn.Close();

                        Logger.Error(String.Format("Received Payment Data Transfer verification response: {0}", strResponse));

                        // If response was SUCCESS, parse response string and output details
                        if (strResponse.StartsWith("SUCCESS"))
                        {
                            Console.WriteLine("Payment request was successful");

                            // Payment information successfully retrieved
                            strResponse = strResponse.Substring("SUCCESS".Length+1);
                            IpnMessage msg = new IpnMessage(HttpUtility.UrlDecode(strResponse), "\n");
                            msg.NotifyVersion = "none";
                            msg.VerifySign = "none";

                            var existingIpnMessage = OrderManager.SaveIpnMessage(msg);

                            ProcessPayPalSubscriberPaymentMessage(existingIpnMessage);

                            OrderPo order = null;
                            OrderSubscriptionProfile orderProfile = null;

                            order = OrderManager.GetOrderByOrderNumber(existingIpnMessage.OptionSelection1);

                            if (order != null)
                            {
                                orderProfile = OrderManager.GetOrderSubscriptionProfileByOrderId(order.OrderId);
                            }

                            if (order != null && order.UserId == userId)
                            {
                                var product = ProductManager.GetProductById(order.ProductId);

                                if (orderProfile != null)
                                {
                                    this.lblCharge.Text = Convert.ToString(orderProfile.CycleBillingAmount);
                                }

                                if (product != null)
                                {
                                    this.lblPlanType.Text = product.Name;
                                }

                                this.lblPaypalConfirmation.Text = order.OrderNumber;

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
                        else
                        {
                            // Error processing Payment Data Transfer
                            Logger.Error(String.Format("Error occured while processing Payment Data Transfer: {0}", strResponse));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Retrieving order details");

                        var orderCookie = Request.Cookies["OrderDetails"];
                        var orderNumber = orderCookie != null ? orderCookie.Value : String.Empty;

                        OrderPo order = null;
                        OrderSubscriptionProfile orderProfile = null;

                        int i = 0;
                        do
                        {
                            Thread.Sleep(10000);
                            i++;

                            Console.WriteLine(String.Format("Retrieving order details for order with id={0}", orderNumber));

                            order = OrderManager.GetOrderByOrderNumber(orderNumber);

                            if (order != null)
                            {
                                Console.WriteLine(String.Format("Retrieved order details for order with id={0}", order.OrderId));
                                orderProfile = OrderManager.GetOrderSubscriptionProfileByOrderId(order.OrderId);
                            }
                        }
                        while (order != null && (orderProfile == null || (orderProfile != null && orderProfile.CycleBillingAmount == 0)));

                        if (order != null)// && order.UserId == userId)
                        {
                            Console.WriteLine(String.Format("Retrieved order data for display for order with id={0}", order.OrderId));

                            var product = ProductManager.GetProductById(order.ProductId);

                            if (orderProfile != null)
                            {
                                this.lblCharge.Text = Convert.ToString(orderProfile.CycleBillingAmount);
                            }

                            if (product != null)
                            {
                                this.lblPlanType.Text = product.Name;
                            }

                            this.lblPaypalConfirmation.Text = order.OrderNumber;

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

        private void ProcessPayPalSubscriberPaymentMessage(IpnMessage ipnObj)
        {
            string facilitator = ConfigurationManager.AppSettings["PaymentReceiverEmail"].ToString();

            if (ipnObj.TxnType == "subscr_payment")
            {
                if (ipnObj.ReceiverEmail != facilitator)
                {
                    Logger.Warn(String.Format("Ignoring ipn message received for a wrong receiver e-mail. Expected '{0}' and got '{1}'", facilitator, ipnObj.ReceiverEmail));
                    return;
                }

                string subscriberId = ipnObj.SubscriberId;

                if (!String.IsNullOrEmpty(subscriberId))
                {
                    var subscriber = OrderManager.GetPayingSubscriberByPayPalSubscriberId(subscriberId);

                    if (subscriber != null)
                    {
                        var existingOrder = OrderManager.GetOrderPoById(subscriber.OrderId);

                        if (existingOrder != null)
                        {
                            if (existingOrder.OrderStatusId == (int)OrderStatusEnum.PendingPaymentResolution)
                            {
                                existingOrder.OrderStatusId = (int)OrderStatusEnum.Active;
                                existingOrder.OrderExpirationDate = null;

                                existingOrder = OrderManager.UpdateOrderStatusById(existingOrder);

                                Logger.Debug(String.Format("Updated order status from 'Pending Payment Resolution' to 'Active' for order with id={0}", existingOrder.OrderId));
                            }

                            // Handle payment posted for a new subscription
                            Logger.Debug(String.Format("Payment received for subscription from Subscriber with Id={0}, Order Number='{1}' in the amount={2}", ipnObj.SubscriberId, existingOrder.OrderNumber, ipnObj.Amount1));

                            var orderTransaction = OrderManager.CreateOrderTransaction(existingOrder.OrderId, subscriber.PayingSubscriberId, ipnObj);

                            Logger.Debug(String.Format("Order trunsaction with id={0} and amount={1} was created for order number = '{2}'", orderTransaction.OrderTransactionId, orderTransaction.McGross, existingOrder.OrderNumber));

                            var productSubscription = OrderManager.UpdateProductSubscriptionStatus(existingOrder.OrderId, ProductSubscriptionStatusEnum.Active);

                            if (productSubscription != null)
                            {
                                Logger.Debug(String.Format("Updated product subscription record with id={0} to Active", productSubscription.ProductSubscriptionId));
                            }
                            else
                            {
                                Logger.Warn(String.Format("Failed to update product subscription record for order with id={0} to Active", existingOrder.OrderId));
                            }
                        }
                        else
                        {
                            Logger.Warn(String.Format("Failed to retrieve order details for order with id={0}", subscriber.OrderId));
                        }
                    }
                    else
                    {
                        string orderNumber = ipnObj.OptionSelection1; // Used with subscription with selection options ipnObj.OptionSelection2;

                        if (!String.IsNullOrEmpty(orderNumber))
                        {
                            CreateOrderDetailsUsingPayPalSubscriptionPaymentMessage(ipnObj);
                            ProcessPayPalSubscriberPaymentMessage(ipnObj);
                        }

                        Logger.Warn(String.Format("Unable to located paying subscriber record for PayPal subscriber id='{0}'", subscriberId));
                    }
                }
            }
        }

        private void CreateOrderDetailsUsingPayPalSubscriptionPaymentMessage(IpnMessage ipnObj)
        {
            Logger.Info(String.Format("Handling new paypal subscriber signup message with ipn: '{0}'", ipnObj.RawMessage));

            string facilitator = ConfigurationManager.AppSettings["PaymentReceiverEmail"].ToString();

            if (ipnObj != null && ipnObj.TxnType == "subscr_payment")
            {
                var orderConfirmationTemplate = new Uri(Server.MapPath("~/Emails/ThankYouForYourSubscriptionEmail.html"));

                if (ipnObj.ReceiverEmail != facilitator)
                {
                    Logger.Warn(String.Format("Ignoring ipn message received for a wrong receiver e-mail. Expected '{0}' and got '{1}'", facilitator, ipnObj.ReceiverEmail));
                    return;
                }

                string orderNumber = ipnObj.OptionSelection1;

                if (!String.IsNullOrEmpty(orderNumber))
                {
                    var existingOrder = OrderManager.GetOrderByOrderNumber(orderNumber);

                    var possibleTxnAmounts = OrderManager.GetPossiblePaymentAmounts(orderNumber);

                    var priorTxn = OrderManager.GetPriorTransactionBySubscriberId(ipnObj.SubscriberId);

                    if (priorTxn.Count > 0)
                    {
                        Logger.Warn(String.Format("Ignoring duplicate ipn message. There are '{0}' order transactions in the db associated to this message already", priorTxn.Count));
                        return;
                    }

                    var user = UserManage.GetUserPoByUserId(existingOrder.UserId);

                    var product = ProductManager.GetProductById(existingOrder.ProductId);

                    if (possibleTxnAmounts.Any(x => x == ipnObj.McGross || x % product.Price == 0))
                    {
                        Logger.Debug(String.Format("Subscription was successfully created for Order='{0}', by subscriber with id={1} and e-mail='{2}', in the amount of ${3}", existingOrder.OrderNumber, ipnObj.SubscriberId, ipnObj.PayerEmail, ipnObj.McGross));

                        var subscriber = OrderManager.AssociatePayingSubscriberWithOrder(existingOrder.OrderId, ipnObj);

                        Logger.Debug(String.Format("New paying subscriber with id={0} was associated with order '{1}'", subscriber.PayingSubscriberId, existingOrder.OrderId));

                        var profile = OrderManager.AssociateSubscriptionProfileWithOrder(existingOrder.OrderId, subscriber.PayingSubscriberId, ipnObj);

                        Logger.Debug(String.Format("Subscription profile with id={0} was successfully associated with order with id={1}", profile.OrderSubscriptionProfileId, existingOrder.OrderId));

                        int channelCount = (int)(ipnObj.McGross / possibleTxnAmounts.FirstOrDefault());

                        if (!product.IsPricePerChannel)
                        {
                            channelCount = product.MaxChannelCount;
                        }

                        Logger.Debug(String.Format("Subscriber with id={0} purchased channel subscription for {1} channels with order={2}", subscriber.PayingSubscriberId, channelCount, existingOrder.OrderId));

                        existingOrder.OrderStatusId = (int)OrderStatusEnum.InTrial;
                        existingOrder.ChannelCount = channelCount;

                        existingOrder = OrderManager.UpdateOrderByOrderId(existingOrder);

                        Logger.Debug(String.Format("Updated order status to '{0}'", existingOrder.OrderStatusId));

                        var productSubscription = OrderManager.GetProductSubscriptionByOrderId(existingOrder.OrderId);

                        if (productSubscription == null)
                        {
                            productSubscription = OrderManager.CreateProductSubscription(existingOrder.OrderId, existingOrder.ProductId, ProductSubscriptionStatusEnum.InTrial);

                            Logger.Debug(String.Format("Added new product subscription record with id={0} to track order with id={1}", productSubscription.ProductSubscriptionId, existingOrder.OrderId));
                        }

                        bool emailSend = EmailManage.SendOrderConfirmationEmail(user, existingOrder, orderConfirmationTemplate, product);

                        Logger.Debug(String.Format("Subscription confirmation e-mail was send to '{0}' for order '{1}'", user.Email, existingOrder.OrderNumber));
                    }
                    else
                    {
                        Logger.Warn(String.Format("Received ipn message is not associated with any valid transaction amounts. Transaction amount received is ${0}", ipnObj.McAmount3));
                    }
                }
                else
                {
                    Logger.Warn(String.Format("Invalid order number specified on the received IPN message: '{0}'", ipnObj.RawMessage));
                }
            }            
        }
    }
}
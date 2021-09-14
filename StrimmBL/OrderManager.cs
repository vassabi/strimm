using log4net;
using Strimm.Model;
using Strimm.Model.WebModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using Strimm.Data.Repositories;
using Strimm.Model.Order;
using Strimm.Shared;
using Strimm.Model.Projections;

using System.Collections.Specialized;

using System.IO;
using System.Configuration;

namespace StrimmBL
{
    public class OrderManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(OrderManager));

        public static OrderConfirmation PlaceOrder(CreateOrderBindingModel model)
        {
            Contract.Requires(model != null, "Invalid order information specified on request");
            Contract.Requires(model.ProductId > 0, "Invalid product id specified on the order");
            Contract.Requires(model.UserId > 0, "Invalid user id specified on the order");

            Logger.Debug(String.Format("Processing new order request for product with id={0} submitted by user with id={1}", model.ProductId, model.UserId));

            var user = UserManage.GetUserPoByUserId(model.UserId);

            if (user == null)
            {
                throw new Exception("User was not found. Order aborted");
            }

            Product product = ProductManager.GetProductById(model.ProductId);

            if (product == null)
            {
                throw new Exception("Product was not found. Order aborted");
            }

            var countries = ReferenceDataManage.GetCountries();

            OrderPo order = null;

            using (var country_repository = new ReferenceDataRepository())
            using (var repository = new OrderRepository())
            {
                var country = countries.Where(x => x.Name.ToLower().Trim() == user.Country.ToLower().Trim()).FirstOrDefault();

                string countryCode = country != null ? country.Code_3 : string.Empty;

                string newOrderNumber = String.Format("STRIMM{0}", CryptoUtils.GenerateRandomString(14));

                int priorOrderCount = GetPriorOrderCountByUserId(user.UserId);

                bool isTrialAllowed = priorOrderCount <= 0;

                Logger.Debug(String.Format("The new order is a(an) {0} subscription with trial period {1} allowed", model.IsAnnual ? "ANNUAL" : "MONTHLY", isTrialAllowed ? "" : "NOT"));

                order = repository.CreateOrder(user.UserId, user.FirstName, user.LastName, user.Address, user.City, user.StateOrProvince, countryCode, model.ProductId, newOrderNumber, product.Description, isTrialAllowed, model.IsAnnual, model.IsUpgrade);
            }

            return (new OrderConfirmation()
            {
                IsSubscription = product.IsSubscription ?? false,
                IsSuccess = (order != null),
                Message = (order != null) ? "Order was created successfully." : "Failed to create order.",
                OrderNumber = order.OrderNumber,
                OrderStatus = order.OrderStatus,
                ProductName = product.Name,
                PublicUrl = user.PublicUrl,
                IsAnnual = order.IsAnnual,
                TrialAllowed = order.TrialAllowed
            });

        }
        public static List<OrderPo>GetOrderPosByUserIdForAdmin(int userId)
        {
            Contract.Requires(userId!=null, "user id cant be null");
           

            Logger.Debug(String.Format("Requesting order with order userId '{0}'", userId));

            List<OrderPo> orderList = new List<OrderPo>();

            using (var repository = new OrderRepository())
            {
               orderList  = repository.GetOrderPosByIdForAdmin(userId);
                
            }

            return orderList;
        }
        public static OrderPo GetOrderByOrderNumber(string orderNumber)
        {
            Contract.Requires(!String.IsNullOrEmpty(orderNumber), "Invalid order number specified");
            Contract.Requires(orderNumber.Substring(0, 6) == "STRIMM", "Order number has an invalid format");

            Logger.Debug(String.Format("Requesting order with order number '{0}'", orderNumber));

            OrderPo order;

            using (var repository = new OrderRepository())
            {
                order = repository.FindOrderByOrderNumber(orderNumber);
            }

            return order;
        }
        
        public static string GetPayPalResponse(bool useSandbox, string content)//IEnumerable<KeyValuePair<string, string>> ipn)
        {
            Logger.Debug("Starting paypal verification of a newly received IPN message");

            string responseState = "";

            // Parse the variables
            // Choose whether to use sandbox or live environment
            string paypalUrl = useSandbox ? "https://www.sandbox.paypal.com/"
                                          : "https://www.paypal.com/";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(paypalUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    HttpResponseMessage response = client.PostAsync("cgi-bin/webscr", new StringContent(content)).Result;//new FormUrlEncodedContent(ipn)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        responseState = response.Content.ReadAsStringAsync().Result;
                        Logger.Debug(String.Format("IPN message was successfully verified against PayPal: {0}", responseState));
                    }
                    else
                    {
                        Logger.Debug(String.Format("Failed to verify IPN message against PayPal: {0}", responseState));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while contacting PayPal to verify IPN message", ex);
            }

            return responseState;
        }

        public static IpnMessage SaveIpnMessage(IpnMessage ipnMsg)
        {
            Contract.Requires(ipnMsg != null, "Invalid ipm message specified");

            Logger.Debug(String.Format("Attempting to save newly received IPN message with SubscriberId='{0}' and Type='{1}'", ipnMsg.SubscriberId, ipnMsg.TxnType));

            IpnMessage message;

            using (var repository = new OrderRepository())
            {
                message = repository.SavePayPalIpnMessage(ipnMsg);
            }

            return message;
        }

        public static List<OrderTransactionPo> GetPriorTransactionBySubscriberId(string subscriberId)
        {
            Contract.Requires(!String.IsNullOrEmpty(subscriberId), "Invalid ipn message subscriber id specified");

            Logger.Debug(String.Format("Requesting existing order transactions that were created in response to ipn message with SubscriberId='{0}'", subscriberId));

            List<OrderTransactionPo> transactions;

            using (var repository = new OrderRepository())
            {
                transactions = repository.GetOrderTransactionsByIpnMessageSubscriberId(subscriberId);
            }

            return transactions;
        }

        public static List<decimal> GetPossiblePaymentAmounts(string orderNumber)
        {
            Contract.Requires(!String.IsNullOrEmpty(orderNumber), "Invalid order number specified");

            Logger.Debug(String.Format("Requesting possible transaction amounts order with order number '{0}'", orderNumber));

            Product product = null;

            using (var repository = new ProductRepository())
            {
                product = repository.GetProductForOrderByOrderNumber(orderNumber);
            }

            List<decimal> prices =  new List<decimal>();

            if (product != null) 
            {
                if (product.Price != null)
                {
                    prices.Add(product.Price.Value);
                }

                if (product.AnnualPrice != null)
                {
                    prices.Add(product.AnnualPrice.Value);
                }
            }

            return prices;
        }

        public static PayingSubscriber AssociatePayingSubscriberWithOrder(int orderId, IpnMessage ipnObj)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified");
            Contract.Requires(ipnObj != null, "Invalid IPN message specified");
            Contract.Requires(!String.IsNullOrEmpty(ipnObj.SubscriberId), "Invalid IPN message. Subscriber is missing");

            Logger.Debug(String.Format("Associating a new paying subscriber '{0}' with an existing order with id={1}", ipnObj.SubscriberId, orderId));

            PayingSubscriber subscriber = null;

            using (var repository = new OrderRepository())
            {
                subscriber = repository.AddPayPalSubscriberToOrderByOrderId(orderId, ipnObj);
            }

            return subscriber;
        }

        public static OrderPo UpdateOrderByOrderId(Order existingOrder)
        {
            Contract.Requires(existingOrder != null, "Invalid order specified");
            Contract.Requires(existingOrder.OrderId > 0, "Invalid order id set on the specified order");

            Logger.Debug(String.Format("Requesting update for order with id={0} to status={1} with channel count={2}", existingOrder.OrderId, existingOrder.OrderStatusId, existingOrder.ChannelCount));

            OrderPo order = null;

            using (var repository = new OrderRepository())
            {
                order = repository.UpdateOrderById(existingOrder);
            }

            return order;
        }

        public static string CreateTrialSubscripionRequest(int productId, int userId, decimal price = 0)
        {

            CreateOrderBindingModel bindingModel = new CreateOrderBindingModel();
            bindingModel.UserId = userId;
            bindingModel.ProductId = productId;
            bindingModel.IsAnnual = false;
            bindingModel.IsUpgrade = false;
            UserPo userPo = UserManage.GetUserPoByUserId(userId);
            Product product = ProductManager.GetProductById(productId);
            var placedOrder = PlaceOrder(bindingModel);

            string subscribtioId = "M-" + RandomUtils.RandomString(15, true).ToString() + RandomUtils.RandomString(3, false);
            string optionSelection = placedOrder.OrderNumber;
            decimal ammount3;
            string result = "";
            if (price != product.Price && product.Price != 0)
            {
                ammount3 = price;
            }
            else
            {
                ammount3 = (decimal)product.Price;
            }
            using (var client = new WebClient())
            {

                var now = DateTime.Now;
                var dt = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, DateTimeKind.Local);
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
                string subscribtionDate = dt.ToLocalTime().ToString("HH:mm:ss MMM dd, yyyy", ci);
                //var values = new NameValueCollection();
                //values["MANUALtxn_type"] = "subscr_signup";
                //values["subscr_id"] = subscribtioId;
                //values["last_name"] = userPo.LastName;
                //values["option_selection1"] = optionSelection;
                //values["residence_country"] = "US";
                //values["mc_currency"] = "USD";
                //values["item_name"] = product.Name;
                //values["business"] = "paypal@strimm.com";
                //values["amount3"] = ammount3.ToString();
                //values["recurring"] = "1";
                //values["verify_sign"] = "AnYzd0W1hn6dtXih9KSVqNf9gGYtAS4q31a36b9D.S2u9ZBzlkH.TD15";
                //values["payer_status"] = "verified";
                //values["payer_email"] = userPo.Email;
                //values["first_name"] = userPo.FirstName;
                //values["receiver_email"] = "paypal@strimm.com";
                //values["payer_id"] = "CUSTOM";
                //values["option_name1"] = placedOrder.OrderNumber;
                //values["reattempt"] = "1";
                //values["item_number"] = product.Name;
                //values["subscr_date"] = subscribtionDate;
                //values["btn_id"] = product.SubscrMonthlyWithTrialButtonId;//get button id by 
                //values["charset"] = "UTF-8";
                //values["notify_version"] = "3.8";
                //values["period3"] = " 1 M";
                //values["mc_amount3"] = product.Price.ToString();
                //values["ipn_track_id"] = "MANUAL";
                string req = "MANUALtxn_type=subscr_signup" +
                             "&subscr_id=" + subscribtioId.ToString() +
                             "&last_name=" + userPo.LastName +
                             "&option_selection1=" + optionSelection +
                             "&residence_country=US" +
                             "&mc_currency=USD" +
                             "&item_name=" + product.Name +
                             "&business=" + ConfigurationManager.AppSettings["PaymentReceiverEmail"].ToString()+
                             "&amount3=" + ammount3.ToString() +
                             "&recurring=1" +
                             "&verify_sign=AnYzd0W1hn6dtXih9KSVqNf9gGYtAS4q31a36b9D.S2u9ZBzlkH.TD15" +
                             "&payer_status=verified" +
                             "&payer_email=" + userPo.Email +
                             "&first_name=" + userPo.FirstName +
                             "&receiver_email="+ConfigurationManager.AppSettings["PaymentReceiverEmail"].ToString()+
                             "&payer_id=CUSTOM" +
                             "&option_name1=" + placedOrder.OrderNumber.ToString() +
                             "&reattempt=1" +
                             "&item_number=" + product.Name +
                             "&subscr_date=" + subscribtionDate +
                             "&btn_id=" + product.SubscrMonthlyWithTrialButtonId +
                             "&charset=UTF-8" +
                             "&notify_version=3.8" +
                             "&period3=1M" +
                             "&mc_amount3=" + ammount3.ToString() +
                             "&ipn_track_id=MANUAL";
                return req;
                //try
                //{
                   
                //    byte[] buffer = System.Text.Encoding.ASCII.GetBytes(req);
                 
                //    HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create("http://localhost:49897/WebServices/UserService.asmx/IPN");
                
                //    WebReq.Method = "POST";
                 
                //    WebReq.ContentType = "application/x-www-form-urlencoded";
                    
                //    WebReq.ContentLength = buffer.Length;
                  
                //    Stream PostData = WebReq.GetRequestStream();
                   
                //    PostData.Write(buffer, 0, buffer.Length);
                //    PostData.Close();
                    
                //    HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                   
                //    Stream Answer = WebResp.GetResponseStream();
                //    StreamReader _Answer = new StreamReader(Answer);
                //    result = _Answer.ReadToEnd();
                //}

                //catch (WebException webex)
                //{
                //    WebResponse errResp = webex.Response;
                //    using (Stream respStream = errResp.GetResponseStream())
                //    {
                //        StreamReader reader = new StreamReader(respStream);
                //        string text = reader.ReadToEnd();
                //    }
                //}
               // return result.Trim() + "\n";
                //  string json = Newtonsoft.Json.JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);

                //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:49897/WebServices/UserService.asmx/IPN");
                //byte[] data = System.Text.Encoding.UTF8.GetBytes(req);
                //httpWebRequest.ContentLength = data.Length;

                //httpWebRequest.Accept = "text/plain";
                //httpWebRequest.Method = "POST";

                //Stream stm = httpWebRequest.GetRequestStream();
                //stm.Write(data, 0, data.Length);

                //try
                //{

                //    WebResponse response = httpWebRequest.GetResponse();
                //    Stream responseStream = response.GetResponseStream();
                //    result = responseStream.ToString();
                //}
                //catch (WebException webex)
                //{
                //    WebResponse errResp = webex.Response;
                //    using (Stream respStream = errResp.GetResponseStream())
                //    {
                //        StreamReader reader = new StreamReader(respStream);
                //        string text = reader.ReadToEnd();
                //    }
                //}



                //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                //{


                //    streamWriter.Write(req);
                //    streamWriter.Flush();
                //    streamWriter.Close();
                //}

                //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                //{
                //     result = streamReader.ReadToEnd();
                //}


            }
        }

        public static string CreateSubscripionRequest(int productId, int userId, decimal price = 0)
        {

            CreateOrderBindingModel bindingModel = new CreateOrderBindingModel();
            bindingModel.UserId = userId;
            bindingModel.ProductId = productId;
            bindingModel.IsAnnual = false;
            bindingModel.IsUpgrade = false;
           
            UserPo userPo = UserManage.GetUserPoByUserId(userId);
            Product product = ProductManager.GetProductById(productId);
            var placedOrder = PlaceOrder(bindingModel);

            string subscribtioId = "M-" + RandomUtils.RandomString(15, true).ToString() + RandomUtils.RandomString(3, false);
            string optionSelection = placedOrder.OrderNumber;
            decimal ammount3;
            string result = "";
            if (price != product.Price && product.Price != 0)
            {
                ammount3 = price;
            }
            else
            {
                ammount3 = (decimal)product.Price;
            }
            using (var client = new WebClient())
            {

                var now = DateTime.Now;
                var dt = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, DateTimeKind.Local);
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
                string subscribtionDate = dt.ToLocalTime().ToString("HH:mm:ss MMM dd, yyyy", ci);
                string req = "MANUALtransaction_subject=" + product.Name +
                             "&payment_date=" + subscribtionDate +
                             "&txn_type=subscr_payment" +
                             "&subscr_id="+ subscribtioId.ToString() +
                             "&last_name=" + userPo.LastName +
                             "&option_selection1=" + optionSelection +
                             "&option_selection2=" + optionSelection +
                             "&receipt_id=" + Guid.NewGuid().ToString() +
                             "&residence_country=US" +
                             "&item_name=" + product.Name +
                             "&payment_gross=" + ammount3.ToString() +
                             "&mc_currency=USD" +
                             "&business=" + ConfigurationManager.AppSettings["PaymentReceiverEmail"].ToString() +
                             "&payment_type= instant" +
                             "&protection_eligibility=Ineligible" +
                             "&verify_sign=AAPxn7kf6l0KjJ.ue6.r-xiQKAhhALEc6mseHgdwh31qdxAYNd-3aaan" +
                             "&payer_status=unverified" +
                             "&payer_email=" + userPo.Email +
                             "&txn_id=" + placedOrder.OrderNumber.ToString() +
                             "&receiver_email=" + ConfigurationManager.AppSettings["PaymentReceiverEmail"].ToString() +
                             "&first_name= Michael" +
                             "&option_name1=" + placedOrder.OrderNumber.ToString() +
                             "&payer_id=" + userPo.UserId +
                             "&receiver_id=KRRJL8PKU6PH6" +
                             "&item_number=" + product.Name +
                             "&payment_status=Completed" +
                             "&payment_fee=2.50" +
                             "&mc_fee=2.50" +
                             "&btn_id=" + product.SubscrMonthlyNoTrialButtonId +
                             "&mc_gross=" + ammount3.ToString() +
                             "&charset=UTF-8" +
                             "&notify_version=3.8" +
                             "&ipn_track_id=MANUAL";
              
                return req;
                


            }
        }


        public static PayingSubscriber GetPayingSubscriberByOrderId(int orderId)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified");

            Logger.Debug(String.Format("Requesting subscriber data for order with id={0}", orderId));

            PayingSubscriber subscriber = null;

            using (var repository = new OrderRepository())
            {
                subscriber = repository.GetPayingSubscriberForOrderByOrderId(orderId);
            }

            return subscriber;
        }

        public static OrderSubscriptionProfile GetOrderSubscriptionProfileByOrderId(int orderId)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified");

            Logger.Debug(String.Format("Requesting order subscription profile for order with id={0}", orderId));

            OrderSubscriptionProfile profile = null;

            using (var repository = new OrderRepository())
            {
                profile = repository.GetOrderSubscriptionProfileByOrderId(orderId);
            }

            return profile;
        }

        public static OrderPo UpdateOrderStatusById(OrderPo existingOrder)
        {
            Contract.Requires(existingOrder != null, "Invalid order specified");
            Contract.Requires(existingOrder.OrderId > 0, "Invalid order id set on the specified order");

            Logger.Debug(String.Format("Requesting update for order with id={0} to status={1}", existingOrder.OrderId, existingOrder.OrderStatusId));

            OrderPo order = null;

            using (var repository = new OrderRepository())
            {
                order = repository.UpdateOrderStatusById(existingOrder);
            }

            return order;
        }

        public static OrderTransactionPo CreateOrderTransaction(int orderId, int payingSubscriberId, IpnMessage ipnObj)
        {
            Contract.Requires(ipnObj != null, "Invalid ipn message specified");
            Contract.Requires(orderId > 0, "Invalid order id specified");
            Contract.Requires(payingSubscriberId > 0, "Invalid paying subscriber id specified");

            Logger.Debug(String.Format("Creating new order transaction for order with id={0} based on IPM message with id={1} received from subscriber with id={2}", orderId, ipnObj.IpnMessageId, payingSubscriberId));

            OrderTransactionPo transaction = null;

            using (var repository = new OrderRepository())
            {
                transaction = repository.AddOrderTransactionToOrderByIdWithGet(orderId, payingSubscriberId, ipnObj);
            }

            return transaction;
        }

        public static OrderSubscriptionProfile AssociateSubscriptionProfileWithOrder(int orderId, int payingSubscriberId, IpnMessage ipnObj)
        {
            Contract.Requires(ipnObj != null, "Invalid ipn message specified");
            Contract.Requires(orderId > 0, "Invalid order id specified");
            Contract.Requires(payingSubscriberId > 0, "Invalid paying subscriber id specified");

            Logger.Debug(String.Format("Associating subscription profile with order with id={0} based on IPM message with id={1} for paying subscriber id={2}", orderId, ipnObj.IpnMessageId, payingSubscriberId));

            OrderSubscriptionProfile profile = null;

            using (var repository = new OrderRepository())
            {
                profile = repository.AddOrderSubscriptionProfileToOrderByOrderId(orderId, payingSubscriberId, ipnObj);
            }

            return profile;
        }
        public static ProductSubscription CreateManualProductSubscription(int orderId, int productId, ProductSubscriptionStatusEnum status)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified");
            Contract.Requires(productId > 0, "Invalid product id specified");

            Logger.Debug(String.Format("Requesting to create a new product subscription for order with id={0}, product id={1} with status={2}", orderId, productId, status));

            ProductSubscription subscription = null;

            using (var productRepository = new ProductRepository())
            using (var orderRepository = new OrderRepository())
            {
                var product = productRepository.FindProductById(productId);
                var order = orderRepository.GetOrderPoById(orderId);

                if (product != null)
                {
                    DateTime? trialStartDate = order.TrialAllowed ? DateTime.Now : new Nullable<DateTime>();
                    DateTime subscriptionStartDate = order.TrialAllowed
                        ? trialStartDate.Value.AddDays(product.TrialPeriodInDays ?? 0)
                        : DateTime.Now;

                    subscription = orderRepository.AddProductSubscription(orderId, trialStartDate, subscriptionStartDate, status);
                }
            }
            UpdateProductSubscriptionStatus(orderId, ProductSubscriptionStatusEnum.Active);

            return subscription;
        }

        public static ProductSubscription CreateProductSubscription(int orderId, int productId, ProductSubscriptionStatusEnum status)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified");
            Contract.Requires(productId > 0, "Invalid product id specified");

            Logger.Debug(String.Format("Requesting to create a new product subscription for order with id={0}, product id={1} with status={2}", orderId, productId, status));

            ProductSubscription subscription = null;

            using (var productRepository = new ProductRepository())
            using (var orderRepository = new OrderRepository())
            {
                var product = productRepository.FindProductById(productId);
                var order = orderRepository.GetOrderPoById(orderId);

                if (product != null)
                {
                    DateTime? trialStartDate = order.TrialAllowed ? DateTime.Now : new Nullable<DateTime>();
                    DateTime subscriptionStartDate = order.TrialAllowed
                        ? trialStartDate.Value.AddDays(product.TrialPeriodInDays ?? 0)
                        : DateTime.Now;
                        
                    subscription = orderRepository.AddProductSubscription(orderId, trialStartDate, subscriptionStartDate, status);
                }
            }

            return subscription;
        }

        public static ProductSubscription UpdateProductSubscriptionStatus(int orderId, ProductSubscriptionStatusEnum status)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified");

            Logger.Debug(String.Format("Requesting to update existing product subscription for order with id={0} to status={1}", orderId, status));

            ProductSubscription subscription = null;

            using (var orderRepository = new OrderRepository())
            {
                subscription = orderRepository.GetProductSubscriptionByOrderId(orderId);

                if (subscription != null)
                {
                    subscription = orderRepository.UpdateProductSubscriptionStatusById(subscription.ProductSubscriptionId, status);
                }
            }

            return subscription;
        }

        public static OrderCancellation CancelOrder(string orderNumber)
        {
            Contract.Requires(!String.IsNullOrEmpty(orderNumber), "Invalid order number specified");

            OrderCancellation confirmation = new OrderCancellation() 
            {
                OrderNumber = orderNumber
            };

            using (var orderRepository = new OrderRepository())
            {
                var order = orderRepository.FindOrderByOrderNumber(orderNumber);

                if (order != null)
                {
                    order.OrderStatusId = (int)OrderStatusEnum.Canceled;
                    order = orderRepository.UpdateOrderStatusById(order);

                    if (order != null && order.OrderStatusId == (int)OrderStatusEnum.Canceled)
                    {
                        confirmation.IsSuccess = true;
                        confirmation.Message = "Order was successfully canceled.";
                    }
                    else
                    {
                        confirmation.IsSuccess = false;
                        confirmation.Message = "Unable to cancel existing order. Please contact customer support";
                    }
                }
                else
                {
                    confirmation.IsSuccess = false;
                    confirmation.Message = "Unable to cancel order. Order was not found. Please contact customer support";
                }
            }

            return confirmation;
        }

        public static PayingSubscriber GetPayingSubscriberByPayPalSubscriberId(string subscriberId)
        {
            Contract.Requires(!String.IsNullOrEmpty(subscriberId), "Invalid subscriber id specified");

            Logger.Debug(String.Format("Requesting to paying subscriber record using paypal subscriber id='{0}'", subscriberId));

            PayingSubscriber subscriber = null;

            using (var orderRepository = new OrderRepository())
            {
                subscriber = orderRepository.GetPayingSubscriberByPayPalSubscriberId(subscriberId);
            }

            return subscriber;
        }

        public static OrderPo GetOrderPoById(int orderId)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified");

            Logger.Debug(String.Format("Requesting order details with id={0}", orderId));

            OrderPo order = null;

            using (var orderRepository = new OrderRepository())
            {
                order = orderRepository.GetOrderPoById(orderId);
            }

            return order;
        }

        public static ProductSubscription GetProductSubscriptionByOrderId(int orderId)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified");

            Logger.Debug(String.Format("Requesting product subscription for order with id={0}", orderId));

            ProductSubscription subscription = null;

            using (var orderRepository = new OrderRepository())
            {
                subscription = orderRepository.GetProductSubscriptionByOrderId(orderId);
            }

            return subscription;
        }

        public static UserChannelEntitlement GetChannelUserEntitlementsById(int userId)
        {
            Contract.Requires(userId > 0, "Invalid user id specified");

            Logger.Debug(String.Format("Requesting user channel entitlements for user with id={0}", userId));

            UserChannelEntitlement entitlements = null;

            using (var orderRepository = new OrderRepository())
            {
                entitlements = orderRepository.GetUserChannelEntitlementsByUserId(userId);
            }

            return entitlements;
        }

        public static List<ProductSubscriptionPo> GetUserProductSubscriptionsByUserId(int userId)
        {
            Contract.Requires(userId > 0, "Invalid user id specified");

            Logger.Debug(String.Format("Requesting product subscriptions for user with id={0}", userId));

            List<ProductSubscriptionPo> subscriptions = null;

            using (var orderRepository = new OrderRepository())
            {
                subscriptions = orderRepository.GetProductSubscriptionsByUserId(userId);
            }

            return subscriptions;
        }

        public static OrderSubscriptionProfile UpdateBillingDetailsOnOrderSubscriptionProfileById(int orderSubscriptionProfileId, decimal? trialPaymentAmount, decimal? monthlyPaymentAmount, string trialPeriod, string billingCycle)
        {
            Contract.Requires(orderSubscriptionProfileId > 0, "Invalid order subscription profile id specified");

            Logger.Debug(String.Format("Requesting update of billing information on order subscription profile with id={0}, initial payment={1}, cycle payment={2}, trial period '{3}', billing cycle='{4}'", orderSubscriptionProfileId, trialPaymentAmount, monthlyPaymentAmount, trialPeriod, billingCycle));

            OrderSubscriptionProfile profile = null;

            using (var orderRepository = new OrderRepository())
            {
                profile = orderRepository.UpdateBillingDetailsOnOrderSubscriptionProfileById(orderSubscriptionProfileId, trialPaymentAmount, monthlyPaymentAmount, trialPeriod, billingCycle);
            }

            return profile;
        }

        public static List<ProductSubscriptionPo> GetActiveProductSubscriptionsByUserId(int userId)
        {
            Contract.Requires(userId > 0, "Invalid user id specified");

            Logger.Debug(String.Format("Requesting product subscriptions for user with id={0}", userId));

            List<ProductSubscriptionPo> subscriptions = null;

            using (var orderRepository = new OrderRepository())
            {
                subscriptions = orderRepository.GetProductSubscriptionsByUserId(userId);
                if (subscriptions != null)
                {
                    subscriptions = subscriptions.Where(x => x.ProductSubscriptionStatusId == (int)ProductSubscriptionStatusEnum.Active ||
                                             x.ProductSubscriptionStatusId == (int)ProductSubscriptionStatusEnum.InTrial ||
                                             x.ProductSubscriptionStatusId == (int)ProductSubscriptionStatusEnum.OnHold).ToList();
                }
            }

            return subscriptions;
        }

        public static List<OrderPo> GetActiveOrdersByUserId(int userId)
        {
            Contract.Requires(userId > 0, "Invalid user id specified");

            Logger.Debug(String.Format("Requesting active orders for user with id={0}", userId));

            List<OrderPo> orders = null;

            using (var orderRepository = new OrderRepository())
            {
                orders = orderRepository.GetOrderPosById(userId);
                if (orders != null)
                {
                    orders = orders.Where(x => x.OrderStatusId == (int)OrderStatusEnum.Active ||
                                      x.OrderStatusId == (int)OrderStatusEnum.InTrial ||
                                      x.OrderStatusId == (int)OrderStatusEnum.PendingPaymentResolution).ToList();
                }
            }

            return orders;
        }

        public static void UpdateUserChannelsSettingsBasedOnCanceledProduct(UserPo user, OrderPo canceledOrder, ProductSubscription canceledProductSubscription)
        {
            var activeProductSubscriptions = OrderManager.GetActiveProductSubscriptionsByUserId(user.UserId);

            int canceledProductId = canceledOrder.ProductId;

            var userActiveOrders = OrderManager.GetActiveOrdersByUserId(user.UserId);
            var allChannels = ChannelManage.GetChannelTubesForUser(user.UserId);

            bool allowCustomBranding = true;

            // If the order being canceled was for professional package and there are no other professional packages
            // purchase by user which are stil active, then reset user settings
            if (canceledOrder.ProductId == (int)ProductEnum.ProfessionalSubscription && !userActiveOrders.Any(x => x.ProductId == (int)ProductEnum.ProfessionalSubscription))
            {
                user.MatureContentAllowed = false;
                user.PrivateVideoModeEnabled = false;

                allowCustomBranding = false;

                UserManage.UpdateUser(user);
            }

            if (activeProductSubscriptions == null || activeProductSubscriptions.Count() == 0)
            {
                // Clear everything, user does not have any active subscription and is not entitled 
                // to anything
                allChannels.ForEach(x =>
                {
                    x.MuteOnStartup = false;
                    x.ChannelPassword = null;
                    x.CustomLabel = null;
                    x.UserDomain = null;
                    x.EmbedEnabled = false;
                    x.IsWhiteLabeled = false;

                    if (canceledOrder.ProductId == (int)ProductEnum.ProfessionalSubscription)
                    {
                        x.MatureContentEnabled = false;
                        x.EmbedOnlyModeEnabled = false;
                        x.CustomPlayerControlsEnabled = false;
                    } 
                    
                    ChannelManage.UpdateChannelTube(x);

                    Logger.Debug(String.Format("Cleared subscription details and turned off embedding for channel with id={0} for user with id={1} because order with id={2} was canceled",
                        x.ChannelTubeId, user.UserId, canceledOrder.OrderId));

                    ChannelManage.DeleteSubscriberDomain(x.UserId, x.ChannelTubeId);

                    Logger.Debug(String.Format("Cleared subscriber domain for channel with id={0} for user with id={1} because order with id={2} was canceled",
                        x.ChannelTubeId, user.UserId, canceledOrder.OrderId));

                });
            }
            else
            {
                switch (canceledProductId)
                {
                    case (int)ProductEnum.BasicSubscription:
                        {
                            // If there are no active orders for higher subscriptions, then there is
                            // only orders for the same basic subscription. In this case, take one of the embedded
                            // channels and turn off the embedding options on the channel
                            if (!userActiveOrders.Any(x => x.ProductId > canceledProductId))
                            {
                                if (allChannels != null)
                                {
                                    var lastChannel = allChannels.Where(x => x.EmbedEnabled).ToList().OrderByDescending(x => x.ChannelTubeId).Take(1).FirstOrDefault();
                                    if (lastChannel != null)
                                    {
                                        lastChannel.EmbedEnabled = false;
                                        ChannelManage.UpdateChannelTube(lastChannel);

                                        Logger.Debug(String.Format("Turned off embedding for channel with id={0} for user with id={1} because basic subscription/order with id={2} was canceled and no other active higher level subscription exists",
                                            lastChannel.ChannelTubeId, user.UserId, canceledOrder.OrderId));

                                        ChannelManage.DeleteSubscriberDomain(user.UserId, lastChannel.ChannelTubeId);

                                        Logger.Debug(String.Format("Cleared subscriber domain for channel with id={0} for user with id={1} because order with id={2} was canceled",
                                            lastChannel.ChannelTubeId, user.UserId, canceledOrder.OrderId));
                                    }
                                }
                            }
                            break;
                        }
                    case (int)ProductEnum.IntermediateSubscription:
                        {
                            if (!userActiveOrders.Any(x => x.ProductId >= canceledProductId))
                            {
                                if (allChannels != null)
                                {
                                    var embeddedChannels = allChannels.Where(x => x.EmbedEnabled == true).OrderByDescending(x => x.ChannelTubeId).Take(allChannels.Count - userActiveOrders.Count).ToList();
                                    embeddedChannels.ForEach(x =>
                                    {
                                        x.EmbedEnabled = false;
                                        x.CustomPlayerControlsEnabled = false;
                                        x.ChannelPassword = null;

                                        ChannelManage.UpdateChannelTube(x);

                                        Logger.Debug(String.Format("Turned off embedding for channel with id={0} for user with id={1} because standard subscription/order with id={2} was canceled and no other active standard or higher level subscription exists",
                                            x.ChannelTubeId, user.UserId, canceledOrder.OrderId));

                                        ChannelManage.DeleteSubscriberDomain(user.UserId, x.ChannelTubeId);

                                        Logger.Debug(String.Format("Cleared subscriber domain for channel with id={0} for user with id={1} because order with id={2} was canceled",
                                            x.ChannelTubeId, user.UserId, canceledOrder.OrderId));

                                    });
                                }
                            }

                            break;
                        }
                    case (int)ProductEnum.StandardSubscription:
                        {
                            if (!userActiveOrders.Any(x => x.ProductId >= canceledProductId))
                            {
                                if (allChannels != null)
                                {
                                    var whiteLabeledChannels = allChannels.Where(x => x.IsWhiteLabeled == true).ToList().OrderByDescending(x => x.ChannelTubeId).ToList();
                                    whiteLabeledChannels.ForEach(x =>
                                    {
                                        x.IsWhiteLabeled = false;
                                        ChannelManage.UpdateChannelTube(x);

                                        Logger.Debug(String.Format("Turned off white labeling for channel with id={0} for user with id={1} because standard subscription/order with id={2} was canceled and no other active standard subscription was purchased",
                                            x.ChannelTubeId, user.UserId, canceledOrder.OrderId));
                                    });

                                    var embeddedChannels = allChannels.Where(x => x.EmbedEnabled == true).OrderByDescending(x => x.ChannelTubeId).Take(allChannels.Count - userActiveOrders.Count).ToList();
                                    embeddedChannels.ForEach(x =>
                                    {
                                        x.EmbedEnabled = false;
                                        ChannelManage.UpdateChannelTube(x);

                                        Logger.Debug(String.Format("Turned off embedding for channel with id={0} for user with id={1} because standard subscription/order with id={2} was canceled and no other active standard or higher level subscription exists",
                                            x.ChannelTubeId, user.UserId, canceledOrder.OrderId));

                                        ChannelManage.DeleteSubscriberDomain(user.UserId, x.ChannelTubeId);

                                        Logger.Debug(String.Format("Cleared subscriber domain for channel with id={0} for user with id={1} because order with id={2} was canceled",
                                            x.ChannelTubeId, user.UserId, canceledOrder.OrderId));

                                    });
                                }
                            }

                            break;
                        }
                    case (int)ProductEnum.CustomSubscription:
                    case (int)ProductEnum.ProfessionalSubscription:
                        {
                            if (!userActiveOrders.Any(x => x.ProductId == (int)ProductEnum.CustomSubscription || x.ProductId == (int)ProductEnum.ProfessionalSubscription))
                            {
                                if (allChannels != null)
                                {
                                    var whiteLabeledChannels = allChannels.Where(x => !String.IsNullOrEmpty(x.ChannelPassword) || x.MuteOnStartup || !String.IsNullOrEmpty(x.CustomLabel)).ToList().OrderByDescending(x => x.ChannelTubeId).ToList();
                                    whiteLabeledChannels.ForEach(x =>
                                    {
                                        x.CustomLabel = null;
                                        x.ChannelPassword = null;
                                        x.MuteOnStartup = false;
                                        x.MatureContentEnabled = false;

                                        ChannelManage.UpdateChannelTube(x);

                                        Logger.Debug(String.Format("Turned off custom labeling, password protection, mute on startup for channel with id={0} for user with id={1} because professional subscription/order with id={2} was canceled and no other professional or custom subscription exists",
                                            x.ChannelTubeId, user.UserId, canceledOrder.OrderId));
                                    });
                                }
                            }

                            if (!userActiveOrders.Any(x => x.ProductId == (int)ProductEnum.StandardSubscription))
                            {
                                var embeddedChannels = allChannels.Where(x => x.EmbedEnabled == true).OrderByDescending(x => x.ChannelTubeId).Take(allChannels.Count - userActiveOrders.Count).ToList();
                                embeddedChannels.ForEach(x =>
                                {
                                    x.EmbedEnabled = false;
                                    x.IsWhiteLabeled = false;

                                    ChannelManage.UpdateChannelTube(x);

                                    Logger.Debug(String.Format("Turned off white labeling & embedding for channel with id={0} for user with id={1} because professional subscription/order with id={2} was canceled and no standard subscription exists",
                                        x.ChannelTubeId, user.UserId, canceledOrder.OrderId));

                                    ChannelManage.DeleteSubscriberDomain(user.UserId, x.ChannelTubeId);

                                    Logger.Debug(String.Format("Cleared subscriber domain for channel with id={0} for user with id={1} because order with id={2} was canceled",
                                        x.ChannelTubeId, user.UserId, canceledOrder.OrderId));

                                });
                            }

                            break;
                        }
                    default:
                        Logger.Debug(String.Format("Invalid product id={0} set on canceled order. Unable to determine which subscription features should be turned off and from which channels", canceledProductId));
                        break;
                }
            }
        }

        public static List<OrderPo> GetExistingOrdersPendingResolution()
        {
            Logger.Debug("Requesting orders pending resolution due to failed payment that expire today");

            List<OrderPo> orders = null;

            using (var orderRepository = new OrderRepository())
            {
                orders = orderRepository.GetPendingOrdersExpiringOn(DateTime.Today);
            }

            return orders;
        }

        public static ProductSubscriptionStatistics GetProductSubscribtionsStatisticsByDateRange(DateTime? startDate, DateTime? endDate)
        {
            ProductSubscriptionStatistics orders = null;

            using (var orderRepository = new OrderRepository())
            {
                orders = orderRepository.GetProductSubscriptionsStatisticsByDateRange(startDate, endDate);
              
            }

            return orders;
        }

        public static List<OrderStatistics> GetOrderStatisticsByDateRange(DateTime? startDate, DateTime? endDate)
        {
            List<OrderStatistics> orderList = new List<OrderStatistics>();

            using (var orderRepository = new OrderRepository())
            {
                orderList = orderRepository.GetOrderStatisticsByDateRange(startDate, endDate);

            }

            return orderList;
        }

        public static List<OrderTransactionPo> GetOrderTransactionPosByOrderId(int orderId)
        {
            List<OrderTransactionPo> orderList = new List<OrderTransactionPo>();
            using(var orderRepostory = new OrderRepository())
            {
                orderList = orderRepostory.GetOrderTransactionPosByOrderId(orderId);
                
            }

            return orderList;
        }

        public static List<ChannelTubePo> GetChannelTubePosByOrderId(int orderId)
        {
            List<ChannelTubePo> channelList = new List<ChannelTubePo>();
            using (var orderRepostory = new OrderRepository())
            {
                channelList = orderRepostory.GetChannelTubePosByOrderId(orderId);
               

            }

            return channelList;
           
        }

        public static List<ChannelCategoryPo> GetChannelTubeCountsByCategoryForExistingSubscriptions()
        {
            List<ChannelCategoryPo> channelList = new List<ChannelCategoryPo>();
            using (var orderRepostory = new OrderRepository())
            {
                channelList = orderRepostory.GetChannelTubeCountsByCategoryForExistingSubscriptions();


            }

            return channelList;

        }

        public static int GetPriorOrderCountByUserId(int userId)
        {
            int count = 0;

            using (var orderRepository = new OrderRepository())
            {
                count = orderRepository.GetPriorOrderCountByUserId(userId);
            }

            return count;
        }

        public static ProductPo GetAvailableProductOptionsByProductIdAndUserId(int userId, int productId, bool isAnnual)
        {
            ProductPo product = null;

            using (var productRepository = new ProductRepository())
            {
                product = productRepository.GetAvailableProductOptionsByProductIdAndUserId(productId, userId, isAnnual);
            }

            return product;
        }

        public static List<ProductPo> GetAvailableProducts()
        {
            List<ProductPo> products = new List<ProductPo>();

            using (var productRepository = new ProductRepository())
            {
                products = productRepository.GetAvailableProducts();
            }

            return products;
        }
    }
}

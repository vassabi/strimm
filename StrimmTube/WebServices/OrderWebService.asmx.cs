using log4net;
using Strimm.Model;
using Strimm.Model.Order;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Services;

namespace StrimmTube.WebServices
{
    /// <summary>
    /// Summary description for OrderWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class OrderWebService : System.Web.Services.WebService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(OrderWebService));

        private static readonly string RECEIVER_EMAIL;

        //static OrderWebService()
        //{
        //    RECEIVER_EMAIL = "dk-facilitator@strimm.com"; // ConfigurationManager.AppSettings["PaymentReceiverEmail"].ToString();
        //}

        [WebMethod]
        public OrderConfirmation PlaceOrder(CreateOrderBindingModel model)
        {
            Logger.Info("Handling a new order request");

            if (model == null)
            {
                return (new OrderConfirmation() { IsSuccess = false, Message = "Invalid request received." });
            }

            if (model.UserId <= 0 || model.ProductId <= 0)
            {
                return (new OrderConfirmation() { IsSuccess = false, Message = "Invalid user or product details specified." });
            }

            Logger.Debug(String.Format("New order request was submitted for product with id={0} by user with id={1}", model.ProductId, model.UserId));

            OrderConfirmation confirmation = null;

            try
            {
                confirmation = OrderManager.PlaceOrder(model);

                Logger.Debug(String.Format("Order was successfully created. New order number is '{0}'", confirmation.OrderNumber));
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while creating new order", ex);

                confirmation = new OrderConfirmation()
                {
                    IsSuccess = false,
                    Message = "Error occured while creating new order. Please try again"
                };
            }

            return confirmation;
        }

        [WebMethod]
        public void IPN()
        {
            Context.Request.InputStream.Seek(0, SeekOrigin.Begin);

            string payload = new StreamReader(Context.Request.InputStream).ReadToEnd();
            payload = WebUtility.UrlDecode(payload);

            var segments = payload.Split('&').Select(x => new KeyValuePair<string, string>(x.Split('=')[0], x.Split('=')[1])).ToList();
            segments.Insert(0, new KeyValuePair<string, string>("cmd", "_notify-validate"));

            // if you want to use the PayPal sandbox change this from false to true
            string response = OrderManager.GetPayPalResponse(true, String.Format("cmd=_notify-validate&{0}", payload));//, segments);

            if (response == "VERIFIED")
            {
                //check the payment_status is Completed
                //check that txn_id has not been previously processed
                //check that receiver_email is your Primary PayPal email
                //check that payment_amount/payment_currency are correct 
                var ipnObj = new IpnMessage(payload);

                var existingIpnMessage = OrderManager.SaveIpnMessage(ipnObj);

                var priorTxn = OrderManager.GetPriorTransactionBySubscriberId(ipnObj.SubscriberId);
                var possibleTxnAmounts = OrderManager.GetPossiblePaymentAmounts(ipnObj.SubscriberId);

                if (ipnObj.PayerStatus == "Completed" &&
                    priorTxn == null &&
                    ipnObj.ReceiverEmail == RECEIVER_EMAIL &&
                    possibleTxnAmounts.Any(x => x == ipnObj.Amount1))
                {
                    Logger.Debug(String.Format("Processing a valid IPN notification message from PayPal with TxnId='{0}' and for Order '{1}'", existingIpnMessage.SubscriberId, existingIpnMessage.OptionSelection1));


                }
                else
                {
                    // Invalid request recieved.
                }

            }
        }

        [WebMethod]

        public ProductSubscriptionStatistics GetProductSubscribtionsStatisticsByDateRange(string startDate, string endDate)
        {
            DateTime? startRange = null;
            DateTime? endRange = null;
            string stringClientDateTimeFormat = "mm/dd/yyyy";
            if (startDate == "null")
            {
                startRange = null;
            }
            else
            {
                startRange = DateTime.ParseExact(startDate, stringClientDateTimeFormat, CultureInfo.InvariantCulture);
            }
            if (endDate == "null")
            {
                endRange = null;
            }
            else
            {
                endRange = DateTime.ParseExact(endDate, stringClientDateTimeFormat, CultureInfo.InvariantCulture);
            }
            return OrderManager.GetProductSubscribtionsStatisticsByDateRange(startRange, endRange);
        }

        [WebMethod]

        public List<OrderStatistics> GetOrderStatisticsByDateRange(string startDate, string endDate)
        {
            DateTime? startRange = null;
            DateTime? endRange = null;
            string stringClientDateTimeFormat = "mm/dd/yyyy";
            if (startDate == "null")
            {
                startRange = null;
            }
            else
            {
                startRange = DateTime.ParseExact(startDate, stringClientDateTimeFormat, CultureInfo.InvariantCulture);
            }
            if (endDate == "null")
            {
                endRange = null;
            }
            else
            {
                endRange = DateTime.ParseExact(endDate, stringClientDateTimeFormat, CultureInfo.InvariantCulture);
            }
            return OrderManager.GetOrderStatisticsByDateRange(startRange, endRange);
        }

        [WebMethod]
        public List<OrderTransactionPo> GetOrderTransactionPosByOrderId(int orderId)
        {
            
            return OrderManager.GetOrderTransactionPosByOrderId(orderId);
        }
        [WebMethod]
        public List<OrderPo>GetOrderPosByUserIdForAdmin(int userId)
        {
            List<OrderPo> orderList = OrderManager.GetOrderPosByUserIdForAdmin(userId);
            List<OrderPo> disticntList = new List<OrderPo>();
            disticntList=orderList.OrderBy(r=>r.ProductId).Distinct().ToList();
            disticntList = disticntList.GroupBy(r => r.OrderNumber).Select(o=>o.First()).ToList();
            return disticntList;
        }
        [WebMethod]
        public OrderPo UpdateOrderById(OrderPo orderPo)
        {
            int productIdBeforeUpdate = OrderManager.GetOrderPoById(orderPo.OrderId).ProductId;
            OrderPo updatedOrder = OrderManager.UpdateOrderByOrderId(orderPo);
            if(updatedOrder!=null)
            {
                if(updatedOrder.OrderStatusId>=5&&orderPo.OrderStatusId>=5)
                {
                    
                    List<ChannelTube> channelList = ChannelManage.GetChannelTubeByUserIdForAdmin(updatedOrder.UserId);
                    List<Product> productList = ProductManager.GetAllProducts();
                  var channelEntitlements =  ChannelManage.GetUserChannelEntitlementsByUserId(updatedOrder.UserId);
                  
                  
                    
                    if(channelList.Count!=0)
                    {
                        Product productOfOrderBeforeUpdate = productList.Where(p => p.ProductId == productIdBeforeUpdate).FirstOrDefault();
                        int productChannelCountBeforeUpdate = productOfOrderBeforeUpdate.MaxChannelCount;
                        List<ChannelTube> embedEnabledChannels = new List<ChannelTube>();
                        if (channelEntitlements.ChannelsAvailableToEmbedCount > 1)
                        {
                            embedEnabledChannels = channelList.Where(c => c.EmbedEnabled == true).Skip(1).ToList();
                        }
                        else
                        {
                            embedEnabledChannels = channelList.Where(c => c.EmbedEnabled == true).ToList();
                        }
                       
                        if(productChannelCountBeforeUpdate > embedEnabledChannels.Count)
                        {
                            if(embedEnabledChannels.Count!=0)
                            {
                                //remove from channel SubscriberDomain
                                foreach (var channel in embedEnabledChannels)
                                {
                                    channel.EmbedEnabled = false;
                                   var isDeleted = UserManage.DeleteUserDomainByChannelIdAndUserId(channel.ChannelTubeId, updatedOrder.UserId);
                                    ChannelManage.UpdateChannelTube(channel);
                                }
                            }
                          
                        }
                        else
                        {
                            List<ChannelTube> channelsToDisableEmbedding = embedEnabledChannels.OrderBy(x => x.CreatedDate).Take(productChannelCountBeforeUpdate).ToList();
                            if (channelsToDisableEmbedding.Count != 0)
                            {
                                foreach (var channel in channelsToDisableEmbedding)
                                {
                                    var isDeleted = UserManage.DeleteUserDomainByChannelIdAndUserId(channel.ChannelTubeId, updatedOrder.UserId);
                                    channel.EmbedEnabled = false;
                                    ChannelManage.UpdateChannelTube(channel);
                                }
                            }
                        }
                    }
                    

                }
            }
            
            return updatedOrder;
        }
        [WebMethod]
        public List<ChannelTubePo> GetChannelTubePosByOrderId(int orderId)
        {
            return OrderManager.GetChannelTubePosByOrderId(orderId);
        }

        [WebMethod]
        public List<ChannelCategoryPo> GetChannelTubeCountsByCategoryForExistingSubscriptions()
        {
            return OrderManager.GetChannelTubeCountsByCategoryForExistingSubscriptions();
        }

        [WebMethod]
        public int GetPriorOrderCountByUserId(int userId)
        {
            return OrderManager.GetPriorOrderCountByUserId(userId);
        }

        [WebMethod]
        public ProductPo GetProductOptionsById(int userId, int productId, bool isAnnual)
        {
            return OrderManager.GetAvailableProductOptionsByProductIdAndUserId(userId, productId, isAnnual);
        }

        [WebMethod]
        public List<ProductPo> GetAvailableProducts()
        {
            return OrderManager.GetAvailableProducts();
        }

        [WebMethod]
        public string CreateTrialSubscripionRequest(int productId, int userId, decimal price = 0)
        {
           return OrderManager.CreateTrialSubscripionRequest(productId, userId, price);
        }
        [WebMethod]
        public string CreateSubscripionRequest(int productId, int userId, decimal price = 0)
        {
            return OrderManager.CreateSubscripionRequest(productId, userId, price);
        }
        [WebMethod]
        public List<Product>GetProducts()
        {
            return ProductManager.GetAllProducts();
        }
    }

}


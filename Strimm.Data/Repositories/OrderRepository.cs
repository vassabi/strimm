using log4net;
using Strimm.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Strimm.Model.Order;
using System.Diagnostics.Contracts;
using System.Data;
using Strimm.Model;
using Strimm.Model.Projections;

namespace Strimm.Data.Repositories
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(OrderRepository));

        public OrderRepository()
            : base()
        {

        }

        public OrderPo CreateOrder(int userId, string firstName, string lastName, string street, string city, string state, string countryCode, int productId, string orderNumber, string description, bool isTrialAllowed, bool isAnnual, bool isUpgrade)
        {
            Contract.Requires(userId > 0, "User id should be greater then 0");
            Contract.Requires(productId > 0, "Product id should be greater then 0");

            OrderPo order = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<OrderPo>("strimm.InsertOrder", new { 
                                                    UserId = userId,
                                                    FirstName = firstName,
                                                    LastName = lastName,
                                                    Street = street,
                                                    City = city,
                                                    State = state,
                                                    CountryCode = countryCode,
                                                    ProductId = productId,
                                                    OrderNumber = orderNumber,
                                                    Description = description,
                                                    IsTrialAllowed = isTrialAllowed,
                                                    IsAnnual = isAnnual,
                                                    IsUpgrade = isUpgrade
                                                }, 
                                                null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                order = results.FirstOrDefault();
            }

            return order;
        }

        public OrderPo FindOrderByOrderNumber(string orderNumber)
        {
            Contract.Requires(!String.IsNullOrEmpty(orderNumber), "Invalid order number specified");

            OrderPo order = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<OrderPo>("strimm.GetOrderByOrderNumber", new { OrderNumber = orderNumber }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                order = results.FirstOrDefault();
            }

            return order;
        }

        public IpnMessage SavePayPalIpnMessage(IpnMessage ipnMsg)
        {
            Contract.Requires(ipnMsg != null, "Invalid data specified on request");
            Contract.Requires(!String.IsNullOrEmpty(ipnMsg.SubscriberId), "Invalid subscriber id specified on paypal IPN message");

            IpnMessage message = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<IpnMessage>("strimm.AddSubscriptionIpnMessage", new
                {
	                IpnTrackingId = ipnMsg.IpnTrackingId,
	                RawMessage = ipnMsg.RawMessage,
	                BtnId = ipnMsg.BtnId,
	                TxnType = ipnMsg.TxnType,
	                ItemName = ipnMsg.ItemName,
	                ItemNumber = ipnMsg.ItemNumber,
	                OptionName1 = ipnMsg.OptionName1,
	                OptionSelection1 = ipnMsg.OptionSelection1,
	                PayerId = ipnMsg.PayerId,
	                FirstName = ipnMsg.FirstName,
	                LastName = ipnMsg.LastName,
	                PayerStatus = ipnMsg.PayerStatus,
	                BusinessEmail = ipnMsg.BusinessEmail,
	                PayerEmail = ipnMsg.PayerEmail,
	                ResidenceCountry = ipnMsg.ResidenceCountry,
	                ReceiverEmail = ipnMsg.ReceiverEmail,
                    Amount1 = ipnMsg.Amount1 ?? 0,
                    Amount3 = ipnMsg.Amount3 ?? 0,
                    McAmount1 = ipnMsg.McAmount1 ?? 0,
                    McAmount3 = ipnMsg.McAmount3 ?? 0,
	                Period1 = ipnMsg.Period1,
	                Period3 = ipnMsg.Period3,
	                Reattempt = ipnMsg.Reattempt,
	                Recurring = ipnMsg.RecurTimes,
	                SubscriptionDate = ipnMsg.SubscriptionDate,
	                SubscriberId = ipnMsg.SubscriberId,
	                McCurrency = ipnMsg.McCurrency,
	                NotifyVersion = ipnMsg.NotifyVersion,
	                VerifySign = ipnMsg.VerifySign,
	                TestIpn = ipnMsg.TestIpn,
                    PaymentStatus = ipnMsg.PaymentStatus,
                    PaymentFee = ipnMsg.PaymentFee ?? 0,
                    McFee = ipnMsg.McFee ?? 0,
                    McGross = ipnMsg.McGross ?? 0,
                    ReceiverId = ipnMsg.ReceiverId,
                    TxnId = ipnMsg.TxnId,
                    ProtectionEligibility = ipnMsg.ProtectionEligibility,
                    PaymentType = ipnMsg.PaymentType,
                    PaymentGross = ipnMsg.PaymentGross ?? 0,
                    PaymentDate = ipnMsg.PaymentDate,
                    TransactionSubject = ipnMsg.TransactionSubject,
                    OptionName2 = ipnMsg.OptionName2,
                    OptionSelection2 = ipnMsg.OptionSelection2
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                message = results.FirstOrDefault();
            }

            return message;
        }

        public IpnMessage GetIpnMessageBySubscriberId(string subscriberId)
        {
            Contract.Requires(!String.IsNullOrEmpty(subscriberId), "Invalid subscriber id specified.");

            IpnMessage message = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<IpnMessage>("strimm.GetIpnMessageBySubscriberId", new { SubscriberId = subscriberId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                message = results.FirstOrDefault();
            }

            return message;
        }

        public IpnMessage GetIpnMessageById(int ipnMessageId)
        {
            Contract.Requires(ipnMessageId > 0, "Invalid ipn message id specified.");

            IpnMessage message = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<IpnMessage>("strimm.GetSubscriptionIpnMessageById", new { IpnMessageId = ipnMessageId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                message = results.FirstOrDefault();
            }

            return message;
        }

        public List<OrderTransactionPo> GetOrderTransactionsByIpnMessageSubscriberId(string subscriberId)
        {
            Contract.Requires(!String.IsNullOrEmpty(subscriberId), "Invalid subscriber id specified.");

            List<OrderTransactionPo> transactions = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                transactions = this.StrimmDbConnection.Query<OrderTransactionPo>("strimm.GetOrderTransactionsPoByIpnMessageSubscriberId", new { SubscriberId = subscriberId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
            }

            return transactions;
        }

        public PayingSubscriber AddPayPalSubscriberToOrderByOrderId(int orderId, IpnMessage ipnObj)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified");
            Contract.Requires(ipnObj != null, "Invalid IPN message specified");
            Contract.Requires(!String.IsNullOrEmpty(ipnObj.SubscriberId), "Invalid IPN message. Subscriber is missing");

            Logger.Debug(String.Format("Associating a new paying subscriber '{0}' with an existing order with id={1}", ipnObj.SubscriberId, orderId));

            PayingSubscriber subscriber = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<PayingSubscriber>("strimm.AddPayPalSubscriberToOrderByOrderId", new { 
                                    OrderId = orderId,
                                    IpnMessageId = ipnObj.IpnMessageId,
                                    FirstName = ipnObj.FirstName,
                                    LastName = ipnObj.LastName,
                                    PayPalSubscriberId = ipnObj.SubscriberId,
                                    PayPalPayerId = ipnObj.PayerId,
                                    PayPalStatus = ipnObj.PayerStatus,
                                    PayPalBusinessEmail = ipnObj.BusinessEmail,
                                    PayPalPayerEmail = ipnObj.PayerEmail,
                                    PayPalReceiverEmail = ipnObj.ReceiverEmail,
                                    PayPalResidenceCountry = ipnObj.ResidenceCountry,
                                    PayPalSubscriptionDate = ipnObj.SubscriptionDate
                                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                subscriber = results.FirstOrDefault();
            }

            return subscriber;
        }

        public OrderPo UpdateOrderById(Order existingOrder)
        {
            Contract.Requires(existingOrder != null, "Invalid order specified");
            Contract.Requires(existingOrder.OrderId > 0, "Invalid order id specified");

            Logger.Debug(String.Format("Updating order with id={0}", existingOrder.OrderId));

            OrderPo order = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<OrderPo>("strimm.UpdateOrderByIdWithGet", new
                {
                    OrderId = existingOrder.OrderId,
                    OrderStatusId = existingOrder.OrderStatusId,
                    ChannelCount = existingOrder.ChannelCount,
                    OrderExpirationDate = existingOrder.OrderExpirationDate
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                order = results.FirstOrDefault();
            }

            return order;
        }

        public OrderPo UpdateOrderStatusById(Order existingOrder)
        {
            Contract.Requires(existingOrder != null, "Invalid order specified");
            Contract.Requires(existingOrder.OrderId > 0, "Invalid order id specified");

            Logger.Debug(String.Format("Updating order with id={0}", existingOrder.OrderId));

            OrderPo order = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<OrderPo>("strimm.UpdateOrderStatusByIdWithGet", new
                {
                    OrderId = existingOrder.OrderId,
                    OrderStatusId = existingOrder.OrderStatusId
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                order = results.FirstOrDefault();
            }

            return order;
        }

        public PayingSubscriber GetPayingSubscriberForOrderByOrderId(int orderId)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified");

            Logger.Debug(String.Format("Retrieving subscriber for order with id={0}", orderId));

            PayingSubscriber subscriber = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<PayingSubscriber>("strimm.GetPayingSubscriberForOrderByOrderId", new
                {
                    OrderId = orderId
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                subscriber = results.FirstOrDefault();
            }

            return subscriber;
        }

        public OrderTransactionPo AddOrderTransactionToOrderByIdWithGet(int orderId, int payingSubscriberId, IpnMessage ipnObj)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified");
            Contract.Requires(ipnObj != null, "Invalid IPN message specified");
            Contract.Requires(payingSubscriberId > 0, "Invalid paying subscriber id specified");

            Logger.Debug(String.Format("Adding order transaction for order with id={0} made by subscriber with id={1} recieved in IPN message with id={2}", orderId, payingSubscriberId, ipnObj.IpnMessageId));

            OrderTransactionPo transaction = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<OrderTransactionPo>("strimm.AddOrderTransactionToOrderByOrderIdWithGet", new
                {
                    OrderId = orderId,
                    OrderTransactionTypeId = (int)OrderTransactionTypeEnum.ReoccuringPayment,
                    OrderTransactionStatusId = (int)OrderTransactionStatusEnum.Completed,
                    TransactionPaymentMethodId = (int)TransactionPaymentMethodEnum.PayPal_Instant,
                    IpnMessageId = ipnObj.IpnMessageId,
                    PayingSubscriberId = payingSubscriberId,
                    TxnId = ipnObj.TxnId,
                    PaymentStatus = ipnObj.PaymentStatus,
                    PaymentDate = ipnObj.PaymentDate,
                    PaymentNumber = ipnObj.PaymentNumber,
                    PaymentGross = ipnObj.PaymentGross,
                    McCurrency = ipnObj.McCurrency,
                    PaymentType = ipnObj.PaymentType,
                    PaymentFee = ipnObj.PaymentFee,
                    PayerStatus = ipnObj.PayerStatus,
                    McFee = ipnObj.McFee,
                    McGross = ipnObj.McGross,
                    ProtectionEligibility = ipnObj.ProtectionEligibility,
                    TransactionSubject = ipnObj.TransactionSubject
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                transaction = results.FirstOrDefault();
            }

            return transaction;
        }

        public OrderSubscriptionProfile AddOrderSubscriptionProfileToOrderByOrderId(int orderId, int subscriberId, IpnMessage ipnObj)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified");
            Contract.Requires(ipnObj != null, "Invalid IPN message specified");
            Contract.Requires(!String.IsNullOrEmpty(ipnObj.ItemNumber), "Invalid IPN message. Unique subscriber id is missing");

            Logger.Debug(String.Format("Adding subscription profile with paypal id '{0}' with an existing order with id={1}", ipnObj.ItemNumber, orderId));

            OrderSubscriptionProfile profile = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<OrderSubscriptionProfile>("strimm.AddOrderSubscriptionProfileToOrderByOrderIdWithGet", new
                {
                    OrderId = orderId,
                    PayingSubscriberId = subscriberId,
                    SubscriptionPlan = ipnObj.ItemName,
                    PayPalSubscriptionId = ipnObj.ItemNumber,
                    ButtonId = ipnObj.BtnId,
                    TrialPaymentAmount = ipnObj.Amount1,
                    CycleBillingAmount = ipnObj.McGross != null && ipnObj.McGross.Value > 0 ? ipnObj.McGross : 
                                                (ipnObj.Amount3 ?? 0),
                    TrialPeriod = ipnObj.Period1,
                    BillingCycle = ipnObj.Period3,
                    Currency = ipnObj.McCurrency
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                profile = results.FirstOrDefault();
            }

            return profile;
        }

        public ProductSubscriptionPo AddProductSubscription(int orderId, DateTime? trialStartDate, DateTime subscriptionStartDate, ProductSubscriptionStatusEnum status)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified");
            Contract.Requires(subscriptionStartDate != null, "Invalid trial end date specified");

            Logger.Debug(String.Format("Adding product subscription for order with id '{0}' with subscription period starting on '{1}' with status={2}", orderId, subscriptionStartDate.ToShortDateString(), status));

            if (trialStartDate == null)
            {
                Logger.Debug(String.Format("Subscription corresponding to order with id={0} does not have a trial period", orderId));
            }
            else
            {
                Logger.Debug(String.Format("Subscription corresponding to order with id={0} does have a trial period that will start on '{1}'", orderId, trialStartDate.Value.ToLongDateString()));
            }

            ProductSubscriptionPo subscription = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<ProductSubscriptionPo>("strimm.InsertProductSubscriptionWithGet", new
                {
                    OrderId = orderId,
                    TrialStartDate = trialStartDate,
                    TrialEndDate = subscriptionStartDate,
                    ProductSubscriptionStatusId = (int)status
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                subscription = results.FirstOrDefault();
            }

            return subscription;
        }

        public ProductSubscriptionPo GetProductSubscriptionByOrderId(int orderId)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified");

            Logger.Debug(String.Format("Retrieving product subscription for order with id '{0}'", orderId));

            ProductSubscriptionPo subscription = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<ProductSubscriptionPo>("strimm.GetProductSubscriptionByOrderId", new
                {
                    OrderId = orderId
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                subscription = results.FirstOrDefault();
            }

            return subscription;
        }

        public List<ProductSubscriptionPo> GetProductSubscriptionsByUserId(int userId)
        {
            Contract.Requires(userId > 0, "Invalid user id specified");

            Logger.Debug(String.Format("Retrieving product subscription for user with id '{0}'", userId));

            List<ProductSubscriptionPo> subscriptions = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                subscriptions = this.StrimmDbConnection.Query<ProductSubscriptionPo>("strimm.GetProductSubscriptionsByUserId", new
                {
                    UserId = userId
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
            }

            return subscriptions;
        }

        public ProductSubscriptionPo UpdateProductSubscriptionStatusById(int productSubscriptionId, ProductSubscriptionStatusEnum status)
        {
            Contract.Requires(productSubscriptionId > 0, "Invalid product subscription id specified");

            Logger.Debug(String.Format("Updating product subscription with id {0} to status {1}", productSubscriptionId, status));

            ProductSubscriptionPo subscription = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<ProductSubscriptionPo>("strimm.UpdateProductSubscriptionStatusById", new
                {
                    ProductSubscriptionId = productSubscriptionId,
                    ProductSubscriptionStatusId = (int)status
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                subscription = results.FirstOrDefault();
            }

            return subscription;
        }

        public PayingSubscriber GetPayingSubscriberByPayPalSubscriberId(string subscriberId)
        {
            Contract.Requires(!String.IsNullOrEmpty(subscriberId), "Invalid paypal subscriber id specified");

            Logger.Debug(String.Format("Retrieving paying subscriber details using paypal subscriber id '{0}'", subscriberId));

            PayingSubscriber subscription = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<PayingSubscriber>("strimm.GetPayingSubscriberByPayPalSubscriberId", new
                {
                    PayPalSubscriberId = subscriberId
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                subscription = results.FirstOrDefault();
            }

            return subscription;
        }

        public OrderSubscriptionProfile GetOrderSubscriptionProfileByOrderId(int orderId)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified");

            Logger.Debug(String.Format("Retrieving order details by id {0}", orderId));

            OrderSubscriptionProfile profile = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<OrderSubscriptionProfile>("strimm.GetOrderSubscriptionProfileByOrderId", new
                {
                    OrderId = orderId
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                profile = results.FirstOrDefault();
            }

            return profile;

        }

        public OrderPo GetOrderPoById(int orderId)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified");

            Logger.Debug(String.Format("Retrieving order details by id {0}", orderId));

            OrderPo order = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<OrderPo>("strimm.GetOrderPoById", new
                {
                    OrderId = orderId
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                order = results.FirstOrDefault();
            }

            return order;
        }

        public UserChannelEntitlement GetUserChannelEntitlementsByUserId(int userId)
        {
            Contract.Requires(userId > 0, "Invalid user id specified");

            Logger.Debug(String.Format("Retrieving user channel entitlements for user with id '{0}'", userId));

            UserChannelEntitlement entitlements = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<UserChannelEntitlement>("strimm.GetUserChannelEntitlementsByUserId", new
                {
                    UserId = userId
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                entitlements = results.FirstOrDefault();
            }

            return entitlements;

        }

        public OrderSubscriptionProfile UpdateBillingDetailsOnOrderSubscriptionProfileById(int orderSubscriptionProfileId, decimal? trialPaymentAmount, decimal? monthlyPaymentAmount, string trialPeriod, string billingCycle)
        {
            Contract.Requires(orderSubscriptionProfileId > 0, "Invalid order subscription profile id specified");

            Logger.Debug(String.Format("Updating update of billing information on order subscription profile with id={0}, initial payment={1}, cycle payment={2}, trial period '{3}', billing cycle='{4}'", orderSubscriptionProfileId, trialPaymentAmount, monthlyPaymentAmount, trialPeriod, billingCycle));

            OrderSubscriptionProfile profile = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<OrderSubscriptionProfile>("strimm.UpdateBillingDeatilsOnOrderSubscriptionProfileByIdWithGet", new
                {
                    OrderSubscriptionProfileId = orderSubscriptionProfileId,
                    TrialPaymentAmount = trialPaymentAmount,
                    CycleBillingAmount = monthlyPaymentAmount,
                    TrialPeriod = trialPeriod,
                    BillingCycle = billingCycle
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                profile = results.FirstOrDefault();
            }

            return profile;
        }

        public List<OrderPo> GetOrderPosById(int userId)
        {
            Contract.Requires(userId > 0, "Invalid user id specified");

            Logger.Debug(String.Format("Retrieving all order for user with id {0}", userId));

            List<OrderPo> orders = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                orders = this.StrimmDbConnection.Query<OrderPo>("strimm.GetOrderPosByUserId", new
                {
                    UserId = userId
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
            }

            return orders;
        }
        public List<OrderPo> GetOrderPosByIdForAdmin(int userId)
        {
            Contract.Requires(userId > 0, "Invalid user id specified");

            Logger.Debug(String.Format("Retrieving all order for user with id {0}", userId));

            List<OrderPo> orders = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                orders = this.StrimmDbConnection.Query<OrderPo>("strimm.GetOrderPosByUserIdForAdmin", new
                {
                    UserId = userId
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
            }

            return orders;
        }

        public List<OrderPo> GetPendingOrdersExpiringOn(DateTime today)
        {
            List<OrderPo> orders = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                orders = this.StrimmDbConnection.Query<OrderPo>("strimm.GetPendingOrderPosExpiringByDate", new
                {
                    ExpirationDate = today
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
            }

            return orders;
        }

        public List<OrderTransactionPo> GetOrderTransactionPosByOrderId(int orderId)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified on request");

            List<OrderTransactionPo> transactions = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                transactions = this.StrimmDbConnection.Query<OrderTransactionPo>("strimm.GetOrderTransactionsPoByOrderId", new
                {
                    OrderId = orderId
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
            }

            return transactions;
        }

        public List<OrderStatistics> GetOrderStatisticsByDateRange(DateTime? startDate, DateTime? endDate)
        {
            List<OrderStatistics> statistics = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                statistics = this.StrimmDbConnection.Query<OrderStatistics>("strimm.GetSubscriptionsByDatePeriod", new
                {
                    StartDate = startDate,
                    EndDate = endDate
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
            }

            return statistics;
        }

        public List<ChannelTubePo> GetChannelTubePosByOrderId(int orderId)
        {
            Contract.Requires(orderId > 0, "Invalid order id specified on request");

            List<ChannelTubePo> channels = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                channels = this.StrimmDbConnection.Query<ChannelTubePo>("strimm.GetChannelTubePoByOrderId", new
                {
                    OrderId = orderId
                }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
            }

            return channels;
        }

        public ProductSubscriptionStatistics GetProductSubscriptionsStatisticsByDateRange(DateTime? startDate, DateTime? endDate)
        {
            ProductSubscriptionStatistics statistics = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<ProductSubscriptionStatistics>("strimm.GetSubscriptionStatistics", new
                {
                    StartDate = startDate,
                    EndDate = endDate
                }, null, false, 30, commandType: CommandType.StoredProcedure);

                statistics = results.FirstOrDefault();
            }

            return statistics;
        }

        public List<ChannelCategoryPo> GetChannelTubeCountsByCategoryForExistingSubscriptions()
        {
            List<ChannelCategoryPo> categoryCounts = null;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                categoryCounts = this.StrimmDbConnection.Query<ChannelCategoryPo>("strimm.GetChannelCategoriesWithChannelCountForExistingSubscriptions", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
            }

            return categoryCounts;
        }

        public int GetPriorOrderCountByUserId(int userId)
        {
            int count = 0;

            if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
            {
                var results = this.StrimmDbConnection.Query<int>("strimm.GetPriorOrdersCountByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                count = results.FirstOrDefault();
            }

            return count;

        }
    }
}

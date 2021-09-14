using Common.Logging;
using Quartz;
using Strimm.Jobs.Core;
using Strimm.Model.Order;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Jobs.ExpirePendingResolutionOrders
{
    [Export(typeof(IStrimmJob))]
    public class ExpirePendingResolutionOrdersJob : BaseJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ExpirePendingResolutionOrdersJob));

        public ExpirePendingResolutionOrdersJob()
            : base(typeof(ExpirePendingResolutionOrdersJob).Name)
        {
        }

        public override void Execute(IJobExecutionContext context)
        {
            Logger.Debug(String.Format("Exectuing 'ExpirePendingResolutionOrdersJob' job at {0}", DateTime.Now.ToShortTimeString()));

            var errors = new List<string>();

            try
            {
                var pendingOrders = OrderManager.GetExistingOrdersPendingResolution();

                if (pendingOrders != null && pendingOrders.Count > 0)
                {
                    pendingOrders.ForEach(x =>
                    {
                        try
                        {
                            x.OrderStatusId = (int)OrderStatusEnum.Canceled;
                            OrderManager.UpdateOrderStatusById(x);

                            Logger.Debug(String.Format("Canceled order with id={0} pending for resolution at the end of the grace period", x.OrderId));

                            var user = UserManage.GetUserPoByUserId(x.UserId);
                            var canceledProductSubscription = OrderManager.GetProductSubscriptionByOrderId(x.OrderId);

                            OrderManager.UpdateUserChannelsSettingsBasedOnCanceledProduct(user, x, canceledProductSubscription);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(String.Format("Failed to expired order with id={0}", x.OrderId), ex);
                        }
                    });
                }
                else
                {
                    Logger.Warn("There are no pending orders to expire");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while expiring pending orders", ex);
            }        
        }
    }
}

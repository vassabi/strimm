using Strimm.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IOrderRepository
    {
        OrderPo CreateOrder(int userId, string firstName, string lastName, string address, string city, string state, string countryCode, int productId, string orderNumber, string description, bool trialAllowed, bool isAnnual, bool isUpgrade);

        OrderPo FindOrderByOrderNumber(string orderNumber);

        int GetPriorOrderCountByUserId(int userId);
        List<OrderPo> GetOrderPosByIdForAdmin(int userId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPizzaCommon.Managers
{
    public interface IOrderManager
    {
        int GetOrderCount(string customerId);
        int GetCompletedOrderCount(string customerId);
        void IncrementOrder(string customerId);
        void DecrementOrder(string customerId);
        void CompleteOrder(string customerId);
    }
}

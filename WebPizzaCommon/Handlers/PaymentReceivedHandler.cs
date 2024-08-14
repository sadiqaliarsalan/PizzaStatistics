using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Managers;
using WebPizzaCommon.Models;

namespace WebPizzaCommon.Handlers
{
    public class PaymentReceivedHandler : IPizzaEventHandler
    {
        private readonly IOrderManager _orderManager;
        private readonly ICustomerManager _customerManager;
        private readonly ILoyaltyPointsManager _loyaltyPointsManager;

        public PaymentReceivedHandler(IOrderManager orderManager, ICustomerManager customerManager, ILoyaltyPointsManager loyaltyPointsManager)
        {
            _orderManager = orderManager;
            _customerManager = customerManager;
            _loyaltyPointsManager = loyaltyPointsManager;
        }

        public void HandleEvent(Event eventData)
        {
            _orderManager.CompleteOrder(eventData.CustomerId);
            var customer = _customerManager.GetCustomer(eventData.CustomerId);
            if (customer != null)
            {
                int completedOrders = _orderManager.GetCompletedOrderCount(eventData.CustomerId);
                bool isLoyal = completedOrders >= 3;
                _customerManager.UpdateLoyaltyStatus(eventData.CustomerId, isLoyal);
                if (isLoyal)
                {
                    _loyaltyPointsManager.AddLoyaltyPoints(eventData.CustomerId, 10);
                }
            }
        }
    }
}

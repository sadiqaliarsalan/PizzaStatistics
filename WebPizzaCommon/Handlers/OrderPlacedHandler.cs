using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Managers;
using WebPizzaCommon.Models;

namespace WebPizzaCommon.Handlers
{
    public class OrderPlacedHandler : IPizzaEventHandler
    {
        private readonly IOrderManager _orderManager;

        public OrderPlacedHandler(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public void HandleEvent(Event eventData)
        {
            _orderManager.IncrementOrder(eventData.CustomerId);
        }
    }
}

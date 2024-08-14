using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Managers;
using WebPizzaCommon.Models;

namespace WebPizzaCommon.Handlers
{
    public class LoyaltySignupHandler : IPizzaEventHandler
    {
        private readonly ICustomerManager _customerManager;

        public LoyaltySignupHandler(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        public void HandleEvent(Event eventData)
        {
            _customerManager.UpdateLoyaltyStatus(eventData.CustomerId, true);
        }
    }
}

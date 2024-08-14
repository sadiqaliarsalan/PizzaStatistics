using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Enums;
using WebPizzaCommon.Models;
using WebPizzaCommon.Utilities;

namespace WebPizzaEventSimulator.Services
{
    public class EventDataService : IEventDataService
    {
        private string[] _customerIds;
        private Random _random = new Random();
        private Dictionary<EventType, string> _eventDataGenerators;

        public EventDataService()
        {
            _customerIds = CustomerUtility.GenerateCustomerIds(10);
            InitEventDataGenerators();
        }

        private void InitEventDataGenerators()
        {
            _eventDataGenerators = new Dictionary<EventType, string>
            {
                { EventType.PizzaOrdered, "PizzaType: Pepperoni" },
                { EventType.CustomerCreated, "CustomerType: Web" },
                { EventType.CustomerAbandonedOrder, "Reason: OutOfStock" },
                { EventType.PizzaPrepared, "PizzaType: Margherita" },
                { EventType.DeliveryDispatched, "DeliveryMethod: Bike" },
                { EventType.DeliveryDelivered, "DeliveryTime: 30 minutes" },
                { EventType.PaymentReceived, "PaymentMethod: CreditCard" },
                { EventType.CustomerFeedbackReceived, "Rating: 5" },
                { EventType.LoyaltySignup, "LoyaltyStatus: Active" },
                { EventType.LoyaltyPointsEarned, "PointsEarned: 10" }
            };
        }

        public Event GenerateEventData()
        {
            // Generate a random event type number between 1 and 10
            EventType eventType = (EventType)_random.Next(0, 10);

            // Choose a random customer ID from the array of customer IDs
            string customerId = _customerIds[_random.Next(_customerIds.Length)];

            // Generate additional data based on the event type
            string additionalData = _eventDataGenerators[eventType];

            // Return event object
            return new Event(eventType, customerId, additionalData);
        }
    }
}

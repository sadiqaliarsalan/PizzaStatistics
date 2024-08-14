using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Enums;
using WebPizzaCommon.Models;

namespace WebPizzaCommon.Utilities
{
    public static class EventUtility
    {
        public static bool TryParseEventData(Event eventData, out EventType eventType, out string customerId, out string additionalData)
        {
            eventType = default;
            customerId = null;
            additionalData = null;

            if (!Enum.IsDefined(typeof(EventType), eventData.Type))
            {
                Console.WriteLine("Invalid event type received.");
                return false;
            }

            eventType = eventData.Type;

            if (string.IsNullOrEmpty(eventData.CustomerId))
            {
                Console.WriteLine("No customer ID provided.");
                return false;
            }

            customerId = eventData.CustomerId;
            additionalData = eventData.AdditionalData;
            return true;
        }
    }
}

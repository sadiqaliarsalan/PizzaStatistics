using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Enums;

namespace WebPizzaCommon.Utilities
{
    public static class EventUtility
    {
        public static bool TryParseEventData(string eventData, out EventType eventType, out string customerId)
        {
            eventType = default;
            customerId = null;

            var eventParts = eventData.Split(',');
            if (eventParts.Length != 3)
            {
                Console.WriteLine("Invalid event data received.");
                return false;
            }

            if (!Enum.TryParse(eventParts[0], out eventType))
            {
                Console.WriteLine("Invalid event type received.");
                return false;
            }

            customerId = eventParts[1];
            return true;
        }
    }
}

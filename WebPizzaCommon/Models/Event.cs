using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Enums;

namespace WebPizzaCommon.Models
{
    // Define an event class
    public class Event
    {
        public EventType Type { get; }
        public DateTime Timestamp { get; }
        public string CustomerId { get; }

        public Event(EventType type, string customerId)
        {
            Type = type;
            Timestamp = DateTime.Now;
            CustomerId = customerId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Enums;

namespace WebPizzaCommon.Models
{
    // Event class
    public class Event
    {
        public EventType Type { get; }

        public DateTime Timestamp { get; }
        
        public string CustomerId { get; }

        public string AdditionalData { get; }

        public Event(EventType type, string customerId, string additionalData)
        {
            Type = type;
            Timestamp = DateTime.Now;
            CustomerId = customerId;
            AdditionalData = additionalData;
        }
    }
}

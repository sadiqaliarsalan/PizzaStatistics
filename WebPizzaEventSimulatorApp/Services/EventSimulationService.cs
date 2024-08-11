using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Managers;

namespace WebPizzaEventSimulator.Services
{
    public class EventSimulationService : IEventSimulationService
    {
        private readonly ITcpConnectionManager _tcpManager;
        private readonly IEventDataService _eventDataGenerator;

        public EventSimulationService(ITcpConnectionManager tcpManager, IEventDataService eventDataGenerator)
        {
            _tcpManager = tcpManager;
            _eventDataGenerator = eventDataGenerator;
        }

        public void SimulateEvents()
        {
            Console.WriteLine("Web Pizza Event Simulator");
            Console.WriteLine("---------------------------");

            try
            {
                while (true)
                {
                    string eventData = _eventDataGenerator.GenerateEventData();
                    _tcpManager.SendData(eventData);
                    Console.WriteLine($"Sent event data: {eventData}");
                    Thread.Sleep(100);
                }
            }
            finally
            {
                _tcpManager.Dispose();
            }

        }
    }
}

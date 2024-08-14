using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebPizzaCommon.Managers;
using WebPizzaCommon.Models;

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
            Console.WriteLine("Press any key to stop simulation ..");

            var task = Task.Run(() => Console.ReadKey());

            try
            {
                while (!task.IsCompleted)
                {
                    Event eventInstance = _eventDataGenerator.GenerateEventData();
                    string jsonData = JsonConvert.SerializeObject(eventInstance);
                    _tcpManager.SendData(jsonData);
                    Console.WriteLine($"Sent event data: {jsonData}");
                    Task.Delay(100).Wait();
                }
            }
            finally
            {
                _tcpManager.Dispose();
            }

            Console.WriteLine("Simulation stopped");
        }
    }
}

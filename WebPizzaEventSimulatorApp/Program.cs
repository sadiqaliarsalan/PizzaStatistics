using System.Net.Sockets;
using System.Text;
using WebPizzaCommon.Enums;
using Microsoft.Extensions.DependencyInjection;
using WebPizzaCommon.Managers;
using WebPizzaEventSimulator.Services;

namespace WebPizzaEventSimulator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string serverAddress = "localhost";
            int serverPort = 9999;

            var serviceProvider = new ServiceCollection()
                .AddSingleton<ITcpConnectionManager>(provider => new TcpConnectionManager(serverAddress, serverPort))
                .AddSingleton<IEventDataService, EventDataService>()
                .AddSingleton<IEventSimulationService, EventSimulationService>()
                .BuildServiceProvider();

            var simulationService = serviceProvider.GetService<IEventSimulationService>();
            simulationService.SimulateEvents();
        }
    }
}
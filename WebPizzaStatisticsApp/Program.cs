using System.Net;
using System.Net.Sockets;
using System.Text;
using ConsoleTables;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WebPizzaCommon.Enums;
using WebPizzaCommon.Managers;
using WebPizzaCommon.Models;
using WebPizzaCommon.Utilities;
using WebPizzaStatistics.Services;

namespace WebPizzaStatistics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ITcpConnectionManager>(x => new TcpConnectionManager("localhost", 9999, true))
                .AddSingleton<ICustomerManager, CustomerManager>()
                .AddSingleton<IOrderManager, OrderManager>()
                .AddSingleton<ILoyaltyPointsManager, LoyaltyPointsManager>() 
                .AddSingleton<IStatisticsService, StatisticsService>()
                .BuildServiceProvider();

            var tcpManager = serviceProvider.GetService<ITcpConnectionManager>();
            var statisticsService = serviceProvider.GetService<IStatisticsService>();

            try
            {
                Console.WriteLine("Web Pizza Statistics");
                Console.WriteLine("---------------------------------------------");

                var displayThread = new Thread(statisticsService.StartDisplayLoop);
                displayThread.Start();

                Console.WriteLine("Listening for incoming TCP connections...");

                while (true)
                {
                    var client = tcpManager.AcceptClient();
                    Console.WriteLine("Connected to a client.");

                    var clientThread = new Thread(() =>
                    {
                        HandleClient(client, tcpManager, statisticsService);
                    });

                    clientThread.Start();
                }
            }
            finally
            {
                tcpManager.Dispose();
            }
        }

        private static void HandleClient(TcpClient client, ITcpConnectionManager tcpManager, IStatisticsService statisticsService)
        {
            while (client.Connected)
            {
                try
                {
                    var eventJson = tcpManager.ReadData(client);
                    var eventData = JsonConvert.DeserializeObject<Event>(eventJson);
                    if (eventData != null && EventUtility.TryParseEventData(eventData, out EventType eventType, out string customerId, out string additionalData))
                    {
                        var newEvent = new Event(eventType, customerId, additionalData);
                        statisticsService.UpdateStatistics(newEvent);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    break;
                }
            }

            client.Close();
            Console.WriteLine("Disconnected from the client.");
        }
    }
}
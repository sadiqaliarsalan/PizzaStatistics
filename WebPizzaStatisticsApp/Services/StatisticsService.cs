using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Enums;
using WebPizzaCommon.Handlers;
using WebPizzaCommon.Managers;
using WebPizzaCommon.Models;

namespace WebPizzaStatistics.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ICustomerManager _customerManager;
        private readonly IOrderManager _orderManager;
        private readonly ILoyaltyPointsManager _loyaltyPointsManager;
        private Dictionary<EventType, IPizzaEventHandler> _eventHandlers;

        public StatisticsService(ICustomerManager customerManager, IOrderManager orderManager, ILoyaltyPointsManager loyaltyPointsManager)
        {
            _customerManager = customerManager;
            _orderManager = orderManager;
            _loyaltyPointsManager = loyaltyPointsManager;
            InitEventHanlders();
        }

        private void InitEventHanlders()
        {
            _eventHandlers = new Dictionary<EventType, IPizzaEventHandler>
            {
                { EventType.PizzaOrdered, new OrderPlacedHandler(_orderManager) },
                { EventType.CustomerCreated, new CustomerCreatedHandler(_customerManager) },
                { EventType.CustomerAbandonedOrder, new CustomerAbandonedOrderHandler(_orderManager) },
                { EventType.PaymentReceived, new PaymentReceivedHandler(_orderManager, _customerManager, _loyaltyPointsManager) },
                { EventType.LoyaltySignup, new LoyaltySignupHandler(_customerManager) }
            };
        }

        public void UpdateStatistics(Event newEvent)
        {
            if (_eventHandlers.TryGetValue(newEvent.Type, out var handler))
            {
                handler.HandleEvent(newEvent);
            }
        }

        public void DisplayStatistics()
        {
            var customers = _customerManager.GetAllCustomers();
            Console.WriteLine("Statistics:");
            var table = new ConsoleTable("Customer ID", "Orders", "Completed Orders", "Loyalty Member", "Loyalty Points");

            foreach (var customer in customers)
            {
                var orders = _orderManager.GetOrderCount(customer.Id);
                var completedOrders = _orderManager.GetCompletedOrderCount(customer.Id);
                var loyaltyPoints = _loyaltyPointsManager.GetLoyaltyPoints(customer.Id);
                table.AddRow(customer.Id, orders, completedOrders, customer.IsLoyaltyMember, loyaltyPoints);
            }

            table.Write(Format.MarkDown);
        }

        public void StartDisplayLoop()
        {
            Console.WriteLine("Press any key to stop display loop ..");
            var task = Task.Run(() => Console.ReadKey());
            while (!task.IsCompleted)
            {
                Console.Clear();
                Console.WriteLine("Web Pizza Statistics");
                Console.WriteLine("---------------------------------------------");
                DisplayStatistics();
                Task.Delay(1000);
            }
        }
    }
}

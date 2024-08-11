using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Enums;
using WebPizzaCommon.Managers;
using WebPizzaCommon.Models;

namespace WebPizzaStatistics.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ICustomerManager _customerManager;
        private readonly IOrderManager _orderManager;
        private readonly ILoyaltyPointsManager _loyaltyPointsManager;

        public StatisticsService(ICustomerManager customerManager, IOrderManager orderManager, ILoyaltyPointsManager loyaltyPointsManager)
        {
            _customerManager = customerManager;
            _orderManager = orderManager;
            _loyaltyPointsManager = loyaltyPointsManager;
        }

        public void UpdateStatistics(Event newEvent)
        {
            switch (newEvent.Type)
            {
                case EventType.PizzaOrdered:
                    _orderManager.IncrementOrder(newEvent.CustomerId);
                    break;
                case EventType.CustomerCreated:
                    _customerManager.AddCustomer(new Customer(newEvent.CustomerId));
                    break;
                case EventType.CustomerAbandonedOrder:
                    _orderManager.DecrementOrder(newEvent.CustomerId);
                    break;
                case EventType.PaymentReceived:
                    _orderManager.CompleteOrder(newEvent.CustomerId);
                    if (_orderManager.GetCompletedOrderCount(newEvent.CustomerId) >= 3)
                        _loyaltyPointsManager.AddLoyaltyPoints(newEvent.CustomerId, 10);
                    break;
                case EventType.LoyaltySignup:
                    var customer = _customerManager.GetCustomer(newEvent.CustomerId);
                    if (customer != null)
                        customer.IsLoyaltyMember = true;
                    break;
                default:
                    break;
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
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Web Pizza Statistics");
                Console.WriteLine("---------------------------------------------");
                DisplayStatistics();
                Thread.Sleep(1000);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Models;

namespace WebPizzaCommon.Managers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly Dictionary<string, Customer> _customers = new();
        private readonly object _lock = new object();

        public Customer GetCustomer(string customerId)
        {
            lock (_lock)
            {
                return _customers.TryGetValue(customerId, out var customer) ? customer : null;
            }
        }

        public void AddCustomer(Customer customer)
        {
            lock (_lock)
            {
                _customers[customer.Id] = customer;
            }
        }

        public List<Customer> GetAllCustomers()
        {
            lock (_lock)
            {
                return _customers.Values.ToList();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Models;

namespace WebPizzaCommon.Managers
{
    public interface ICustomerManager
    {
        Customer GetCustomer(string customerId);

        void AddCustomer(string customerId);

        List<Customer> GetAllCustomers();

        void UpdateLoyaltyStatus(string customerId, bool isLoyal);
    }
}

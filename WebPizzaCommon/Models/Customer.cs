using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPizzaCommon.Models
{
    // Define a customer class
    public class Customer
    {
        public string Id { get; }
        public bool IsLoyaltyMember { get; set; }

        public Customer(string id)
        {
            Id = id;
            IsLoyaltyMember = false;
        }
    }
}

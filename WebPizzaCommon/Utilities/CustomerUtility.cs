using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPizzaCommon.Utilities
{
    public static class CustomerUtility
    {
        public static string[] GenerateCustomerIds(int count)
        {
            string[] customerIds = new string[count];
            for (int i = 0; i < customerIds.Length; i++)
            {
                customerIds[i] = Guid.NewGuid().ToString();
            }
            return customerIds;
        }
    }
}

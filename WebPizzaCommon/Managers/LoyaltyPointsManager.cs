using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Models;

namespace WebPizzaCommon.Managers
{
    public class LoyaltyPointsManager : ILoyaltyPointsManager
    {
        private readonly Dictionary<string, int> _loyaltyPoints = new();
        private readonly object _lock = new object();

        public int GetLoyaltyPoints(string customerId)
        {
            lock (_lock)
            {
                return _loyaltyPoints.TryGetValue(customerId, out var points) ? points : 0;
            }
        }

        public void AddLoyaltyPoints(string customerId, int points)
        {
            lock (_lock)
            {
                if (_loyaltyPoints.ContainsKey(customerId))
                    _loyaltyPoints[customerId] += points;
                else
                    _loyaltyPoints[customerId] = points;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPizzaCommon.Managers
{
    public class InventoryManager : IInventoryManager
    {
        private readonly Dictionary<string, int> _inventory = new Dictionary<string, int>();

        public void IncreaseInventory(string pizzaType, int quantity)
        {
            if (_inventory.ContainsKey(pizzaType))
            {
                _inventory[pizzaType] += quantity;
            }
            else
            {
                _inventory[pizzaType] = quantity;
            }

            Console.WriteLine($"Increased inventory");
        }

        public void DecreaseInventory(string pizzaType, int quantity)
        {
            if (_inventory.ContainsKey(pizzaType) && _inventory[pizzaType] >= quantity)
            {
                _inventory[pizzaType] -= quantity;
                Console.WriteLine($"Decreased inventory");
            }
        }
    }
}

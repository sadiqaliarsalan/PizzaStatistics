using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPizzaCommon.Managers
{
    public interface IInventoryManager
    {
        void IncreaseInventory(string pizzaType, int quantity);
        void DecreaseInventory(string pizzaType, int quantity);
    }
}

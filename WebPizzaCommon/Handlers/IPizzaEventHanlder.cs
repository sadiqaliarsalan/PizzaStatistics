using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Models;

namespace WebPizzaCommon.Handlers
{
    public interface IPizzaEventHandler
    {
        void HandleEvent(Event eventData);
    }
}

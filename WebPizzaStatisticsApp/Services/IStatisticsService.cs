using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Models;

namespace WebPizzaStatistics.Services
{
    public interface IStatisticsService
    {
        void UpdateStatistics(Event newEvent);
        void DisplayStatistics();
        void StartDisplayLoop();
    }
}

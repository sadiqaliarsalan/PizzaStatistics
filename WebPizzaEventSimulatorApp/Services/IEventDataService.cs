﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Models;

namespace WebPizzaEventSimulator.Services
{
    public interface IEventDataService
    {
        Event GenerateEventData();
    }
}

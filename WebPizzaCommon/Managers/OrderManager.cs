﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Models;

namespace WebPizzaCommon.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly Dictionary<string, int> _orders = new();
        private readonly Dictionary<string, int> _completedOrders = new();
        private readonly object _lock = new object();

        public int GetOrderCount(string customerId)
        {
            lock (_lock)
            {
                return _orders.TryGetValue(customerId, out var count) ? count : 0;
            }
        }

        public int GetCompletedOrderCount(string customerId)
        {
            lock (_lock)
            {
                return _completedOrders.TryGetValue(customerId, out var count) ? count : 0;
            }
        }

        public void IncrementOrder(string customerId)
        {
            lock (_lock)
            {
                if (_orders.ContainsKey(customerId))
                    _orders[customerId]++;
                else
                    _orders[customerId] = 1;
            }
        }

        // handle a case where an order if not found for a customer
        // could be that abandon order is received before the order is even placed
        public void DecrementOrder(string customerId)
        {
            lock (_lock)
            {
                if (_orders.ContainsKey(customerId))
                {
                    if (_orders[customerId] > 0)
                    {
                        _orders[customerId]--;
                    }
                    else
                    {
                        Console.WriteLine($"No active orders to decrement for customer: {customerId}");
                    }
                }
                else
                {
                    Console.WriteLine($"No orders found for customer: {customerId}");
                }
            }
        }

        public void CompleteOrder(string customerId)
        {
            lock (_lock)
            {
                if (_orders.ContainsKey(customerId) && _orders[customerId] > 0)
                {
                    _orders[customerId]--;
                    if (_completedOrders.ContainsKey(customerId))
                        _completedOrders[customerId]++;
                    else
                        _completedOrders[customerId] = 1;
                }
            }
        }
    }
}

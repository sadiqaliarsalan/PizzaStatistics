using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Managers;

namespace WebPizzaCommon.Tests.Managers
{
    [TestFixture]
    public class OrderManagerTests
    {
        private OrderManager _orderManager;

        [SetUp]
        public void Setup()
        {
            _orderManager = new OrderManager();
        }

        [Test]
        public void IncrementOrder_ShouldIncreaseOrderCount_WhenNewCustomer_Test()
        {
            var customerId = "some customer id";
            _orderManager.IncrementOrder(customerId);
            _orderManager.IncrementOrder(customerId);
            _orderManager.IncrementOrder(customerId);
            _orderManager.IncrementOrder(customerId);
            _orderManager.IncrementOrder(customerId);

            Assert.AreEqual(5, _orderManager.GetOrderCount(customerId));
        }

        [Test]
        public void DecrementOrder_ShouldDecreaseOrderCount_WhenOrdersExist_Test()
        {
            var customerId = "some customer id";
            _orderManager.IncrementOrder(customerId);
            _orderManager.IncrementOrder(customerId);
            _orderManager.DecrementOrder(customerId);

            Assert.AreEqual(1, _orderManager.GetOrderCount(customerId));
        }

        [Test]
        public void CompleteOrder_ShouldTransferOrderToCompleted_WhenOrdersExist_Test()
        {
            var customerId = "some customer id";
            _orderManager.IncrementOrder(customerId);
            _orderManager.CompleteOrder(customerId);

            Assert.AreEqual(0, _orderManager.GetOrderCount(customerId));
            Assert.AreEqual(1, _orderManager.GetCompletedOrderCount(customerId));
        }

        [Test]
        public void CompleteOrder_ShouldReturnZero_WhenNoOrdersExist_Test()
        {
            var customerId = "some customer id";
            _orderManager.CompleteOrder(customerId);

            Assert.AreEqual(0, _orderManager.GetOrderCount(customerId));
            Assert.AreEqual(0, _orderManager.GetCompletedOrderCount(customerId));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Managers;

namespace WebPizzaCommon.Tests.Managers
{
    [TestFixture]
    public class CustomerManagerTests
    {
        private CustomerManager _customerManager;

        [SetUp]
        public void Setup()
        {
            _customerManager = new CustomerManager();
        }

        [Test]
        public void AddCustomer_ShouldAddNewCustomer_WhenCustomerDoesNotExist_Test()
        {
            var customerId = "some customer id";
            _customerManager.AddCustomer(customerId);

            var result = _customerManager.GetCustomer(customerId);
            Assert.IsNotNull(result);
            Assert.AreEqual(customerId, result.Id);
        }

        [Test]
        public void AddCustomer_ShouldNotAddCustomer_WhenCustomerAlreadyExists_Test()
        {
            var customerId = "some customer id";
            _customerManager.AddCustomer(customerId);
            _customerManager.AddCustomer(customerId);

            var allCustomers = _customerManager.GetAllCustomers();
            Assert.AreEqual(1, allCustomers.Count);
        }

        [Test]
        public void GetCustomer_ShouldReturnCustomer_WhenCustomerExists_Test()
        {
            var customerId = "some customer id";
            _customerManager.AddCustomer(customerId);

            var result = _customerManager.GetCustomer(customerId);
            Assert.IsNotNull(result);
            Assert.AreEqual(customerId, result.Id);
        }

        [Test]
        public void GetCustomer_ShouldReturnNull_WhenCustomerDoesNotExist_Test()
        {
            var result = _customerManager.GetCustomer("some customer id");
            Assert.IsNull(result);
        }

        [Test]
        public void GetAllCustomers_ShouldReturnAllCustomers_Test()
        {
            _customerManager.AddCustomer("customer 1");
            _customerManager.AddCustomer("customer 2");

            var result = _customerManager.GetAllCustomers();
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void UpdateLoyaltyStatus_ShouldUpdateStatus_WhenCustomerExists_Test()
        {
            var customerId = "some customer id";
            _customerManager.AddCustomer(customerId);

            _customerManager.UpdateLoyaltyStatus(customerId, true);
            var customer = _customerManager.GetCustomer(customerId);

            Assert.IsTrue(customer.IsLoyaltyMember);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizzaCommon.Managers;

namespace WebPizzaCommon.Tests.Managers
{
    [TestFixture]
    public class LoyaltyPointsManagerTests
    {
        private ILoyaltyPointsManager _loyaltyPointsManager;

        [SetUp]
        public void Setup()
        {
            _loyaltyPointsManager = new LoyaltyPointsManager();
        }

        [Test]
        public void AddLoyaltyPoints_NewCustomer_ShouldAddPoints_Test()
        {
            string customerId = "abc customer";
            int pointsToAdd = 10;

            _loyaltyPointsManager.AddLoyaltyPoints(customerId, pointsToAdd);
            int result = _loyaltyPointsManager.GetLoyaltyPoints(customerId);

            Assert.AreEqual(pointsToAdd, result);
        }

        [Test]
        public void AddLoyaltyPoints_ExistingCustomer_ShouldAccumulatePoints_Test()
        {
            string customerId = "abc customer";
            _loyaltyPointsManager.AddLoyaltyPoints(customerId, 5);
            int additionalPoints = 10;

            _loyaltyPointsManager.AddLoyaltyPoints(customerId, additionalPoints);
            int result = _loyaltyPointsManager.GetLoyaltyPoints(customerId);
            Assert.AreEqual(15, result);
        }

        [Test]
        public void GetLoyaltyPoints_NonExistentCustomer_ShouldReturnZero_Test()
        {
            string customerId = "abc customer";
            int result = _loyaltyPointsManager.GetLoyaltyPoints(customerId);
            Assert.AreEqual(0, result);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocking_Exercise;
using Mocking_Exercise.Interfaces;

namespace Mocking.Tests
{
    public class OrderTests
    {
        private IWarehouse warehouse;

        public void TestSetup()
        {
            this.warehouse = new StandardWarehouse();

            this.warehouse.AddStock("bread", 100);
            this.warehouse.AddStock("butter", 0);
            this.warehouse.AddStock("chocolate bar", 500);
            this.warehouse.AddStock("egg", 10);
            this.warehouse.AddStock("toilet paper", 1000);
            this.warehouse.AddStock("waterbottle", 100);
        }

        [DataTestMethod]
        [DataRow("egg")]
        [DataRow("butter")]
        [DataRow("chocolate bar")]
        [DataRow("bread")]
        [DataRow("toilet paper")]
        [DataRow("waterbottle")]
        public void Has_Product_With_Existing_Product_Returns_True(string product)
        {
            this.TestSetup();
            Assert.AreEqual(this.warehouse.HasProduct(product), true);
        }
    }
}

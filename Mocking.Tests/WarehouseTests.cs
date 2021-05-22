using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocking_Exercise;
using Mocking_Exercise.Exceptions;
using Mocking_Exercise.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mocking.Tests
{
    [TestClass]
    public class WarehouseTests
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

        [DataTestMethod]
        [DataRow("beans")]
        [DataRow("meat")]
        [DataRow("pizza")]
        [DataRow("cheese")]
        [DataRow("pringles")]
        [DataRow("red bull")]
        public void Has_Product_With_Not_Existing_Product_Returns_False(string product)
        {
            this.TestSetup();
            Assert.AreEqual(this.warehouse.HasProduct(product), false);
        }

        [DataTestMethod]
        [DataRow("beans", 100)]
        [DataRow("meat", 50)]
        [DataRow("pizza", 20)]
        [DataRow("cheese", 30)]
        [DataRow("pringles", 200)]
        [DataRow("red bull", 1000)]
        public void CurrentStock_Returns_Amount_Of_Added_Stock_Of_Specified_Product(string product, int amount)
        {
            this.TestSetup();
            this.warehouse.AddStock(product, amount);
            Assert.AreEqual(this.warehouse.CurrentStock(product), amount);
        }

        [DataTestMethod]
        [DataRow("egg", 50)]
        [DataRow("butter", 100)]
        [DataRow("chocolate bar", 150)]
        [DataRow("bread", 200)]
        [DataRow("toilet paper", 250)]
        [DataRow("waterbottle", 300)]
        public void CurrentStock_Returns_Sum_Of_Existing_And_Added_Stock(string product, int amount)
        {
            this.TestSetup();
            var newAmount = amount + this.warehouse.CurrentStock(product);
            this.warehouse.AddStock(product, amount);
            Assert.AreEqual(this.warehouse.CurrentStock(product), newAmount);
        }

        [DataTestMethod]
        [DataRow("beans")]
        [DataRow("meat")]
        [DataRow("pizza")]
        [DataRow("cheese")]
        [DataRow("pringles")]
        [DataRow("red bull")]
        public void CurrentStock_Returns_NoSuchProductException_If_Called_With_Non_Existing_Product(string product)
        {
            this.TestSetup();
            try
            {
                this.warehouse.CurrentStock(product);
                Assert.Fail();
            }
            catch (NoSuchProductException)
            {
            }
        }

        [DataTestMethod]
        [DataRow("beans", 100)]
        [DataRow("meat", 50)]
        [DataRow("pizza", 20)]
        [DataRow("cheese", 30)]
        [DataRow("pringles", 200)]
        [DataRow("red bull", 1000)]
        public void TakeStock_Returns_NoSuchProductException_If_Called_With_Non_Existing_Product(string product, int amount)
        {
            this.TestSetup();
            try
            {
                this.warehouse.TakeStock(product, amount);
                Assert.Fail();
            }
            catch (NoSuchProductException)
            {
            }
        }

        [DataTestMethod]
        [DataRow("egg", 150)]
        [DataRow("butter", 1)]
        [DataRow("chocolate bar", 1000)]
        [DataRow("bread", 1001)]
        [DataRow("toilet paper", 11)]
        [DataRow("waterbottle", 101)]
        public void TakeStock_Returns_InsufficientStockException_If_Called_With_An_Amount_Bigger_Than_Current(string product, int amount)
        {
            this.TestSetup();
            try
            {
                this.warehouse.TakeStock(product, amount);
                Assert.Fail();
            }
            catch (InsufficientStockException)
            {
            }
        }
    }
}

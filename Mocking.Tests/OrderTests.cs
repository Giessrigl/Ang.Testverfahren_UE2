using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocking_Exercise;
using Mocking_Exercise.Exceptions;
using Mocking_Exercise.Interfaces;
using System;

namespace Mocking.Tests
{
    [TestClass]
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
        [DataRow("egg", 50)]
        [DataRow("butter", 100)]
        [DataRow("chocolate bar", 80)]
        [DataRow("bread", 10)]
        [DataRow("toilet paper", 1000)]
        [DataRow("waterbottle", 100)]
        public void IsFilled_Standard_Output_Returns_False(string product, int amount)
        {
            this.TestSetup();
            var order = new Order(product, amount);
            Assert.AreEqual(order.IsFilled, false);
        }

        [DataTestMethod]
        [DataRow("", 50)]
        [DataRow("   ", 100)]
        [DataRow("              ", 0)]
        [DataRow(null, 1000)]
        public void Order_Throws_ArgumentException_If_Called_With_Invalid_ProductName(string product, int amount)
        {
            this.TestSetup();
            try
            {
                var order = new Order(product, amount);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [DataTestMethod]
        [DataRow("egg", 0)]
        [DataRow("butter", -10)]
        [DataRow("chocolate bar", -1)]
        [DataRow("bread", -1000)]
        public void Order_Throws_ArgumentException_If_Called_With_Amount_Less_Than_1(string product, int amount)
        {
            this.TestSetup();
            try
            {
                var order = new Order(product, amount);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [DataTestMethod]
        [DataRow("egg", 10)]
        [DataRow("chocolate bar", 499)]
        [DataRow("bread", 50)]
        [DataRow("toilet paper", 2)]
        [DataRow("waterbottle", 1)]
        public void Fill_Returns_OrderAlreadyFilled_If_Fill_Is_Called_After_Being_Already_Filled(string product, int amount)
        {
            this.TestSetup();
            var order = new Order(product, amount);
            order.Fill(this.warehouse);

            try
            {
                order.Fill(this.warehouse);
                Assert.Fail();
            }
            catch(OrderAlreadyFilledException)
            {

            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [DataTestMethod]
        [DataRow("egg", 10)]
        [DataRow("chocolate bar", 499)]
        [DataRow("bread", 50)]
        [DataRow("toilet paper", 2)]
        [DataRow("waterbottle", 1)]
        public void IsFilled_Returns_True_After_Order_Is_Being_Filled(string product, int amount)
        {
            this.TestSetup();
            var order = new Order(product, amount);
            order.Fill(this.warehouse);

            Assert.AreEqual(order.IsFilled, true);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocking_Exercise;
using Mocking_Exercise.Exceptions;
using Mocking_Exercise.Interfaces;
using Moq;

namespace Mocking.Tests
{
    [TestClass]
    public class MockingTests
    {
        [DataTestMethod]
        [DataRow("egg", 50)]
        [DataRow("butter", 100)]
        [DataRow("chocolate bar", 80)]
        public void CanFillOrder_Of_Order_Class_Calls_HasProduct_And_CurrentStock_Of_Warehouse_Interface_If_HasProduct_Returns_True(string product, int amount)
        {
            var mock = new Mock<IWarehouse>();

            mock.Setup(warehouse => warehouse.HasProduct(product)).Returns(true);
            mock.Setup(warehouse => warehouse.CurrentStock(product)).Returns(100);

            IWarehouse warehouse = mock.Object;

            var order = new Order(product, amount);
            order.CanFillOrder(warehouse);

            mock.Verify(warehouse => warehouse.HasProduct(product), Times.Once);
            mock.Verify(warehouse => warehouse.CurrentStock(product), Times.Once);
        }

        [DataTestMethod]
        [DataRow("egg", 50)]
        [DataRow("butter", 100)]
        [DataRow("chocolate bar", 80)]
        public void CanFillOrder_Of_Order_Class_Calls_Only_HasProduct_Of_Warehouse_Interface_If_HasProduct_Returns_False(string product, int amount)
        {
            var mock = new Mock<IWarehouse>();

            mock.Setup(warehouse => warehouse.HasProduct(product)).Returns(false);
            mock.Setup(warehouse => warehouse.CurrentStock(product)).Returns(100);

            IWarehouse warehouse = mock.Object;

            var order = new Order(product, amount);
            order.CanFillOrder(warehouse);

            mock.Verify(warehouse => warehouse.HasProduct(product), Times.Once);
            mock.Verify(warehouse => warehouse.CurrentStock(product), Times.Never);
        }

        [DataTestMethod]
        [DataRow("egg", 50)]
        [DataRow("butter", 100)]
        [DataRow("chocolate bar", 80)]
        public void Fill_Of_Order_Class_Calls_TakeStock_Of_Warehouse_Interface_If_CanFillOrder_Is_Successful(string product, int amount)
        {
            var mock = new Mock<IWarehouse>();

            mock.Setup(warehouse => warehouse.TakeStock(product, amount));

            mock.Setup(warehouse => warehouse.HasProduct(product)).Returns(true);
            mock.Setup(warehouse => warehouse.CurrentStock(product)).Returns(100);

            IWarehouse warehouse = mock.Object;

            var order = new Order(product, amount);
            order.Fill(warehouse);

            mock.Verify(warehouse => warehouse.TakeStock(product, amount), Times.Once);
        }

        [DataTestMethod]
        [DataRow("egg", 50)]
        [DataRow("butter", 100)]
        [DataRow("chocolate bar", 120)]
        public void Fill_Of_Order_Class_NOT_Calls_TakeStock_Of_Warehouse_Interface_If_CanFillOrder_Is_Unsuccessful(string product, int amount)
        {
            var mock = new Mock<IWarehouse>();

            mock.Setup(warehouse => warehouse.TakeStock(product, amount));

            mock.Setup(warehouse => warehouse.HasProduct(product)).Returns(false);
            mock.Setup(warehouse => warehouse.CurrentStock(product)).Returns(100);

            IWarehouse warehouse = mock.Object;

            var order = new Order(product, amount);
            order.Fill(warehouse);

            mock.Verify(warehouse => warehouse.TakeStock(product, amount), Times.Never);
        }

        [DataTestMethod]
        [DataRow("egg", 50)]
        [DataRow("butter", 100)]
        [DataRow("chocolate bar", 80)]
        public void Fill_Of_Order_Class_Only_Calls_TakeStock_Of_Warehouse_Interface_Once_If_Fill_Is_Called_Twice_And_First_Time_Successful(string product, int amount)
        {
            var mock = new Mock<IWarehouse>();

            mock.Setup(warehouse => warehouse.TakeStock(product, amount));

            mock.Setup(warehouse => warehouse.HasProduct(product)).Returns(true);
            mock.Setup(warehouse => warehouse.CurrentStock(product)).Returns(100);

            IWarehouse warehouse = mock.Object;

            var order = new Order(product, amount);
            order.Fill(warehouse);
            try
            {
                order.Fill(warehouse);
            }
            catch (OrderAlreadyFilledException)
            {
                
            }

            mock.Verify(warehouse => warehouse.TakeStock(product, amount), Times.Once);
        }
    }
}

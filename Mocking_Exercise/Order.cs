using Mocking_Exercise.Interfaces;
using System;

namespace Mocking_Exercise
{
    public class Order
    {
        private string product;

        private int amount;

        public Order(string product, int amount)
        {
            this.product = product;
            this.amount = amount;
        }

        public bool IsFilled
        {
            get;
            private set;
        }

        public bool CanFillOrder(IWarehouse warehouse)
        {
            throw new NotImplementedException();
        }

        public void Fill(IWarehouse warehouse)
        {
            throw new NotImplementedException();
        }
    }
}

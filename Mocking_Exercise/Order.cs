using Mocking_Exercise.Exceptions;
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
            if (string.IsNullOrWhiteSpace(product))
                throw new ArgumentNullException();

            if (amount < 1)
                throw new ArgumentOutOfRangeException();

            this.product = product;
            this.amount = amount;
            this.IsFilled = false;
        }

        public bool IsFilled
        {
            get;
            private set;
        }

        public bool CanFillOrder(IWarehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException(nameof(warehouse));

            if (!warehouse.HasProduct(this.product))
                return false;

            if (warehouse.CurrentStock(product) < this.amount)
                return false;

            return true;
        }

        public void Fill(IWarehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException(nameof(warehouse));

            if (this.IsFilled)
                throw new OrderAlreadyFilledException();

            if (!this.CanFillOrder(warehouse))
                return;

            warehouse.TakeStock(this.product, this.amount);
            this.IsFilled = true;
        }
    }
}

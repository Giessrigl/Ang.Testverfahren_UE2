using Mocking_Exercise.Exceptions;
using Mocking_Exercise.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Mocking_Exercise
{
    public class StandardWarehouse : IWarehouse
    {
        private ConcurrentDictionary<string, int> products;

        public void AddStock(string product, int amount)
        {
            if (string.IsNullOrWhiteSpace(product))
                throw new ArgumentNullException();

            if (this.products.TryGetValue(product, out int currentAmount))
            {
                amount += currentAmount;
                this.products.TryUpdate(product, amount, currentAmount);
            }

            this.products.TryAdd(product, amount);
        }

        public int CurrentStock(string product)
        {
            if (string.IsNullOrWhiteSpace(product))
                throw new ArgumentNullException();

            if (!HasProduct(product))
                throw new NoSuchProductException($"There is no product with name: {product}.", product);

            if (this.products.TryGetValue(product, out int currentAmount))
            {
                return currentAmount;
            }

            return 0;
        }

        public bool HasProduct(string product)
        {
            if (string.IsNullOrWhiteSpace(product))
                throw new ArgumentNullException();

            return this.products.ContainsKey(product);
        }

        public void TakeStock(string product, int amount)
        {
            if (string.IsNullOrWhiteSpace(product))
                throw new ArgumentNullException();

            if (!HasProduct(product))
                throw new NoSuchProductException($"There is no product with name: {product}.", product);

            this.products.TryGetValue(product, out int currentStock);
            if (currentStock < amount)
                throw new InsufficientStockException($"There are only {currentStock} items of {product} available but ordered were {amount}.", product);

            amount = currentStock -= amount;
            this.products.TryUpdate(product, amount, currentStock);
        }
    }
}

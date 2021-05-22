using Mocking_Exercise.Interfaces;
using System;

namespace Mocking_Exercise
{
    public class StandardWarehouse : IWarehouse
    {
        public void AddStock(string product, int amount)
        {
            throw new NotImplementedException();
        }

        public int CurrentStock(string product)
        {
            throw new NotImplementedException();
        }

        public bool HasProduct(string product)
        {
            throw new NotImplementedException();
        }

        public void TakeStock(string product, int amount)
        {
            throw new NotImplementedException();
        }
    }
}

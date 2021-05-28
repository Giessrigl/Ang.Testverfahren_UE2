using Mocking_Exercise.Interfaces;
using System;

namespace Mocking_Exercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWarehouse warehouse = new StandardWarehouse();
            warehouse.AddStock("bread", 100);
            warehouse.AddStock("butter", 0);
            warehouse.AddStock("chocolate bar", 500);
            warehouse.AddStock("egg", 10);
            warehouse.AddStock("toilet paper", 1000);
            warehouse.AddStock("waterbottle", 100);

        }
    }
}

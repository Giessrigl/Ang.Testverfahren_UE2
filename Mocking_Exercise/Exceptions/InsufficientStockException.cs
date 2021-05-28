using System;
using System.Collections.Generic;
using System.Text;

namespace Mocking_Exercise.Exceptions
{
    [Serializable]
    public class InsufficientStockException : Exception
    {
        public string ProductName
        {
            get;
        }

        public InsufficientStockException()
        {

        }

        public InsufficientStockException(string message) : base(message)
        {

        }

        public InsufficientStockException(string message, Exception inner) : base(message, inner)
        {

        }

        public InsufficientStockException(string message, string productName) : this(message)
        {
            ProductName = productName;
        }
    }
}

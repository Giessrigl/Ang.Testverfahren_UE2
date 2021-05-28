using System;
using System.Collections.Generic;
using System.Text;

namespace Mocking_Exercise.Exceptions
{
    public class NoSuchProductException : Exception
    {
        public string ProductName
        {
            get;
        }

        public NoSuchProductException()
        {

        }

        public NoSuchProductException(string message) : base(message)
        {

        }

        public NoSuchProductException(string message, Exception inner) : base(message, inner)
        {

        }

        public NoSuchProductException(string message, string productName) : this(message)
        {
            ProductName = productName;
        }
    }
}

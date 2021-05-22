using System;
using System.Collections.Generic;
using System.Text;

namespace Mocking_Exercise.Exceptions
{
    public class OrderAlreadyFilledException : Exception
    {
        public OrderAlreadyFilledException()
        {

        }

        public OrderAlreadyFilledException(string message) : base(message)
        {

        }

        public OrderAlreadyFilledException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}

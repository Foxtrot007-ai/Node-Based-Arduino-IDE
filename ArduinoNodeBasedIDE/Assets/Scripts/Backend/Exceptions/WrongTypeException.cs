using System;

namespace Backend.Exceptions
{
    public class WrongTypeException : Exception
    {
        public WrongTypeException(string message) : base(message)
        {
        }
    }
}

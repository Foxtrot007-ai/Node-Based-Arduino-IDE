using System;
using Backend.API;

namespace Backend.Exceptions
{
    public class InOutMustBeConnectedException : Exception
    {
        public InOutMustBeConnectedException(IConnection inOut)
        {
            InOut = inOut;
        }
        public IConnection InOut { get;  }
    }
}

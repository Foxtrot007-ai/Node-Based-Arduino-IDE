using Backend.API;
using Backend.IO;
using Backend.Type;
using NUnit.Framework;

namespace Tests.EditMode.ut.Backend.Helpers
{
    public static class ExpectHelper
    {
        public static void TypeEqual(IType type, IConnection inOut)
        {
            Assert.AreEqual(type, ((TypeIO)inOut).MyType);
        }

    }
}

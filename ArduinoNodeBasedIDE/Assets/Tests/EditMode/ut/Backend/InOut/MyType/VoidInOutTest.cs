using System.Collections.Generic;
using Backend.Exceptions.InOut;
using Backend.InOut;
using Backend.InOut.MyType;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.InOut.MyType
{
    [TestFixture]
    [TestOf(typeof(VoidInOut))]
    [Category("InOut")]
    public class VoidInOutTest
    {
        private VoidInOut _voidOut;

        [SetUp]
        public void Setup()
        {
            _voidOut = (VoidInOut) InOutHelper.CreateBaseInOut(InOutType.Void);
        }

        private static List<IInOut> _wrong = new()
        {
            InOutHelper.CreateBaseInOut(InOutType.Primitive, InOutSide.Input),
            InOutHelper.CreateBaseInOut(InOutType.String, InOutSide.Input),
            InOutHelper.CreateBaseInOut(InOutType.Class, InOutSide.Input),
            InOutHelper.CreateBaseInOut(InOutType.Flow,InOutSide.Input),
            // InOutHelper.CreateVoid(), //Same side exception
        };

        [Test]
        [TestCaseSource(nameof(_wrong))]
        public void WrongConnectionTypeException(IInOut input)
        {
            Assert.Throws<WrongConnectionTypeException>(() => _voidOut.Connect(input));
        }
    }
}
using System.Collections.Generic;
using Backend.Exceptions.InOut;
using Backend.InOut;
using Backend.InOut.Base;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.InOut.Base
{
    [TestFixture]
    [TestOf(typeof(PrimitiveInOut))]
    [Category("InOut")]
    public class PrimitiveInOutTest
    {
        private PrimitiveInOut _primitiveIn;
        
        [SetUp]
        public void Setup()
        {
            _primitiveIn = (PrimitiveInOut) InOutHelper.CreateBaseInOut(InOutType.Primitive, InOutSide.Input);
        }
        
        private static List<IInOut> _wrong = new()
        {
            InOutHelper.CreateBaseInOut(InOutType.Class),
            InOutHelper.CreateBaseInOut(InOutType.Flow),
            InOutHelper.CreateBaseInOut(InOutType.Void),
        };
        
        [Test]
        [TestCaseSource(nameof(_wrong))]
        public void WrongConnectionTypeException(IInOut output)
        {
            Assert.Throws<WrongConnectionTypeException>(() => _primitiveIn.Connect(output));
        }
        
        private static List<IInOut> _ok = new()
        {
            InOutHelper.CreateBaseInOut(InOutType.Primitive),
            InOutHelper.CreateBaseInOut(InOutType.String),
        };
        
        [Test]
        [TestCaseSource(nameof(_ok))]
        public void ConnectionOk(IInOut output)
        {
            //given
            //when
            _primitiveIn.Connect(output);
            //then
            InOutHelper.ExpectAreConnected(_primitiveIn, output);
        }
    }
}
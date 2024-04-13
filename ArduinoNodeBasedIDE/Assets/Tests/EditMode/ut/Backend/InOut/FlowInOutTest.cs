using System.Collections.Generic;
using Backend.Exceptions.InOut;
using Backend.InOut;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.InOut.MyType
{
    [TestFixture]
    [TestOf(typeof(FlowInOut))]
    [Category("InOut")]
    public class FlowInOutTest
    {

        private FlowInOut _flowIn;

        [SetUp]
        public void Init()
        {
            _flowIn = (FlowInOut) InOutHelper.CreateBaseInOut(InOutType.Flow, InOutSide.Input);
        }

        private static List<IInOut> _wrong = new()
        {
            InOutHelper.CreateBaseInOut(InOutType.Primitive),
            InOutHelper.CreateBaseInOut(InOutType.String),
            InOutHelper.CreateBaseInOut(InOutType.Class),
            InOutHelper.CreateBaseInOut(InOutType.Void),
        };

        [Test]
        [TestCaseSource(nameof(_wrong))]
        public void WrongConnectionTypeException(IInOut output)
        {
            Assert.Throws<WrongConnectionTypeException>(() =>  _flowIn.Connect(output));
        }

        private static List<IInOut> _ok = new()
        {
            InOutHelper.CreateBaseInOut(InOutType.Flow)
        };
        
        [Test]
        [TestCaseSource(nameof(_ok))]
        public void ConnectionOk(IInOut output)
        {
            //given
            //when
            _flowIn.Connect(output);
            //then
            InOutHelper.ExpectAreConnected(_flowIn, output);
        }

    }
}
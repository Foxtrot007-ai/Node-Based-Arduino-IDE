using System.Collections.Generic;
using Backend.Connection;
using Backend.Exceptions.InOut;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Connection
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
            _flowIn = InOutHelper.CreateFlowInOut(InOutSide.Input);
        }

        private static List<InOut> _wrong = new()
        {
            InOutHelper.CreateBaseMock(),
            InOutHelper.CreateMyTypeInOutMock(),
        };

        [Test]
        [TestCaseSource(nameof(_wrong))]
        public void WrongConnectionTypeException(InOut output)
        {
            Assert.Throws<WrongConnectionTypeException>(() =>  _flowIn.Connect(output));
        }
        
        [Test]
        public void ConnectionOk()
        {
            //given
            var output = InOutHelper.CreateFlowInOut();
            //when
            _flowIn.Connect(output);
            //then
            InOutHelper.ExpectAreConnected(_flowIn, output);
        }

    }
}
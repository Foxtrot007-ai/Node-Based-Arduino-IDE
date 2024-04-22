using System.Collections.Generic;
using Backend.Connection;
using Backend.Exceptions.InOut;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Connection
{
    [TestFixture]
    [TestOf(nameof(MyTypeInOut))]
    [Category("InOut")]
    public class MyTypeInOutTest
    {
        private MyTypeInOut _myTypeInput;

        [SetUp]
        public void Init()
        {
            _myTypeInput = InOutHelper.CreateMyTypeInOutMock(InOutSide.Input);
        }

        private static List<InOut> _wrong = new()
        {
            InOutHelper.CreateBaseMock(),
            InOutHelper.CreateFlowInOut(),
        };

        [Test]
        [TestCaseSource(nameof(_wrong))]
        public void ConnectionWrongConnectionTypeException(InOut output)
        {
            //given
            //when
            var exception = Assert.Throws<WrongConnectionTypeException>(() => _myTypeInput.Connect(output));
            //then
        }

        [Test]
        public void ConnectionCannotBeCastException()
        {
            //given
            var output = InOutHelper.CreateMyTypeInOutMock();
            output.MyType.CanBeCast(output.MyType).Returns(false);
            //when
            var exception = Assert.Throws<CannotBeCastException>(() => _myTypeInput.Connect(output));
            //then
            output.MyType.Received().CanBeCast(_myTypeInput.MyType);
            _myTypeInput.MyType.DidNotReceiveWithAnyArgs().CanBeCast(default);
        }

        [Test]
        public void ConnectionOk()
        {
            //given
            var output = InOutHelper.CreateMyTypeInOutMock();
            output.MyType.CanBeCast(_myTypeInput.MyType).Returns(true);
            //when
            _myTypeInput.Connect(output);
            //then
            InOutHelper.ExpectAreConnected(_myTypeInput, output);
            output.MyType.Received().CanBeCast(_myTypeInput.MyType);
            _myTypeInput.MyType.DidNotReceiveWithAnyArgs().CanBeCast(default);
        }

    }
}

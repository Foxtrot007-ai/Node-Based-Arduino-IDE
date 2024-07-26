using Backend.Connection;
using Backend.Connection.MyType;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Connection
{
    [TestFixture]
    [TestOf(typeof(AutoInOut))]
    [Category("InOut")]
    public class AutoInOutTest
    {
        private AutoInOut _autoInput;
        private MyTypeInOut _myTypeOutput;
        [SetUp]
        public void Init()
        {
            _autoInput = InOutHelper.CreateAutoInOut(InOutSide.Input);
            _myTypeOutput = InOutHelper.CreateMyTypeInOutMock();
        }

        [Test]
        public void ShouldSetTypeAfterConnectAndDisconnect()
        {
            //given
            InOutHelper.Connect(_autoInput, _myTypeOutput);
            Assert.AreSame(_myTypeOutput.MyType, _autoInput.MyType);
            //when
            _autoInput.Disconnect();
            //then
            InOutHelper.ExpectAreNotConnected(_autoInput, _myTypeOutput);
            Assert.Null(_autoInput.MyType);
        }

        [Test]
        public void ShouldNotSetTypeAfterConnectAndDisconnectWhenTypeWasChange()
        {
            //given
            var newMyType = MockHelper.CreateType();
            _autoInput.ChangeMyType(newMyType);
            Assert.AreSame(newMyType, _autoInput.MyType);
            //when
            InOutHelper.Connect(_autoInput, _myTypeOutput);

            Assert.AreNotSame(_autoInput.MyType, _myTypeOutput.MyType);
            Assert.AreSame(newMyType, _autoInput.MyType);

            _autoInput.Disconnect();
            InOutHelper.ExpectAreNotConnected(_autoInput, _myTypeOutput);
            Assert.AreNotSame(_autoInput.MyType, _myTypeOutput.MyType);
            Assert.AreSame(newMyType, _autoInput.MyType);
        }

        [Test]
        public void ShouldSetNullTypeAfterReset()
        {
            //given
            var newMyType = MockHelper.CreateType();
            _autoInput.ChangeMyType(newMyType);
            Assert.AreSame(newMyType, _autoInput.MyType);
            //when
            _autoInput.ResetMyType();
            //then
            Assert.Null(_autoInput.MyType);
        }

        [Test]
        public void ShouldSetTypeToConnectedTypeAfterReset()
        {
            //given
            var newMyType = MockHelper.CreateType();
            _autoInput.ChangeMyType(newMyType);
            Assert.AreSame(newMyType, _autoInput.MyType);
            //when
            InOutHelper.Connect(_autoInput, _myTypeOutput);
            Assert.AreNotSame(_autoInput.MyType, _myTypeOutput.MyType);
            Assert.AreSame(newMyType, _autoInput.MyType);

            _autoInput.ResetMyType();
            //then 
            InOutHelper.ExpectAreConnected(_autoInput, _myTypeOutput);
            Assert.AreSame(_autoInput.MyType, _myTypeOutput.MyType);
            Assert.AreNotSame(newMyType, _autoInput.MyType);
        }

        [Test]
        public void ShouldUpdateTypeAfterConnectedChangeType()
        {
            //given
            var anyOutput = InOutHelper.CreateAnyInOut();
            InOutHelper.Connect(_autoInput, anyOutput);

            Assert.AreSame(anyOutput.MyType, _autoInput.MyType);
            //when
            var newType = MockHelper.CreateType();
            newType.CanBeCast(newType).Returns(true);
            anyOutput.ChangeMyType(newType);
            Assert.AreSame(newType, anyOutput.MyType);
            //then
            InOutHelper.ExpectAreConnected(_autoInput, anyOutput);
            Assert.AreSame(anyOutput.MyType, _autoInput.MyType);
        }

        [Test]
        public void MyTypeShouldBeNullIfAutoAutoConnection()
        {
            //given
            var autoOutput = InOutHelper.CreateAutoInOut(InOutSide.Output);
            //when
            _autoInput.Connect(autoOutput);
            //then
            InOutHelper.ExpectAreConnected(autoOutput, _autoInput);
            Assert.IsNull(autoOutput.MyType);
            Assert.IsNull(_autoInput.MyType);
        }
    }
}

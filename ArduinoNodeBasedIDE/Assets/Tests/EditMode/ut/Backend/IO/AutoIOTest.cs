using Backend.Exceptions;
using Backend.Exceptions.InOut;
using Backend.IO;
using Backend.Node;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.IO
{
    [TestFixture]
    [TestOf(typeof(AutoIO))]
    [Category("IO")]
    public class AutoIOTest
    {
        private AutoIO _autoInput;
        private TypeIO _myTypeOutput;
        [SetUp]
        public void Init()
        {
            _autoInput = IOHelper.CreateAutoIO(IOSide.Input);
            _myTypeOutput = IOHelper.CreateTypeIO();
        }

        [Test]
        public void ShouldSetTypeAfterConnectAndDisconnect()
        {
            //given
            IOHelper.ConnectAuto(_autoInput, _myTypeOutput);
            Assert.AreSame(_myTypeOutput.MyType, _autoInput.MyType);
            //when
            _autoInput.Disconnect();
            //then
            IOHelper.ExpectAreNotConnected(_autoInput, _myTypeOutput);
            Assert.Null(_autoInput.MyType);
        }

        [Test]
        public void ShouldNotSetTypeAfterConnectAndDisconnectWhenTypeWasChange()
        {
            //given
            var newMyType = MockHelper.CreateType();
            _autoInput.ChangeType(newMyType);
            Assert.AreSame(newMyType, _autoInput.MyType);
            //when
            IOHelper.Connect(_autoInput, _myTypeOutput);

            Assert.AreNotSame(_autoInput.MyType, _myTypeOutput.MyType);
            Assert.AreSame(newMyType, _autoInput.MyType);

            _autoInput.Disconnect();
            IOHelper.ExpectAreNotConnected(_autoInput, _myTypeOutput);
            Assert.AreNotSame(_autoInput.MyType, _myTypeOutput.MyType);
            Assert.AreSame(newMyType, _autoInput.MyType);
        }

        [Test]
        public void ShouldSetNullTypeAfterReset()
        {
            //given
            var newMyType = MockHelper.CreateType();
            _autoInput.ChangeType(newMyType);
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
            _autoInput.ChangeType(newMyType);
            Assert.AreSame(newMyType, _autoInput.MyType);
            //when
            IOHelper.Connect(_autoInput, _myTypeOutput);
            Assert.AreNotSame(_autoInput.MyType, _myTypeOutput.MyType);
            Assert.AreSame(newMyType, _autoInput.MyType);

            _autoInput.ResetMyType();
            //then 
            IOHelper.ExpectAreConnected(_autoInput, _myTypeOutput);
            Assert.AreSame(_autoInput.MyType, _myTypeOutput.MyType);
            Assert.AreNotSame(newMyType, _autoInput.MyType);
        }

        [Test]
        public void ShouldUpdateTypeAfterConnectedChangeType()
        {
            //given
            IOHelper.ConnectAuto(_autoInput, _myTypeOutput);

            Assert.AreSame(_myTypeOutput.MyType, _autoInput.MyType);
            //when
            var newType = MockHelper.CreateType();
            newType.CanBeCast(newType).Returns(true);
            _myTypeOutput.ChangeType(newType);
            Assert.AreSame(newType, _myTypeOutput.MyType);
            //then
            IOHelper.ExpectAreConnected(_autoInput, _myTypeOutput);
            Assert.AreSame(_myTypeOutput.MyType, _autoInput.MyType);
        }

        [Test]
        public void MyTypeShouldBeNullIfAutoAutoConnection()
        {
            //given
            var autoOutput = IOHelper.CreateAutoIO(IOSide.Output);
            //when
            _autoInput.Connect(autoOutput);
            //then
            IOHelper.ExpectAreConnected(autoOutput, _autoInput);
            Assert.IsNull(autoOutput.MyType);
            Assert.IsNull(_autoInput.MyType);
        }

        [Test]
        public void AutoCannotBeClassChangeTypeTest()
        {
            var autoI = new AutoIO(Substitute.ForPartsOf<BaseNode>(), IOSide.Input, false);

            Assert.Throws<WrongTypeException>(() => autoI.ChangeType(MockHelper.CreateClassType("test")));
        }

        [Test]
        public void AutoCannotBeClassConnectTest()
        {
            var autoI = new AutoIO(Substitute.ForPartsOf<BaseNode>(), IOSide.Input, false);
            var classO = IOHelper.CreateTypeIO(myType: MockHelper.CreateClassType("test"));


            Assert.Throws<WrongConnectionTypeException>(() => autoI.Connect(classO));
        }
    }
}

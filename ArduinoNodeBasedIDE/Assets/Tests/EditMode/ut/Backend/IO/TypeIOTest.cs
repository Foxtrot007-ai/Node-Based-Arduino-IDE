using System;
using System.Collections.Generic;
using Backend.Exceptions;
using Backend.Exceptions.InOut;
using Backend.IO;
using Backend.Type;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.IO
{
    [TestFixture]
    [TestOf(nameof(TypeIO))]
    [Category("IO")]
    public class TypeIOTest
    {
        private TypeIO _typeInput;
        private ISubscribeIO _sub1;

        [SetUp]
        public void Init()
        {
            _sub1 = Substitute.For<ISubscribeIO>();
            _typeInput = IOHelper.CreateTypeIO(IOSide.Input);
            _typeInput.Subscribe(_sub1);
        }

        private static List<BaseIO> _wrong = new()
        {
            IOHelper.CreateBaseMock(),
            IOHelper.CreateFlowIO(),
        };

        private void ExpectNoNotify()
        {
            _sub1.DidNotReceiveWithAnyArgs().ConnectNotify(default);
            _sub1.DidNotReceiveWithAnyArgs().DisconnectNotify(default);
            _sub1.DidNotReceiveWithAnyArgs().TypeChangeNotify(default);
        }

        private void ExpectConnectNotify()
        {
            _sub1.Received().ConnectNotify(_typeInput);
            _sub1.DidNotReceiveWithAnyArgs().DisconnectNotify(default);
            _sub1.DidNotReceiveWithAnyArgs().TypeChangeNotify(default);
        }

        private void ExpectFullNotify()
        {
            _sub1.Received().ConnectNotify(_typeInput);
            _sub1.Received().TypeChangeNotify(_typeInput);
            _sub1.Received().DisconnectNotify(_typeInput);
        }
        [Test]
        [TestCaseSource(nameof(_wrong))]
        public void ConnectionWrongConnectionTypeException(BaseIO output)
        {
            var exception = Assert.Throws<WrongConnectionTypeException>(() => _typeInput.Connect(output));

            ExpectNoNotify();
        }

        [Test]
        public void ConnectionCannotBeCastException()
        {

            var output = IOHelper.CreateTypeIO();
            output.MyType.CanBeCast(_typeInput.MyType).Returns(false);

            var exception = Assert.Throws<CannotBeCastException>(() => _typeInput.Connect(output));

            output.MyType.Received().CanBeCast(_typeInput.MyType);
            _typeInput.MyType.DidNotReceiveWithAnyArgs().CanBeCast(default);
            ExpectNoNotify();
        }

        [Test]
        public void ConnectionNeedAdapterException()
        {

            var output = IOHelper.CreateTypeIO();
            output.MyType.CanBeCast(_typeInput.MyType).Returns(true);
            output.MyType.IsAdapterNeed(_typeInput.MyType).Returns(true);

            var exception = Assert.Throws<AdapterNeedException>(() => _typeInput.Connect(output));

            output.MyType.Received().CanBeCast(_typeInput.MyType);
            _typeInput.MyType.DidNotReceiveWithAnyArgs().CanBeCast(default);
            ExpectNoNotify();
        }

        [Test]
        public void ConnectionOk()
        {
            var output = IOHelper.CreateTypeIO();
            output.MyType.CanBeCast(_typeInput.MyType).Returns(true);
            output.MyType.IsAdapterNeed(_typeInput.MyType).Returns(false);

            _typeInput.Connect(output);

            IOHelper.ExpectAreConnected(_typeInput, output);
            output.MyType.Received().CanBeCast(_typeInput.MyType);
            _typeInput.MyType.DidNotReceiveWithAnyArgs().CanBeCast(default);
            ExpectConnectNotify();
        }

        [Test]
        public void ShouldNotCheckIfConnectToAuto()
        {
            //given
            var output = IOHelper.CreateTypeIO(IOSide.Output);
            var input = IOHelper.CreateAutoIO(IOSide.Input);
            //when
            output.Connect(input);
            //then
            IOHelper.ExpectAreConnected(input, output);
            output.MyType.DidNotReceiveWithAnyArgs().CanBeCast(default);
        }

        [Test]
        public void ChangeTypeNullArgumentException()
        {
            //given
            //when
            var exception = Assert.Throws<ArgumentNullException>(() => _typeInput.ChangeType(null));
            //then 
            Assert.AreEqual("Cannot change type to null.", exception.Message);
            ExpectNoNotify();
        }

        [Test]
        public void ChangeTypeVoidWrongTypeException()
        {
            //given
            //when
            var newType = MockHelper.CreateType(EType.Void);
            var exception = Assert.Throws<WrongTypeException>(() => _typeInput.ChangeType(newType));
            //then 
            Assert.AreEqual("Cannot change type to void for input side.", exception.Message);
            ExpectNoNotify();
        }

        [Test]
        public void ChangeTypeDisconnectAndCannotBeCastException()
        {
            var output = IOHelper.CreateTypeIO();
            IOHelper.Connect(_typeInput, output);

            var newType = MockHelper.CreateType();
            output.MyType.CanBeCast(newType).Returns(false);

            Assert.Throws<CannotBeCastException>(() => _typeInput.ChangeType(newType));

            Assert.AreSame(newType, _typeInput.MyType);
            IOHelper.ExpectNullConnected(_typeInput, output);
            ExpectFullNotify();
        }

        [Test]
        public void ChangeTypeDisconnectAndNeedAdapterException()
        {
            //given
            var output = IOHelper.CreateTypeIO();
            IOHelper.Connect(_typeInput, output);

            var newType = MockHelper.CreateType();
            output.MyType.CanBeCast(newType).Returns(true);
            output.MyType.IsAdapterNeed(newType).Returns(true);
            //when
            Assert.Throws<AdapterNeedException>(() => _typeInput.ChangeType(newType));
            //then 
            Assert.AreSame(newType, _typeInput.MyType);
            IOHelper.ExpectNullConnected(_typeInput, output);
            ExpectFullNotify();
        }

        [Test]
        public void ChangeTypeNoDisconnect()
        {
            //given
            var output = IOHelper.CreateTypeIO();
            IOHelper.Connect(_typeInput, output);

            var newType = MockHelper.CreateType();
            output.MyType.CanBeCast(newType).Returns(true);
            output.MyType.IsAdapterNeed(newType).Returns(false);
            //when
            _typeInput.ChangeType(newType);
            //then 
            Assert.AreSame(newType, _typeInput.MyType);
            IOHelper.ExpectAreConnected(_typeInput, output);
            
            _sub1.Received().ConnectNotify(_typeInput);
            _sub1.DidNotReceiveWithAnyArgs().DisconnectNotify(default);
            _sub1.Received().TypeChangeNotify(_typeInput);
        }
    }
}

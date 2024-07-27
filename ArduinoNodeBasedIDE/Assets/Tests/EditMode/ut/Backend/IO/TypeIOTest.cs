using System;
using System.Collections.Generic;
using Backend.Connection;
using Backend.Exceptions;
using Backend.Exceptions.InOut;
using Backend.Type;
using NSubstitute;
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

        [SetUp]
        public void Init()
        {
            _typeInput = IOHelper.CreateTypeIO(IOSide.Input);
        }

        private static List<BaseIO> _wrong = new()
        {
            IOHelper.CreateBaseMock(),
            IOHelper.CreateFlowIO(),
        };

        [Test]
        [TestCaseSource(nameof(_wrong))]
        public void ConnectionWrongConnectionTypeException(BaseIO output)
        {
            //given
            //when
            var exception = Assert.Throws<WrongConnectionTypeException>(() => _typeInput.Connect(output));
            //then
        }

        [Test]
        public void ConnectionCannotBeCastException()
        {
            //given
            var output = IOHelper.CreateTypeIO();
            output.MyType.CanBeCast(_typeInput.MyType).Returns(false);
            //when
            var exception = Assert.Throws<CannotBeCastException>(() => _typeInput.Connect(output));
            //then
            output.MyType.Received().CanBeCast(_typeInput.MyType);
            _typeInput.MyType.DidNotReceiveWithAnyArgs().CanBeCast(default);
        }

        [Test]
        public void ConnectionNeedAdapterException()
        {
            //given
            var output = IOHelper.CreateTypeIO();
            output.MyType.CanBeCast(_typeInput.MyType).Returns(true);
            output.MyType.IsAdapterNeed(_typeInput.MyType).Returns(true);
            //when
            var exception = Assert.Throws<AdapterNeedException>(() => _typeInput.Connect(output));
            //then
            output.MyType.Received().CanBeCast(_typeInput.MyType);
            _typeInput.MyType.DidNotReceiveWithAnyArgs().CanBeCast(default);
        }

        [Test]
        public void ConnectionOk()
        {
            //given
            var output = IOHelper.CreateTypeIO();
            output.MyType.CanBeCast(_typeInput.MyType).Returns(true);
            output.MyType.IsAdapterNeed(_typeInput.MyType).Returns(false);

            //when
            _typeInput.Connect(output);
            //then
            IOHelper.ExpectAreConnected(_typeInput, output);
            output.MyType.Received().CanBeCast(_typeInput.MyType);
            _typeInput.MyType.DidNotReceiveWithAnyArgs().CanBeCast(default);
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
        }

        [Test]
        public void ChangeTypeDisconnectAndCannotBeCastException()
        {
            //given
            var output = IOHelper.CreateTypeIO();
            IOHelper.Connect(_typeInput, output);

            var newType = MockHelper.CreateType();
            output.MyType.CanBeCast(newType).Returns(false);
            //when
            Assert.Throws<CannotBeCastException>(() => _typeInput.ChangeType(newType));
            //then 
            Assert.AreSame(newType, _typeInput.MyType);
            IOHelper.ExpectNullConnected(_typeInput, output);
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
        }
    }
}

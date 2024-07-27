using System;
using Backend.Connection;
using Backend.Exceptions;
using Backend.Exceptions.InOut;
using Backend.Type;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Connection
{
    [TestFixture]
    [TestOf(nameof(AnyInOut))]
    [Category("InOut")]
    public class AnyInOutTest
    {
        private AnyInOut _anyInput;

        [SetUp]
        public void Init()
        {
            _anyInput = InOutHelper.CreateAnyInOut(InOutSide.Input);
        }

        [Test]
        public void ChangeTypeNullArgumentException()
        {
            //given
            //when
            var exception = Assert.Throws<ArgumentNullException>(() => _anyInput.ChangeMyType(null));
            //then 
            Assert.AreEqual("Cannot change type to null.", exception.Message);
        }

        [Test]
        public void ChangeTypeVoidWrongTypeException()
        {
            //given
            //when
            var newType = MockHelper.CreateType(EType.Void);
            var exception = Assert.Throws<WrongTypeException>(() => _anyInput.ChangeMyType(newType));
            //then 
            Assert.AreEqual("Cannot change type to void for input side.", exception.Message);
        }

        [Test]
        public void ChangeTypeDisconnectAndCannotBeCastException()
        {
            //given
            var output = InOutHelper.CreateTypeInOut();
            InOutHelper.Connect(_anyInput, output);

            var newType = MockHelper.CreateType();
            output.MyType.CanBeCast(newType).Returns(false);
            //when
            Assert.Throws<CannotBeCastException>(() => _anyInput.ChangeMyType(newType));
            //then 
            Assert.AreSame(newType, _anyInput.MyType);
            InOutHelper.ExpectNullConnected(_anyInput, output);
        }

        [Test]
        public void ChangeTypeDisconnectAndNeedAdapterException()
        {
            //given
            var output = InOutHelper.CreateTypeInOut();
            InOutHelper.Connect(_anyInput, output);

            var newType = MockHelper.CreateType();
            output.MyType.CanBeCast(newType).Returns(true);
            output.MyType.IsAdapterNeed(newType).Returns(true);
            //when
            Assert.Throws<AdapterNeedException>(() => _anyInput.ChangeMyType(newType));
            //then 
            Assert.AreSame(newType, _anyInput.MyType);
            InOutHelper.ExpectNullConnected(_anyInput, output);
        }

        [Test]
        public void ChangeTypeNoDisconnect()
        {
            //given
            var output = InOutHelper.CreateTypeInOut();
            InOutHelper.Connect(_anyInput, output);

            var newType = MockHelper.CreateType();
            output.MyType.CanBeCast(newType).Returns(true);
            output.MyType.IsAdapterNeed(newType).Returns(false);
            //when
            _anyInput.ChangeMyType(newType);
            //then 
            Assert.AreSame(newType, _anyInput.MyType);
            InOutHelper.ExpectAreConnected(_anyInput, output);
        }
    }
}

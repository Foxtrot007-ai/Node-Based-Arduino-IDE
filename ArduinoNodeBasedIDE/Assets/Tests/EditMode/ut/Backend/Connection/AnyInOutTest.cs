using System;
using Backend.Connection;
using Backend.Connection.MyType;
using Backend.Exceptions.InOut;
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

        private void Connect(MyTypeInOut input, MyTypeInOut output)
        {
            output.MyType.CanBeCast(input.MyType).Returns(true);
            output.Connect(input);
            InOutHelper.ExpectAreConnected(output, input);
        }
        
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
        public void ChangeTypeDisconnectAndCannotBeCastException()
        {
            //given
            var output = InOutHelper.CreateMyTypeInOutMock();
            Connect(_anyInput, output);

            var newType = TypeHelper.CreateMyTypeMock();
            output.MyType.CanBeCast(newType).Returns(false);
            //when
            Assert.Throws<CannotBeCastException>(() => _anyInput.ChangeMyType(newType));
            //then 
            Assert.AreSame(newType, _anyInput.MyType);
            InOutHelper.ExpectNullConnected(_anyInput, output);
        }

        [Test]
        public void ChangeTypeNoDisconnect()
        {
            //given
            var output = InOutHelper.CreateMyTypeInOutMock();
            Connect(_anyInput, output);
            
            var newType = TypeHelper.CreateMyTypeMock();
            output.MyType.CanBeCast(newType).Returns(true);
            //when
            _anyInput.ChangeMyType(newType);
            //then 
            Assert.AreSame(newType, _anyInput.MyType);
            InOutHelper.ExpectAreConnected(_anyInput, output);
        }
    }
}

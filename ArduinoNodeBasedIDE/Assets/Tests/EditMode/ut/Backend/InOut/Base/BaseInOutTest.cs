using System;
using System.Collections.Generic;
using Backend.Exceptions.InOut;
using Backend.InOut;
using Backend.InOut.Base;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.InOut.Base
{
    [TestFixture]
    [TestOf(typeof(BaseInOut))]
    [Category("InOut")]
    public class BaseInOutTest
    {
        
        [Test]
        public void ConnectionNullArgumentException()
        {
            //given
            var baseInOut1 = InOutHelper.CreateBaseMock(InOutSide.Input);
            //when
            Exception exception = Assert.Throws<ArgumentNullException>(() => baseInOut1.Connect(null));
            //then
        }

        [Test] 
        public void ConnectionAlreadyConnectedException()
        {
            //given
            var baseInOut1 = InOutHelper.CreateBaseMock(InOutSide.Input);
            var baseInOut2 = InOutHelper.CreateBaseMock(InOutSide.Output);
            var baseInOut3 = InOutHelper.CreateBaseMock(InOutSide.Input);
            InOutHelper.Connect(baseInOut1, baseInOut2);
            
            //when
            InOutException exception1 = Assert.Throws<AlreadyConnectedException>(() => baseInOut1.Connect(baseInOut3));
            InOutException exception2 = Assert.Throws<AlreadyConnectedException>(() => baseInOut3.Connect(baseInOut1));
            //then
        }
        
        [Test]
        [TestCase(InOutSide.Input, InOutSide.Input)]
        [TestCase(InOutSide.Output, InOutSide.Output)]
        public void ConnectionInputSameSideException(InOutSide side1, InOutSide side2)
        {
            //given
            var baseInOut1 = InOutHelper.CreateBaseMock(side1);
            var baseInOut2 = InOutHelper.CreateBaseMock(side2);
            //when
            InOutException exception = Assert.Throws<SameSideException>(() => baseInOut1.Connect(baseInOut2));
            //then
        }


        [Test]
        public void ConnectionSelfException()
        {
            //given
            var parent = NodeHelper.CreateBaseParent();
            var baseInOut1 = InOutHelper.CreateBaseMock(InOutSide.Input, parent);
            var baseInOut2 = InOutHelper.CreateBaseMock(InOutSide.Output, parent);
            //when
            InOutException exception = Assert.Throws<SelfConnectionException>(() => baseInOut1.Connect(baseInOut2));
            //then
        }

        private static List<InOutType> _wrongType = new()
        {
            InOutType.Void,
            InOutType.Dynamic
        };
        
        [Test]
        [TestCaseSource(nameof(_wrongType))]
        public void WrongConnectionTypeException(InOutType inOutType)
        {
            //given
            var baseInOut1 = InOutHelper.CreateBaseMock(InOutSide.Input);
            var baseInOut2 = InOutHelper.CreateBaseMock(InOutSide.Output, inOutType: inOutType);
            //when
            //then
            Assert.Throws<WrongConnectionTypeException>(() => baseInOut1.Connect(baseInOut2));
        }
        
        [Test]
        public void ConnectionOk()
        {
            //given
            var baseInOut1 = InOutHelper.CreateBaseMock(InOutSide.Input);
            var baseInOut2 = InOutHelper.CreateBaseMock(InOutSide.Output);
            //when
            baseInOut1.Connect(baseInOut2);
            //then
            InOutHelper.ExpectAreConnected(baseInOut1, baseInOut2);
        }

        [Test]
        public void ConnectionCycleException()
        {
            //given
            var parent1 = NodeHelper.CreateBaseParent();
            var baseIn1 = InOutHelper.CreateBaseMock(InOutSide.Input, parent1);
            var baseOut1 = InOutHelper.CreateBaseMock(InOutSide.Output, parent1);

            var parent2 = NodeHelper.CreateBaseParent();
            var baseIn2 = InOutHelper.CreateBaseMock(InOutSide.Input, parent2);
            var baseOut2 = InOutHelper.CreateBaseMock(InOutSide.Output, parent2);

            var parent3 = NodeHelper.CreateBaseParent();
            var baseIn3 = InOutHelper.CreateBaseMock(InOutSide.Input, parent3);
            var baseOut3 = InOutHelper.CreateBaseMock(InOutSide.Output, parent3);

            InOutHelper.Connect(baseOut1,baseIn2);
            InOutHelper.Connect(baseOut2,baseIn3);

            //when
            InOutException exception = Assert.Throws<CycleException>(() => baseOut3.Connect(baseIn1));
            //then
        }

        [Test]
        public void DisconnectOk()
        {
            //given
            var baseInOut1 = InOutHelper.CreateBaseMock(InOutSide.Input);
            var baseInOut2 = InOutHelper.CreateBaseMock(InOutSide.Output);
            InOutHelper.Connect(baseInOut1, baseInOut2);
            //when
            baseInOut1.Disconnect();
            //then
            Assert.IsNull(baseInOut1.Connected);
            Assert.IsNull(baseInOut2.Connected);
        }

        [Test]
        public void ChangeTypeException()
        {
            //given
            var baseInOut = InOutHelper.CreateBaseMock(InOutSide.Input);
            //when
            InOutException exception = Assert.Throws<NotChangeableException>(() => baseInOut.ChangeType(null));
            //then
        }
        
        [Test]
        public void ReconnectOk()
        {
            //given
            var baseInOut1 = InOutHelper.CreateBaseMock(InOutSide.Input);
            var baseInOut2 = InOutHelper.CreateBaseMock(InOutSide.Output);
            //when
            baseInOut1.Reconnect(baseInOut2);
            //then
            InOutHelper.ExpectAreConnected(baseInOut1, baseInOut2);
        }
    }
}
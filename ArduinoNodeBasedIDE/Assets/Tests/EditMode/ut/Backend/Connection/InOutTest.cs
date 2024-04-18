using System;
using Backend.Connection;
using Backend.Exceptions.InOut;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Connection
{
    [TestFixture]
    [TestOf(typeof(InOut))]
    [Category("InOut")]
    public class InOutTest
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
            baseInOut1.Connect(baseInOut2);
            
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

            baseOut1.Connect(baseIn2);
            baseOut2.Connect(baseIn3);

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
            baseInOut1.Connect(baseInOut2);
            //when
            baseInOut1.Disconnect();
            //then
            Assert.IsNull(baseInOut1.Connected);
            Assert.IsNull(baseInOut2.Connected);
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
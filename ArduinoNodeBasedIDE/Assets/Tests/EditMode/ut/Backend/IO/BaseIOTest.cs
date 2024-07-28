using System;
using Backend.Connection;
using Backend.Exceptions.InOut;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;
using Tests.EditMode.ut.Backend.Mocks;

namespace Tests.EditMode.ut.Backend.IO
{
    [TestFixture]
    [TestOf(typeof(BaseIO))]
    [Category("IO")]
    public class BaseIOTest
    {

        [Test]
        public void ConnectionNullArgumentException()
        {
            //given
            var baseInOut1 = IOHelper.CreateBaseMock(IOSide.Input);
            //when
            var exception = Assert.Throws<ArgumentNullException>(() => baseInOut1.Connect(null));
            //then
        }

        [Test]
        public void ConnectionAlreadyConnectedException()
        {
            //given
            var baseInOut1 = IOHelper.CreateBaseMock(IOSide.Input);
            var baseInOut2 = IOHelper.CreateBaseMock(IOSide.Output);
            var baseInOut3 = IOHelper.CreateBaseMock(IOSide.Input);
            baseInOut1.Connect(baseInOut2);

            //when
            var exception1 = Assert.Throws<AlreadyConnectedException>(() => baseInOut1.Connect(baseInOut3));
            var exception2 = Assert.Throws<AlreadyConnectedException>(() => baseInOut3.Connect(baseInOut1));
            //then
        }

        [Test]
        [TestCase(IOSide.Input, IOSide.Input)]
        [TestCase(IOSide.Output, IOSide.Output)]
        public void ConnectionInputSameSideException(IOSide side1, IOSide side2)
        {
            //given
            var baseInOut1 = IOHelper.CreateBaseMock(side1);
            var baseInOut2 = IOHelper.CreateBaseMock(side2);
            //when
            var exception = Assert.Throws<SameSideException>(() => baseInOut1.Connect(baseInOut2));
            //then
        }


        [Test]
        public void ConnectionSelfException()
        {
            //given
            var parent = new BaseNodeMock();
            var baseInOut1 = IOHelper.CreateBaseMock(IOSide.Input, parent);
            var baseInOut2 = IOHelper.CreateBaseMock(IOSide.Output, parent);
            //when
            var exception = Assert.Throws<SelfConnectionException>(() => baseInOut1.Connect(baseInOut2));
            //then
        }

        [Test]
        public void ConnectionOk()
        {
            //given
            var baseInOut1 = IOHelper.CreateBaseMock(IOSide.Input);
            var baseInOut2 = IOHelper.CreateBaseMock(IOSide.Output);
            //when
            baseInOut1.Connect(baseInOut2);
            //then
            IOHelper.ExpectAreConnected(baseInOut1, baseInOut2);
        }

        [Test]
        public void ConnectionOkNotify()
        {
            //given
            var sub1 = Substitute.For<ISubscribeIO>();
            var sub12 = Substitute.For<ISubscribeIO>();
            var baseInOut1 = IOHelper.CreateBaseMock(IOSide.Input);
            baseInOut1.Subscribe(sub1);
            baseInOut1.Subscribe(sub12);

            var sub2 = Substitute.For<ISubscribeIO>();
            var baseInOut2 = IOHelper.CreateBaseMock(IOSide.Output);
            baseInOut2.Subscribe(sub2);
            baseInOut2.Subscribe(sub12);
            //when
            baseInOut1.Connect(baseInOut2);
            //then
            IOHelper.ExpectAreConnected(baseInOut1, baseInOut2);

            sub1.Received().ConnectNotify(baseInOut1);
            sub1.DidNotReceiveWithAnyArgs().DisconnectNotify(default);

            sub2.Received().ConnectNotify(baseInOut2);
            sub2.DidNotReceiveWithAnyArgs().DisconnectNotify(default);

            sub12.Received().ConnectNotify(baseInOut2);
            sub12.Received().ConnectNotify(baseInOut1);
            sub12.DidNotReceiveWithAnyArgs().DisconnectNotify(default);
        }

        [Test]
        public void ConnectionOkNotifyRemoved()
        {
            //given
            var sub1 = Substitute.For<ISubscribeIO>();
            var baseInOut1 = IOHelper.CreateBaseMock(IOSide.Input);
            baseInOut1.Subscribe(sub1);
            var sub2 = Substitute.For<ISubscribeIO>();
            var baseInOut2 = IOHelper.CreateBaseMock(IOSide.Output);
            baseInOut2.Subscribe(sub2);

            baseInOut1.Unsubscribe(sub1);
            //when
            baseInOut1.Connect(baseInOut2);
            //then
            IOHelper.ExpectAreConnected(baseInOut1, baseInOut2);

            sub1.DidNotReceiveWithAnyArgs().ConnectNotify(default);
            sub1.DidNotReceiveWithAnyArgs().DisconnectNotify(default);

            sub2.Received().ConnectNotify(baseInOut2);
            sub2.DidNotReceiveWithAnyArgs().DisconnectNotify(default);

        }

        [Test]
        public void ConnectionExceptionNotNotify()
        {
            //given
            var sub1 = Substitute.For<ISubscribeIO>();
            var baseInOut1 = IOHelper.CreateBaseMock(IOSide.Input);
            baseInOut1.Subscribe(sub1);

            var sub2 = Substitute.For<ISubscribeIO>();
            var baseInOut2 = IOHelper.CreateBaseMock(IOSide.Input);
            baseInOut2.Subscribe(sub2);
            //when
            Assert.Catch(() => baseInOut1.Connect(baseInOut2));
            //then

            sub1.DidNotReceiveWithAnyArgs().ConnectNotify(default);
            sub1.DidNotReceiveWithAnyArgs().DisconnectNotify(default);

            sub2.DidNotReceiveWithAnyArgs().ConnectNotify(default);
            sub2.DidNotReceiveWithAnyArgs().DisconnectNotify(default);
        }

        [Test]
        public void ConnectionCycleException()
        {
            //given
            var parent1 = new BaseNodeMock();
            var baseIn1 = IOHelper.CreateBaseMock(IOSide.Input, parent1);
            var baseOut1 = IOHelper.CreateBaseMock(IOSide.Output, parent1);

            var parent2 = new BaseNodeMock();
            var baseIn2 = IOHelper.CreateBaseMock(IOSide.Input, parent2);
            var baseOut2 = IOHelper.CreateBaseMock(IOSide.Output, parent2);

            var parent3 = new BaseNodeMock();
            var baseIn3 = IOHelper.CreateBaseMock(IOSide.Input, parent3);
            var baseOut3 = IOHelper.CreateBaseMock(IOSide.Output, parent3);

            baseOut1.Connect(baseIn2);
            baseOut2.Connect(baseIn3);

            //when
            var exception = Assert.Throws<CycleException>(() => baseOut3.Connect(baseIn1));
            //then
        }

        [Test]
        public void DisconnectOk()
        {
            //given
            var baseInOut1 = IOHelper.CreateBaseMock(IOSide.Input);
            var baseInOut2 = IOHelper.CreateBaseMock(IOSide.Output);
            baseInOut1.Connect(baseInOut2);
            //when
            baseInOut1.Disconnect();
            //then
            Assert.IsNull(baseInOut1.Connected);
            Assert.IsNull(baseInOut2.Connected);
        }

        [Test]
        public void DisconnectNotConnected()
        {
            var baseInOut1 = IOHelper.CreateBaseMock(IOSide.Input);
            baseInOut1.Disconnect();
        }
        
        [Test]
        public void DisconnectOkNotify()
        {
            //given
            var baseInOut1 = IOHelper.CreateBaseMock(IOSide.Input);
            var baseInOut2 = IOHelper.CreateBaseMock(IOSide.Output);
            baseInOut1.Connect(baseInOut2);

            var sub12 = Substitute.For<ISubscribeIO>();
            var sub1 = Substitute.For<ISubscribeIO>();
            baseInOut1.Subscribe(sub1);
            baseInOut1.Subscribe(sub12);
            var sub2 = Substitute.For<ISubscribeIO>();
            baseInOut2.Subscribe(sub2);
            baseInOut2.Subscribe(sub12);
            //when
            baseInOut1.Disconnect();
            //then
            Assert.IsNull(baseInOut1.Connected);
            Assert.IsNull(baseInOut2.Connected);

            sub1.Received().DisconnectNotify(baseInOut1);
            sub1.DidNotReceiveWithAnyArgs().ConnectNotify(default);

            sub2.Received().DisconnectNotify(baseInOut2);
            sub2.DidNotReceiveWithAnyArgs().ConnectNotify(default);

            sub12.Received().DisconnectNotify(baseInOut1);
            sub12.Received().DisconnectNotify(baseInOut2);
            sub12.DidNotReceiveWithAnyArgs().ConnectNotify(default);
        }

        [Test]
        public void DeleteTest()
        {
            var baseInOut1 = IOHelper.CreateBaseMock(IOSide.Input);
            var baseInOut2 = IOHelper.CreateBaseMock(IOSide.Output);
            baseInOut1.Connect(baseInOut2);

            baseInOut1.Delete();

            Assert.IsNull(baseInOut1.Connected);
            Assert.True(baseInOut1.IsDeleted);

            Assert.IsNull(baseInOut2.Connected);
            Assert.False(baseInOut2.IsDeleted);
        }
    }
}

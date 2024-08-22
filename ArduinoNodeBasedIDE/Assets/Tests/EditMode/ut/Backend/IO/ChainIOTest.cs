using Backend.IO;
using Backend.Node;
using Backend.Type;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.IO
{
    [TestFixture]
    [TestOf(typeof(ChainIO))]
    [Category("IO")]
    public class ChainIOTest
    {
        private ChainIO _chainIO1;
        private ChainIO _chainIO2;
        private TypeIO _typeIOMock1;
        private TypeIO _typeIOMock2;
        private IType _singleType;

        [SetUp]
        public void Init()
        {
            _typeIOMock1 = IOHelper.CreateTypeIO();
            _typeIOMock2 = IOHelper.CreateTypeIO();
            _singleType = MockHelper.CreateType();
        }

        private void PrepareNoResizeSetup()
        {
            _chainIO1 = new ChainIO(Substitute.ForPartsOf<BaseNode>(), IOSide.Input, false, false);
            _chainIO2 = new ChainIO(Substitute.ForPartsOf<BaseNode>(), IOSide.Input, false, false);
            _chainIO1.AppendChain(_chainIO2);
        }

        private void PrepareNoResizeSingleTypeSetup()
        {
            _chainIO1 = new ChainIO(Substitute.ForPartsOf<BaseNode>(), IOSide.Input, false, false, _singleType);
            _chainIO2 = new ChainIO(Substitute.ForPartsOf<BaseNode>(), IOSide.Input, false, false, _singleType);
            _chainIO1.AppendChain(_chainIO2);
        }

        private void PrepareResizeSetup()
        {
            _chainIO1 = new ChainIO(Substitute.ForPartsOf<BaseNode>(), IOSide.Input, true, false);
            _chainIO2 = new ChainIO(Substitute.ForPartsOf<BaseNode>(), IOSide.Input, true, false);
            _chainIO1.AppendChain(_chainIO2);
        }

        private void PrepareResizeSingleTypeSetup()
        {
            _chainIO1 = new ChainIO(Substitute.ForPartsOf<BaseNode>(), IOSide.Input, true, false, _singleType);
            _chainIO2 = new ChainIO(Substitute.ForPartsOf<BaseNode>(), IOSide.Input, true, false, _singleType);
            _chainIO1.AppendChain(_chainIO2);
        }

        [Test]
        public void NoResizeFirstNodeConnectAndDisconnect()
        {
            PrepareNoResizeSetup();
            IOHelper.ConnectAuto(_chainIO1, _typeIOMock1);

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(_typeIOMock1.MyType, _chainIO1.MyType);
            Assert.AreSame(_typeIOMock1.MyType, _chainIO2.MyType);

            _chainIO1.Disconnect();

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(null, _chainIO1.MyType);
            Assert.AreSame(null, _chainIO2.MyType);
        }

        [Test]
        public void NoResizeSecondNodeConnectAndDisconnect()
        {
            PrepareNoResizeSetup();
            IOHelper.ConnectAuto(_chainIO2, _typeIOMock1);

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(null, _chainIO1.MyType);
            Assert.AreSame(_typeIOMock1.MyType, _chainIO2.MyType);

            _chainIO2.Disconnect();

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(null, _chainIO1.MyType);
            Assert.AreSame(null, _chainIO2.MyType);
        }

        [Test]
        public void NoResizeSecondNodeConnectAndThenFirst()
        {
            PrepareNoResizeSetup();
            IOHelper.ConnectAuto(_chainIO2, _typeIOMock1);

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(null, _chainIO1.MyType);
            Assert.AreSame(_typeIOMock1.MyType, _chainIO2.MyType);

            IOHelper.AllowConnect(_typeIOMock1, _typeIOMock2.MyType);
            IOHelper.ConnectAuto(_chainIO1, _typeIOMock2);

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(_typeIOMock2.MyType, _chainIO1.MyType);
            Assert.AreSame(_typeIOMock2.MyType, _chainIO2.MyType);
        }

        [Test]
        public void NoResizeSingleTypeFirstNodeConnectAndDisconnect()
        {
            PrepareNoResizeSingleTypeSetup();
            IOHelper.AllowConnect(_typeIOMock1, _singleType);
            _chainIO1.Connect(_typeIOMock1);

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(_singleType, _chainIO1.MyType);
            Assert.AreSame(_singleType, _chainIO2.MyType);

            _chainIO1.Disconnect();

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(_singleType, _chainIO1.MyType);
            Assert.AreSame(_singleType, _chainIO2.MyType);
        }

        [Test]
        public void NoResizeSingleTypeSecondNodeConnectAndDisconnect()
        {
            PrepareNoResizeSingleTypeSetup();
            IOHelper.AllowConnect(_typeIOMock1, _singleType);
            _chainIO2.Connect(_typeIOMock1);

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(_singleType, _chainIO1.MyType);
            Assert.AreSame(_singleType, _chainIO2.MyType);

            _chainIO2.Disconnect();

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(_singleType, _chainIO1.MyType);
            Assert.AreSame(_singleType, _chainIO2.MyType);
        }

        [Test]
        public void NoResizeSingleTypeSecondNodeConnectAndThenFirst()
        {
            PrepareNoResizeSingleTypeSetup();
            IOHelper.AllowConnect(_typeIOMock1, _singleType);
            _chainIO2.Connect(_typeIOMock1);

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(_singleType, _chainIO1.MyType);
            Assert.AreSame(_singleType, _chainIO2.MyType);

            IOHelper.AllowConnect(_typeIOMock2, _singleType);
            IOHelper.ConnectAuto(_chainIO1, _typeIOMock2);

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(_singleType, _chainIO1.MyType);
            Assert.AreSame(_singleType, _chainIO2.MyType);
        }

        [Test]
        public void ResizeFirstNodeConnectAndDisconnect()
        {
            PrepareResizeSetup();
            IOHelper.ConnectAuto(_chainIO1, _typeIOMock1);

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(_typeIOMock1.MyType, _chainIO1.MyType);
            Assert.AreSame(_typeIOMock1.MyType, _chainIO2.MyType);

            _chainIO1.Disconnect();

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(null, _chainIO1.MyType);
            Assert.AreSame(null, _chainIO2.MyType);
        }

        [Test]
        public void ResizeSecondNodeConnectAndDisconnect()
        {
            PrepareResizeSetup();
            IOHelper.ConnectAuto(_chainIO2, _typeIOMock1);

            Assert.AreEqual(3, _chainIO1.ToList().Count);
            Assert.AreSame(null, _chainIO1.MyType);
            Assert.AreSame(_typeIOMock1.MyType, _chainIO2.MyType);
            Assert.AreSame(_typeIOMock1.MyType, _chainIO2.Next.MyType);
            Assert.True(_chainIO2.Next.IsOptional);

            _chainIO2.Disconnect();

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(null, _chainIO1.MyType);
            Assert.AreSame(null, _chainIO2.MyType);
        }

        [Test]
        public void ResizeSecondNodeConnectAndThenFirst()
        {
            PrepareResizeSetup();
            IOHelper.ConnectAuto(_chainIO2, _typeIOMock1);

            Assert.AreEqual(3, _chainIO1.ToList().Count);
            Assert.AreSame(null, _chainIO1.MyType);
            Assert.AreSame(_typeIOMock1.MyType, _chainIO2.MyType);
            Assert.AreSame(_typeIOMock1.MyType, _chainIO2.Next.MyType);
            Assert.True(_chainIO2.Next.IsOptional);

            IOHelper.AllowConnect(_typeIOMock1, _typeIOMock2.MyType);
            IOHelper.ConnectAuto(_chainIO1, _typeIOMock2);

            Assert.AreEqual(3, _chainIO1.ToList().Count);
            Assert.AreSame(_typeIOMock2.MyType, _chainIO1.MyType);
            Assert.AreSame(_typeIOMock2.MyType, _chainIO2.MyType);
            Assert.AreSame(_typeIOMock2.MyType, _chainIO2.Next.MyType);
        }

        [Test]
        public void ResizeSingleTypeFirstNodeConnectAndDisconnect()
        {
            PrepareResizeSingleTypeSetup();
            IOHelper.AllowConnect(_typeIOMock1, _singleType);
            _chainIO1.Connect(_typeIOMock1);

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(_singleType, _chainIO1.MyType);
            Assert.AreSame(_singleType, _chainIO2.MyType);

            _chainIO1.Disconnect();

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(_singleType, _chainIO1.MyType);
            Assert.AreSame(_singleType, _chainIO2.MyType);
        }

        [Test]
        public void ResizeSingleTypeSecondNodeConnectAndDisconnect()
        {
            PrepareResizeSingleTypeSetup();
            IOHelper.AllowConnect(_typeIOMock1, _singleType);
            _chainIO2.Connect(_typeIOMock1);

            Assert.AreEqual(3, _chainIO1.ToList().Count);
            Assert.AreSame(_singleType, _chainIO1.MyType);
            Assert.AreSame(_singleType, _chainIO2.MyType);
            Assert.AreSame(_singleType, _chainIO2.Next.MyType);

            _chainIO2.Disconnect();

            Assert.AreEqual(2, _chainIO1.ToList().Count);
            Assert.AreSame(_singleType, _chainIO1.MyType);
            Assert.AreSame(_singleType, _chainIO2.MyType);
        }

        [Test]
        public void ResizeSingleTypeSecondNodeConnectAndThenFirst()
        {
            PrepareResizeSingleTypeSetup();
            IOHelper.AllowConnect(_typeIOMock1, _singleType);
            _chainIO2.Connect(_typeIOMock1);

            Assert.AreEqual(3, _chainIO1.ToList().Count);
            Assert.AreSame(_singleType, _chainIO1.MyType);
            Assert.AreSame(_singleType, _chainIO2.MyType);
            Assert.AreSame(_singleType, _chainIO2.Next.MyType);

            IOHelper.AllowConnect(_typeIOMock2, _singleType);
            IOHelper.ConnectAuto(_chainIO1, _typeIOMock2);

            Assert.AreEqual(3, _chainIO1.ToList().Count);
            Assert.AreSame(_singleType, _chainIO1.MyType);
            Assert.AreSame(_singleType, _chainIO2.MyType);
            Assert.AreSame(_singleType, _chainIO2.Next.MyType);
        }
    }
}

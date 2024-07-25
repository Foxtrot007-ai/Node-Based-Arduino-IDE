using System.Collections.Generic;
using Backend.API;
using Backend.Connection;
using Backend.Connection.MyType;
using Backend.Node;
using Backend.Node.BuildIn;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Mocks.Connection;

namespace Tests.EditMode.ut.Backend.Node.BuildIn
{
    [TestFixture]
    [TestOf(nameof(ReturnNode))]
    [Category("Node")]
    public class ReturnNodeTest : BaseNodeTestSetup
    {
        private ReturnNode _sut;
        private AutoInOutMock _autoIn;

        [SetUp]
        public override void Init()
        {
            base.Init();
            _autoIn = CreateAutoInOutMock();
            _sut = new ReturnNode(_buildInTemplateMock);

            PrepareSetup();
        }

        private void PrepareSetup()
        {
            SetInOutMock<BaseNode>(_sut, "_prevNode", _prevMock);
            SetInOutMock<ReturnNode>(_sut, "_returnIn", _autoIn);
            SetInputsList(_sut, new List<IConnection> { _prevMock, _autoIn });

            _prevMock.MakeConnect();
        }

        [Test]
        public void ConstructorTest()
        {
            var sut = new ReturnNode(_buildInTemplateMock);

            Assert.AreEqual(2, _sut.InputsList.Count);
            Assert.IsInstanceOf<FlowInOut>(sut.InputsList[0]);
            Assert.IsInstanceOf<AutoInOut>(sut.InputsList[1]);

            Assert.AreEqual(0, sut.OutputsList.Count);
        }

        [Test]
        public void ToCodeAutoNotConnectedTest()
        {
            _autoIn.MakeDisconnect();
            _sut.ToCode(_codeManagerMock);

            _codeManagerMock.Received().AddLine("return ;");
        }

        [Test]
        public void ToCodeAutoConnectedTest()
        {
            _autoIn.ToCodeParamReturn(_codeManagerMock, "test");
            _autoIn.MakeConnect();

            _sut.ToCode(_codeManagerMock);

            _codeManagerMock.Received().AddLine("return test;");
        }
    }
}

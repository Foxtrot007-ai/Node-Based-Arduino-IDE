using System.Collections.Generic;
using Backend.API;
using Backend.Connection;
using Backend.Node;
using Backend.Node.BuildIn;
using NUnit.Framework;

namespace Tests.EditMode.ut.Backend.Node.BuildIn
{
    [TestFixture]
    [TestOf(nameof(StartNode))]
    [Category("Node")]
    public class StartNodeTest : BaseNodeTestSetup
    {
        private StartNode _sut;

        [SetUp]
        public override void Init()
        {
            base.Init();
            _sut = new StartNode(_buildInTemplateMock);

            PrepareSetup();
        }

        private void PrepareSetup()
        {
            SetInOutMock<BaseNode>(_sut, "_nextNode", _nextMock);
            SetOutputsList(_sut, new List<IConnection> { _nextMock });
            _nextMock.MakeConnect();
        }

        [Test]
        public void ConstructorTest()
        {
            var sut = new StartNode(_buildInTemplateMock);

            Assert.AreEqual(0, sut.InputsList.Count);

            Assert.AreEqual(1, sut.OutputsList.Count);
            Assert.IsInstanceOf<FlowInOut>(sut.OutputsList[0]);
        }

        [Test]
        public void ToCodeTest()
        {
            _sut.ToCode(_codeManagerMock);

            _nextMock.ExpectToCode(_codeManagerMock);
        }
    }
}

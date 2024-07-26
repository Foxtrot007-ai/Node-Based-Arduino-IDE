using System.Collections.Generic;
using Backend.API;
using Backend.Connection;
using Backend.Connection.MyType;
using Backend.Node.BuildIn;
using NSubstitute;
using NUnit.Framework;

namespace Tests.EditMode.ut.Backend.Node.BuildIn
{
    [TestFixture]
    [TestOf(nameof(ReturnNode))]
    [Category("Node")]
    public class ReturnNodeTest : BaseNodeTestSetup
    {
        private ReturnNode _sut;

        [SetUp]
        public void Init()
        {
            _sut = new ReturnNode(_buildInTemplateMock);

            PrepareBaseSetup(_sut);

            SetInOutMock<ReturnNode>("_returnIn", _auto1);
            SetInputsList(_prevMock, _auto1);
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
            _auto1.MakeDisconnect();
            _sut.ToCode(_codeManagerMock);

            _codeManagerMock.Received().AddLine("return ;");
        }

        [Test]
        public void ToCodeAutoConnectedTest()
        {
            _auto1.ToCodeParamReturn(_codeManagerMock, "test");
            _auto1.MakeConnect();

            _sut.ToCode(_codeManagerMock);

            _codeManagerMock.Received().AddLine("return test;");
        }
    }
}

using System.Collections.Generic;
using Backend.API;
using Backend.Connection;
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
        public void Init()
        {
            _sut = new StartNode(_buildInTemplateMock);

            PrepareBaseSetup(_sut);

            SetOutputsList(_nextMock);
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

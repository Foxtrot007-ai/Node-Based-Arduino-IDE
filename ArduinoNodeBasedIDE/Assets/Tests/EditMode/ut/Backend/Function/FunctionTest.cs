using Backend;
using Backend.API;
using Backend.Node.BuildIn;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;
using MyFunction = Backend.Function.Function;

namespace Tests.EditMode.ut.Backend.Function
{
    [TestFixture]
    [TestOf(nameof(MyFunction))]
    [Category("Function")]
    public class FunctionTest
    {
        private CodeManager _codeManagerMock;
        private MyFunction _sut;
        private StartNode _startNodeMock;
        private IBackendManager _backendManager;

        [SetUp]
        public void Init()
        {
            _startNodeMock = Substitute.For<StartNode>();
            _codeManagerMock = Substitute.For<CodeManager>();
            _backendManager = Substitute.For<IBackendManager>();

            _sut = new MyFunction(_backendManager, "test", new PathName("TEST-1"));
            MockHelper.SetFieldValue<MyFunction, StartNode>(_sut, "_startNode", _startNodeMock);
        }

        [Test]
        public void ToCodeTest()
        {
            CodeManager callCodeManager = null;
            _startNodeMock
                .When(x =>
                          x.ToCode(Arg.Any<CodeManager>()))
                .Do(info => callCodeManager = info.ArgAt<CodeManager>(0));

            _sut.ToCode(_codeManagerMock);

            Assert.AreNotSame(callCodeManager, _codeManagerMock);
            _codeManagerMock.Received().AddLine("void test()");
            _codeManagerMock.Received().AddLines(callCodeManager.CodeLines);
        }
    }
}

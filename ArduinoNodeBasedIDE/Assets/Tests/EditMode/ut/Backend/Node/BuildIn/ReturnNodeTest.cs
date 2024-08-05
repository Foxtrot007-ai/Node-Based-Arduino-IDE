using Backend.Connection;
using Backend.IO;
using Backend.Node.BuildIn;
using Backend.Type;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Node.BuildIn
{
    [TestFixture]
    [TestOf(nameof(ReturnNode))]
    [Category("Node")]
    public class ReturnNodeTest : BaseNodeTestSetup
    {
        private ReturnNode _sut;
        private global::Backend.MyFunction.Function _functionMock;
        private IType _outputTypeMock;
        
        [SetUp]
        public void Init()
        {
            _outputTypeMock = MockHelper.CreateType();
            _functionMock = Substitute.For<global::Backend.MyFunction.Function>();
            _outputTypeMock.EType.Returns(EType.Int);
            _functionMock.OutputType.Returns(_outputTypeMock);
            
            _sut = new ReturnNode(_buildInTemplateMock, _functionMock);

            PrepareBaseSetup(_sut);
            SetInOutMock<ReturnNode>("_returnIn", _type1);
        }

        private void PrepareVoidSetup()
        {
            SetInputsList(_prevMock);
            SetInOutMock<ReturnNode>("_returnIn", null);
            _outputTypeMock.EType.Returns(EType.Void);
        }

        private void PrepareNonVoidSetup()
        {
            SetInputsList(_prevMock, _type1);
            _outputTypeMock.EType.Returns(EType.Int);
        }

        [Test]
        public void ConstructorNoVoidTest()
        {
            _outputTypeMock.EType.Returns(EType.Int);
            var sut = new ReturnNode(_buildInTemplateMock, _functionMock);

            Assert.AreEqual(2, sut.InputsList.Count);
            Assert.IsInstanceOf<FlowIO>(sut.InputsList[0]);
            Assert.IsInstanceOf<TypeIO>(sut.InputsList[1]);

            Assert.AreEqual(0, sut.OutputsList.Count);
            _functionMock.Received().AddReturnRef(sut);
        }

        [Test]
        public void ConstructorVoidTest()
        {
            _outputTypeMock.EType.Returns(EType.Void);
            var sut = new ReturnNode(_buildInTemplateMock, _functionMock);

            Assert.AreEqual(1, sut.InputsList.Count);
            Assert.IsInstanceOf<FlowIO>(sut.InputsList[0]);

            Assert.AreEqual(0, sut.OutputsList.Count);
            _functionMock.Received().AddReturnRef(sut);
        }
        
        [Test]
        public void ToCodeVoidTest()
        {
            PrepareVoidSetup();
            _sut.ToCode(_codeManagerMock);

            _codeManagerMock.Received().AddLine("return ;");
        }

        [Test]
        public void ToCodeNonVoidTest()
        {
            PrepareNonVoidSetup();
            _type1.ToCodeParamReturn(_codeManagerMock, "test");

            _sut.ToCode(_codeManagerMock);

            _codeManagerMock.Received().AddLine("return test;");
        }

        [Test]
        public void ChangeTypeToVoid()
        {
            PrepareNonVoidSetup();
            var newType = MockHelper.CreateType(EType.Void);
            
            _sut.ChangeInputType(newType);
            
            Assert.AreEqual(1, _sut.InputsList.Count);
            _type1.Received().Delete();
        }

        [Test]
        public void ChangeVoidToType()
        {
            PrepareVoidSetup();
            var newType = MockHelper.CreateType(EType.Int);
            
            _sut.ChangeInputType(newType);
            
            Assert.AreEqual(2, _sut.InputsList.Count);
            ExpectHelper.TypeEqual(newType, _sut.InputsList[1]);
        }

        [Test]
        public void ChangeTypeToType()
        {
            PrepareNonVoidSetup();
            var newType = MockHelper.CreateType(EType.Long);
            
            _sut.ChangeInputType(newType);
            
            Assert.AreEqual(2, _sut.InputsList.Count);
            _type1.Received().ChangeType(newType);
        }

        [Test]
        public void DeleteTest()
        {
            _sut.Delete();
            
            _functionMock.Received().RemoveReturnRef(_sut);
        }
    }
}

using System.Collections.Generic;
using Backend;
using Backend.API;
using Backend.API.DTO;
using Backend.Exceptions;
using Backend.Function;
using Backend.Node;
using Backend.Node.BuildIn;
using Backend.Type;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Function
{
    [TestFixture]
    [TestOf(nameof(UserFunction))]
    [Category("Function")]
    public class UserFunctionTest
    {
        private StartNode _startNodeMock;
        private UserFunctionManager _userFunctionManagerMock;
        private ParamsManager _paramsManagerMock;
        private FunctionManageDto _dto;
        private UserFunctionNode _userNode1;
        private UserFunctionNode _userNode2;
        private CodeManager _codeManagerMock;
        
        private UserFunction _sut;

        [SetUp]
        public void Init()
        {
            _userFunctionManagerMock = Substitute.For<UserFunctionManager>();
            _paramsManagerMock = Substitute.For<ParamsManager>();
            _startNodeMock = Substitute.For<StartNode>();
            _userNode1 = Substitute.For<UserFunctionNode>();
            _userNode2 = Substitute.For<UserFunctionNode>();
            _codeManagerMock = Substitute.For<CodeManager>();
            _dto = DtoHelper.CreateFunctionManage();
            
            _sut = new UserFunction(_userFunctionManagerMock, _dto);
            MockHelper.SetField<global::Backend.Function.Function, StartNode>(_sut, "_startNode", _startNodeMock);
            MockHelper.SetField<UserFunction, ParamsManager>(_sut, "_paramsManager", _paramsManagerMock);
            _sut.AddRef(_userNode1);
            _sut.AddRef(_userNode2);
        }

        [Test]
        public void ConstructorTest()
        {
            var sut = new UserFunction(_userFunctionManagerMock, DtoHelper.CreateFunctionManage());
            
            _userFunctionManagerMock.Received().AddRef(sut);
        }

        [Test]
        public void ChangeInvalidDtoTest()
        {
            var newDto = DtoHelper.CreateFunctionManage("test2", EType.Bool);
            _userFunctionManagerMock
                .When(x => x.Validate(newDto))
                .Do(x => throw new InvalidFunctionManageDto());

            Assert.Throws<InvalidFunctionManageDto>(() => _sut.Change(newDto));
            Assert.AreEqual(_dto.FunctionName, _sut.Name);
            Assert.AreEqual(_dto.OutputType, _sut.OutputType);
            
            _userNode1.DidNotReceiveWithAnyArgs().ChangeOutputType(default);
            _userNode2.DidNotReceiveWithAnyArgs().ChangeOutputType(default);
        }

        [Test]
        public void ChangeSameTypeTest()
        {
            var newDto = DtoHelper.CreateFunctionManage("test2");
            
            _sut.Change(newDto);
            
            Assert.AreEqual(newDto.FunctionName, _sut.Name);
            Assert.AreEqual(_dto.OutputType, _sut.OutputType);
            
            _userNode1.DidNotReceiveWithAnyArgs().ChangeOutputType(default);
            _userNode2.DidNotReceiveWithAnyArgs().ChangeOutputType(default);
        }

        [Test]
        public void ChangeTypeTest()
        {
            var newDto = DtoHelper.CreateFunctionManage("test2", EType.String);

            _sut.Change(newDto);
            
            Assert.AreEqual(newDto.FunctionName, _sut.Name);
            Assert.AreEqual(newDto.OutputType, _sut.OutputType);
            
            _userNode1.Received().ChangeOutputType(newDto.OutputType);
            _userNode2.Received().ChangeOutputType(newDto.OutputType);
        }

        [Test]
        public void AddInOutTest()
        {
            var variable = MockHelper.CreateVariable();
            
            _sut.AddInOut(variable);
            
            _userNode1.Received().AddParam(variable);
            _userNode2.Received().AddParam(variable);
        }

        [Test]
        public void DeleteInOutTest()
        {
            _sut.DeleteInOut(1);
            
            _userNode1.Received().RemoveParam(1);
            _userNode2.Received().RemoveParam(1);
        }
        
        [Test]
        public void DeleteTest()
        {
            _sut.Delete();
            
            _userFunctionManagerMock.Received().DeleteRef(_sut);
            _userNode1.Received().Delete();
        }

        [Test]
        public void ToCodeTest()
        {
            var variable1 = MockHelper.CreateVariable();
            var variable2 = MockHelper.CreateVariable();
            _paramsManagerMock.Variables.Returns(new List<IVariable> { variable1, variable2 });

            variable1.Type.ToCode().Returns("int");
            variable1.Name.Returns("param1");
            variable2.Type.ToCode().Returns("string");
            variable2.Name.Returns("param2");
            ((IType)_dto.OutputType).ToCode().Returns("int");
            
            CodeManager callCodeManager = null;
            _startNodeMock
                .When(x =>
                          x.ToCode(Arg.Any<CodeManager>()))
                .Do(info => callCodeManager = info.ArgAt<CodeManager>(0));
            _sut.ToCode(_codeManagerMock);
            
            Assert.AreEqual(2, callCodeManager.Variables.Count);
            Assert.AreEqual(CodeManager.VariableStatus.Param, callCodeManager.Variables[variable1]);
            Assert.AreEqual(CodeManager.VariableStatus.Param, callCodeManager.Variables[variable2]);
            
            _codeManagerMock.Received().AddLine("int test(int param1, string param2)");
            _codeManagerMock.Received().AddLines(callCodeManager.CodeLines);
        }
    }
}

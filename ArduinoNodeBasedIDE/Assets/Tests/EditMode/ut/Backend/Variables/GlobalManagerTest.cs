using System.Collections.Generic;
using Backend.API;
using Backend.Exceptions;
using Backend.Function;
using Backend.Variables;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Variables
{
    [TestFixture]
    [TestOf(nameof(GlobalVariablesManager))]
    [Category("Function")]
    [Category("Manager")]
    [Category("Variable")]
    public class GlobalManagerTest
    {
        private IBackendManager _backendManagerMock;
        private GlobalVariablesManager _sut;
        private global::Backend.Function.Function _startMock;
        private global::Backend.Function.Function _loopMock;
        private UserFunction _userFunctionMock;
        
        [SetUp]
        public void Init()
        {
            _startMock = Substitute.For<global::Backend.Function.Function>();
            _loopMock = Substitute.For<global::Backend.Function.Function>();
            _userFunctionMock = Substitute.For<UserFunction>();
            
            _backendManagerMock = Substitute.For<IBackendManager>();

            _backendManagerMock.Start.Returns(_startMock);
            _backendManagerMock.Loop.Returns(_loopMock);
            _backendManagerMock.Functions.Functions.Returns(new List<IUserFunction> { _userFunctionMock });
            _sut = new GlobalVariablesManager(_backendManagerMock);
        }

        [Test]
        public void NonDuplicateVariableName()
        {
            var dto = DtoHelper.CreateVariableManage();
            var name = dto.VariableName;
            _startMock.IsVariableLocalDuplicate(name).Returns(false);
            _loopMock.IsVariableLocalDuplicate(name).Returns(false);
            _userFunctionMock.IsVariableLocalDuplicate(name).Returns(false);

            _sut.AddVariable(dto);
        }
        
        [Test]
        public void DuplicateVariableNameSelf()
        {
            var dto = DtoHelper.CreateVariableManage();
            var name = dto.VariableName;
            _startMock.IsVariableLocalDuplicate(name).Returns(false);
            _loopMock.IsVariableLocalDuplicate(name).Returns(false);
            _userFunctionMock.IsVariableLocalDuplicate(name).Returns(false);
            
            _sut.AddVariable(dto);

            Assert.Throws<InvalidVariableManageDto>(() => _sut.AddVariable(dto));
        }
        
        [Test]
        public void DuplicateVariableNameStart()
        {
            var dto = DtoHelper.CreateVariableManage();
            var name = dto.VariableName;
            _startMock.IsVariableLocalDuplicate(name).Returns(true);
            _loopMock.IsVariableLocalDuplicate(name).Returns(false);
            _userFunctionMock.IsVariableLocalDuplicate(name).Returns(false);
            
            Assert.Throws<InvalidVariableManageDto>(() => _sut.AddVariable(dto));
        }
        
        [Test]
        public void DuplicateVariableNameLoop()
        {
            var dto = DtoHelper.CreateVariableManage();
            var name = dto.VariableName;
            _startMock.IsVariableLocalDuplicate(name).Returns(false);
            _loopMock.IsVariableLocalDuplicate(name).Returns(true);
            _userFunctionMock.IsVariableLocalDuplicate(name).Returns(false);
            
            Assert.Throws<InvalidVariableManageDto>(() => _sut.AddVariable(dto));
        }
        
        [Test]
        public void DuplicateVariableNameUserFunction()
        {
            var dto = DtoHelper.CreateVariableManage();
            var name = dto.VariableName;
            _startMock.IsVariableLocalDuplicate(name).Returns(false);
            _loopMock.IsVariableLocalDuplicate(name).Returns(false);
            _userFunctionMock.IsVariableLocalDuplicate(name).Returns(true);
            
            Assert.Throws<InvalidVariableManageDto>(() => _sut.AddVariable(dto));
        }
    }
}

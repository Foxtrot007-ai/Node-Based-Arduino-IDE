using System.Collections.Generic;
using Backend;
using Backend.API;
using Backend.Exceptions;
using Backend.MyFunction;
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
        private global::Backend.MyFunction.Function _startMock;
        private global::Backend.MyFunction.Function _loopMock;
        private UserFunction _userFunctionMock;

        [SetUp]
        public void Init()
        {
            _startMock = Substitute.For<global::Backend.MyFunction.Function>();
            _loopMock = Substitute.For<global::Backend.MyFunction.Function>();
            _userFunctionMock = Substitute.For<UserFunction>();

            _backendManagerMock = Substitute.For<IBackendManager>();

            _backendManagerMock.Setup.Returns(_startMock);
            _backendManagerMock.Loop.Returns(_loopMock);
            _backendManagerMock.Functions.Functions.Returns(new List<IUserFunction> { _userFunctionMock });
            _sut = new GlobalVariablesManager(_backendManagerMock, new PathName("TEST-1"));
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


        [Test]
        public void AddVariableTest()
        {
            var variable1 = _sut.AddVariable(DtoHelper.CreateVariableManage("test1"));
            var variable2 = _sut.AddVariable(DtoHelper.CreateVariableManage("test2"));

            Assert.AreEqual(2, _sut.Variables.Count);
            Assert.AreSame(variable1, _sut.Variables[0]);
            Assert.AreSame(variable2, _sut.Variables[1]);
            Assert.AreEqual("TEST-1/GLOBAL_VAR-1", ((Variable)variable1).PathName.ToString());
            Assert.AreEqual("TEST-1/GLOBAL_VAR-2", ((Variable)variable2).PathName.ToString());
        }
    }
}

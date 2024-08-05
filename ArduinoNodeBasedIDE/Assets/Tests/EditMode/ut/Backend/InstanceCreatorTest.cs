using System;
using Backend;
using Backend.API;
using Backend.MyFunction;
using Backend.Variables;
using NSubstitute;
using NUnit.Framework;

namespace Tests.EditMode.ut.Backend
{
    [TestFixture]
    [TestOf(nameof(InstanceCreator))]
    [Category("Instance")]
    public class InstanceCreatorTest
    {
        private IBackendManager _backendManagerMock;
        private INode _nodeMock;
        private IVariable _variableMock;
        private INode _getVariable;
        private INode _setVariable;
        private VariablesManager _variablesManagerMock;
        private UserFunctionManager _functionManagerMock;
        private UserFunction _userFunctionMock;
        private IInstanceCreator _sut;

        [SetUp]
        public void Init()
        {
            _backendManagerMock = Substitute.For<IBackendManager>();
            _nodeMock = Substitute.For<INode>();
            _setVariable = Substitute.For<INode>();
            _getVariable = Substitute.For<INode>();
            _variableMock = Substitute.For<IVariable>();
            _variableMock.CreateGetNode().Returns(_getVariable);
            _variableMock.CreateSetNode().Returns(_setVariable);
            _variablesManagerMock = Substitute.For<VariablesManager>();
            _functionManagerMock = Substitute.For<UserFunctionManager>();
            _userFunctionMock = Substitute.For<UserFunction>();

            _sut = new InstanceCreator(_backendManagerMock);
        }

        private void MockGetVariableByPn(string pn)
        {
            _variablesManagerMock
                .GetVariableByPn(Arg.Is<PathName>(x => x.ToString() == pn))
                .Returns(_variableMock);
        }
        
        private void MockGetFunctionByPn(string pn)
        {
            _functionManagerMock
                .GetFunctionByPn(Arg.Is<PathName>(x => x.ToString() == pn))
                .Returns(_userFunctionMock);
        }
        
        [Test]
        public void NotRootPathTest()
        {
            Assert.Throws<ArgumentException>(() => _sut.CreateNodeInstance("test"));
        }
        
        [Test]
        public void TemplateTest()
        {
            var path = "ROOT-1/TEMPLATE-1";
            var template = Substitute.For<ITemplate>();
            template.CreateNodeInstance(null).Returns(_nodeMock);
            var templateManager = Substitute.For<TemplateManager>();
            templateManager.GetTemplateById(1).Returns(template);
            _backendManagerMock.Templates.Returns(templateManager);
            
            Assert.AreSame(_nodeMock, _sut.CreateNodeInstance(path, null));
        }

        [Test]
        public void GlobalVariableGetTest()
        {
            var path = "ROOT-1/GLOBAL_VAR-1/GET-1";
            MockGetVariableByPn("GLOBAL_VAR-1/GET-1");
            _backendManagerMock.GlobalVariables.Returns(_variablesManagerMock);

            Assert.AreSame(_getVariable, _sut.CreateNodeInstance(path));
        }
        
        [Test]
        public void GlobalVariableSetTest()
        {
            var path = "ROOT-1/GLOBAL_VAR-1/SET-1";
            MockGetVariableByPn("GLOBAL_VAR-1/SET-1");
            _backendManagerMock.GlobalVariables.Returns(_variablesManagerMock);

            Assert.AreSame(_setVariable, _sut.CreateNodeInstance(path));
        }

        [Test]
        public void SetupVariableGetTest()
        {
            var path = "ROOT-1/SETUP-1/LOCAL_VAR-1/GET-1";
            MockGetVariableByPn("LOCAL_VAR-1/GET-1");
            _backendManagerMock.Setup.Variables.Returns(_variablesManagerMock);
            
            Assert.AreSame(_getVariable, _sut.CreateNodeInstance(path));
        }
        
        [Test]
        public void SetupVariableSetTest()
        {
            var path = "ROOT-1/SETUP-1/LOCAL_VAR-1/SET-1";
            MockGetVariableByPn("LOCAL_VAR-1/SET-1");
            _backendManagerMock.Setup.Variables.Returns(_variablesManagerMock);

            Assert.AreSame(_setVariable, _sut.CreateNodeInstance(path));
        }
        
        [Test]
        public void LoopVariableGetTest()
        {
            var path = "ROOT-1/LOOP-1/LOCAL_VAR-1/GET-1";
            MockGetVariableByPn("LOCAL_VAR-1/GET-1");
            _backendManagerMock.Loop.Variables.Returns(_variablesManagerMock);
            
            Assert.AreSame(_getVariable, _sut.CreateNodeInstance(path));
        }
        
        [Test]
        public void LoopVariableSetTest()
        {
            var path = "ROOT-1/LOOP-1/LOCAL_VAR-1/SET-1";
            MockGetVariableByPn("LOCAL_VAR-1/SET-1");
            _backendManagerMock.Loop.Variables.Returns(_variablesManagerMock);

            Assert.AreSame(_setVariable, _sut.CreateNodeInstance(path));
        }
        
        [Test]
        public void UserFuncLocalVariableGetTest()
        {
            var path = "ROOT-1/USER_FUNCTION-1/LOCAL_VAR-1/GET-1";
            MockGetVariableByPn("LOCAL_VAR-1/GET-1");
            MockGetFunctionByPn("USER_FUNCTION-1/LOCAL_VAR-1/GET-1");
            _backendManagerMock.Functions.Returns(_functionManagerMock);
            _userFunctionMock.Variables.Returns(_variablesManagerMock);
            
            Assert.AreSame(_getVariable, _sut.CreateNodeInstance(path));
        }
        
        [Test]
        public void UserFuncLocalVariableSetTest()
        {
            var path = "ROOT-1/USER_FUNCTION-1/LOCAL_VAR-1/SET-1";
            MockGetVariableByPn("LOCAL_VAR-1/SET-1");
            MockGetFunctionByPn("USER_FUNCTION-1/LOCAL_VAR-1/SET-1");
            _backendManagerMock.Functions.Returns(_functionManagerMock);
            _userFunctionMock.Variables.Returns(_variablesManagerMock);

            Assert.AreSame(_setVariable, _sut.CreateNodeInstance(path));
        }
        
        [Test]
        public void UserFuncParamVariableGetTest()
        {
            var path = "ROOT-1/USER_FUNCTION-1/PARAM_VAR-1/GET-1";
            MockGetVariableByPn("PARAM_VAR-1/GET-1");
            MockGetFunctionByPn("USER_FUNCTION-1/PARAM_VAR-1/GET-1");
            _backendManagerMock.Functions.Returns(_functionManagerMock);
            _userFunctionMock.InputList.Returns(_variablesManagerMock);
            
            Assert.AreSame(_getVariable, _sut.CreateNodeInstance(path));
        }
        
        [Test]
        public void UserFuncParamVariableSetTest()
        {
            var path = "ROOT-1/USER_FUNCTION-1/PARAM_VAR-1/SET-1";
            MockGetVariableByPn("PARAM_VAR-1/SET-1");
            MockGetFunctionByPn("USER_FUNCTION-1/PARAM_VAR-1/SET-1");
            _backendManagerMock.Functions.Returns(_functionManagerMock);
            _userFunctionMock.InputList.Returns(_variablesManagerMock);

            Assert.AreSame(_setVariable, _sut.CreateNodeInstance(path));
        }
    }
}

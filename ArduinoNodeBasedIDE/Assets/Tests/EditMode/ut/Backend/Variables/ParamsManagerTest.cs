using Backend.Exceptions;
using Backend.Function;
using Backend.Variables;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Variables
{
    [TestFixture]
    [TestOf(nameof(ParamsManager))]
    [Category("Function")]
    [Category("Manager")]
    [Category("Variable")]
    public class ParamsManagerTest
    {
        private UserFunction _userFunctionMock;
        private ParamsManager _sut;

        [SetUp]
        public void Init()
        {
            _userFunctionMock = Substitute.For<UserFunction>();
            _sut = new ParamsManager(_userFunctionMock);
        }

        [Test]
        public void NonDuplicateVariableName()
        {
            var dto = DtoHelper.CreateVariableManage();
            _userFunctionMock.IsVariableDuplicate(dto.VariableName).Returns(false);

            _sut.AddVariable(dto);
        }

        [Test]
        public void DuplicateVariableName()
        {
            var dto = DtoHelper.CreateVariableManage();
            _userFunctionMock.IsVariableDuplicate(dto.VariableName).Returns(true);

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

            _userFunctionMock.Received().AddInOut(variable1);
            _userFunctionMock.Received().AddInOut(variable2);
        }

        [Test]
        public void DeleteRefTest()
        {
            var variable1 = _sut.AddVariable(DtoHelper.CreateVariableManage("test1"));
            var variable2 = _sut.AddVariable(DtoHelper.CreateVariableManage("test2"));

            _sut.DeleteRef(variable2);
            _sut.DeleteRef(variable1);

            _userFunctionMock.Received().DeleteInOut(1);
            _userFunctionMock.Received().DeleteInOut(0);
        }
    }
}

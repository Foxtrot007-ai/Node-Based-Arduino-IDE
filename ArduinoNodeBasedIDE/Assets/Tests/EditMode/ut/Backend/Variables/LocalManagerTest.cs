using Backend.Exceptions;
using Backend.Function;
using Backend.Variables;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Variables
{
    [TestFixture]
    [TestOf(nameof(LocalVariablesManager))]
    [Category("Function")]
    [Category("Manager")]
    [Category("Variable")]
    public class LocalManagerTest
    {
        private UserFunction _userFunctionMock;
        private LocalVariablesManager _sut;

        [SetUp]
        public void Init()
        {
            _userFunctionMock = Substitute.For<UserFunction>();
            _sut = new LocalVariablesManager(_userFunctionMock);
        }

        [Test]
        public void NoDuplicateVariableName()
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
    }
}

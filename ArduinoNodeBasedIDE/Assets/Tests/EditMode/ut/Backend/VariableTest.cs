using Backend;
using Backend.API.DTO;
using Backend.Exceptions;
using Backend.Node.BuildIn;
using Backend.Type;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;
using Tests.EditMode.ut.Backend.mocks;

namespace Tests.EditMode.ut.Backend
{
    [TestFixture]
    [TestOf(nameof(Variable))]
    [Category("Variable")]
    public class VariableTest
    {
        private VariablesManager _variablesManagerMock;
        private Variable _sut;
        private VariableNode _nodeMock;
        private VariableManageDto _baseDto;

        [SetUp]
        public void Init()
        {
            _baseDto = DtoHelper.CreateVariableManage();
            _variablesManagerMock = Substitute.For<VariablesManager>();
            _variablesManagerMock.IsDtoValid(_baseDto).Returns(true);
            _sut = new Variable(_variablesManagerMock, _baseDto);
            _nodeMock = Substitute.For<VariableNodeMock>(_sut);
            _sut.AddRef(_nodeMock);
        }

        private void ExpectEqualDto(VariableManageDto variableManageDto)
        {
            Assert.AreEqual(variableManageDto.Type, _sut.Type);
            Assert.AreEqual(variableManageDto.VariableName, _sut.Name);
        }

        [Test]
        public void ConstructorTest()
        {
            var dto = DtoHelper.CreateVariableManage();
            var newSut = new Variable(_variablesManagerMock, dto);
            _variablesManagerMock.Received().AddRef(newSut);
            ExpectEqualDto(dto);
        }

        [Test]
        public void ChangeVariableNullName()
        {
            var newDto = DtoHelper.CreateVariableManage(null);
            _variablesManagerMock.IsDtoValid(newDto).Returns(true);
            _sut.Change(newDto);

            ExpectEqualDto(_baseDto);
            _nodeMock.DidNotReceiveWithAnyArgs().ChangeType(default);
        }

        [Test]
        public void ChangeVariableSameName()
        {
            _sut.Change(_baseDto);

            ExpectEqualDto(_baseDto);
            _nodeMock.DidNotReceiveWithAnyArgs().ChangeType(default);
        }

        [Test]
        public void ChangeVariableNotValidName()
        {
            var newDto = DtoHelper.CreateVariableManage("test2");
            _variablesManagerMock.IsDtoValid(newDto).Returns(false);

            var exception = Assert.Throws<InvalidVariableManageDto>(() => _sut.Change(newDto));

            ExpectEqualDto(_baseDto);
            _nodeMock.DidNotReceiveWithAnyArgs().ChangeType(default);
        }

        [Test]
        public void ChangeVariableNameSameType()
        {
            var newDto = DtoHelper.CreateVariableManage("test2");
            _variablesManagerMock.IsDtoValid(newDto).Returns(true);

            _sut.Change(newDto);

            Assert.AreEqual(_baseDto.Type, _sut.Type);
            Assert.AreEqual(newDto.VariableName, _sut.Name);
            _nodeMock.DidNotReceiveWithAnyArgs().ChangeType(default);
        }

        [Test]
        public void ChangeVariableNameAndType()
        {
            var newDto = DtoHelper.CreateVariableManage(EType.String, "test2");
            _variablesManagerMock.IsDtoValid(newDto).Returns(true);

            _sut.Change(newDto);

            ExpectEqualDto(newDto);
            _nodeMock.Received().ChangeType((IType)newDto.Type);
        }

        [Test]
        public void ShouldDeleteRefVariables()
        {
            _sut.Delete();
            _nodeMock.Received().Delete();
            _variablesManagerMock.Received().DeleteRef(_sut);
        }
    }
}

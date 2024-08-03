using Backend;
using Backend.API.DTO;
using Backend.Exceptions;
using Backend.Json;
using Backend.Node.BuildIn;
using Backend.Type;
using Backend.Variables;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;
using Tests.EditMode.ut.Backend.mocks;

namespace Tests.EditMode.ut.Backend.Variables
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
            _sut = new Variable(_variablesManagerMock, _baseDto, new PathName("TEST-1"));
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
            var newSut = new Variable(_variablesManagerMock, dto, new PathName("TEST-1"));
            _variablesManagerMock.Received().AddRef(newSut);
            ExpectEqualDto(dto);
        }

        [Test]
        public void ConstructorJsonTest()
        {
            var json = new VariableJson
            {
                Name = "test",
                PathName = "TEST-1",
                Type = "string",
            };

            var newSut = new Variable(_variablesManagerMock, json);
            Assert.AreEqual("test", newSut.Name);
            Assert.AreEqual("TEST-1", newSut.Id);
            Assert.AreEqual(EType.String, newSut.Type.EType);
            _variablesManagerMock.Received().AddRef(newSut);
        }

        [Test]
        public void ChangeVariableNullName()
        {
            var newDto = DtoHelper.CreateVariableManage(null);
            Assert.Throws<InvalidVariableManageDto>(() => _sut.Change(newDto));

            ExpectEqualDto(_baseDto);
            _nodeMock.DidNotReceiveWithAnyArgs().ChangeType(default);
        }

        [Test]
        public void ChangeVariableVoidType()
        {
            var newDto = DtoHelper.CreateVariableManage(EType.Void, "test");
            Assert.Throws<InvalidVariableManageDto>(() => _sut.Change(newDto));

            ExpectEqualDto(_baseDto);
            _nodeMock.DidNotReceiveWithAnyArgs().ChangeType(default);
        }

        [Test]
        public void ChangeVariableSameNameAndType()
        {
            _variablesManagerMock.IsDuplicateName(_baseDto.VariableName).Returns(true);
            var newDto = DtoHelper.CreateVariableManage();

            _sut.Change(newDto);
            ExpectEqualDto(_baseDto);
            _nodeMock.DidNotReceiveWithAnyArgs().ChangeType(default);
        }

        [Test]
        public void ChangeVariableDuplicateName()
        {
            _variablesManagerMock.IsDuplicateName("abc").Returns(true);
            var newDto = DtoHelper.CreateVariableManage(EType.Void, "abc");
            Assert.Throws<InvalidVariableManageDto>(() => _sut.Change(newDto));

            ExpectEqualDto(_baseDto);
            _nodeMock.DidNotReceiveWithAnyArgs().ChangeType(default);
        }

        [Test]
        public void ChangeVariableNameSameType()
        {
            var newDto = DtoHelper.CreateVariableManage("test2");
            _variablesManagerMock.IsDuplicateName("test2").Returns(false);

            _sut.Change(newDto);

            Assert.AreEqual(_baseDto.Type, _sut.Type);
            Assert.AreEqual(newDto.VariableName, _sut.Name);
            _nodeMock.DidNotReceiveWithAnyArgs().ChangeType(default);
        }

        [Test]
        public void ChangeVariableNameAndType()
        {
            var newDto = DtoHelper.CreateVariableManage(EType.String, "test2");
            _variablesManagerMock.IsDuplicateName("test2").Returns(false);

            _sut.Change(newDto);

            ExpectEqualDto(newDto);
            _nodeMock.Received().ChangeType((IType)newDto.Type);
        }

        [Test]
        public void ShouldDeleteRefVariables()
        {
            _sut.Delete();
            _nodeMock.Received().Delete();
            Assert.True(_sut.IsDelete);
        }
    }
}

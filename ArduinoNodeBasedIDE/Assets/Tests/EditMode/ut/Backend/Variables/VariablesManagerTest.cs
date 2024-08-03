using System.Collections.Generic;
using Backend.Exceptions;
using Backend.Json;
using Backend.Type;
using Backend.Variables;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;
using Tests.EditMode.ut.Backend.Mocks;

namespace Tests.EditMode.ut.Backend.Variables
{
    [TestFixture]
    [TestOf(nameof(VariablesManager))]
    [Category("Variable")]
    public class VariablesManagerTest
    {

        private VariablesManagerMock _sut;
        
        [SetUp]
        public void Init()
        {
            _sut = new VariablesManagerMock();
        }

        [Test]
        public void ConstructorJsonTest()
        {
            var json1 = new VariableJson
            {
                Name = "test1",
                PathName = "TEST-1",
                Type = "string",
            };
            
            var json2 = new VariableJson
            {
                Name = "test2",
                PathName = "TEST-23",
                Type = "long",
            };

            var newSut = new VariablesManagerMock(new List<VariableJson> {json1, json2});
            
            Assert.AreEqual(2, newSut.Variables.Count);
            Assert.AreEqual("test1", newSut.Variables[0].Name);
            Assert.AreEqual("TEST-1", newSut.Variables[0].Id);
            Assert.AreEqual(EType.String, newSut.Variables[0].Type.EType);
            
            Assert.AreEqual("test2", newSut.Variables[1].Name);
            Assert.AreEqual("TEST-23", newSut.Variables[1].Id);
            Assert.AreEqual(EType.Long, newSut.Variables[1].Type.EType);

            var highestId = MockHelper.GetFieldValue<VariablesManager, long>(newSut, "_highestId");
            Assert.AreEqual(23, highestId);
        }
        
        [Test]
        public void AddVariableVoidNameTest()
        {
            var dto = DtoHelper.CreateVariableManage(EType.Void, "test");
            Assert.Throws<InvalidVariableManageDto>(() => _sut.AddVariable(dto));

        }

        [Test]
        public void AddVariableNullNameTest()
        {
            var dto = DtoHelper.CreateVariableManage(EType.Void, null);
            Assert.Throws<InvalidVariableManageDto>(() => _sut.AddVariable(dto));

        }

        [Test]
        public void AddVariableSameNameTest()
        {
            var dto = DtoHelper.CreateVariableManage();
            _sut.AddVariable(dto);

            Assert.True(_sut.IsDuplicateName(dto.VariableName));
        }

        [Test]
        public void ShouldCreate2Variables()
        {
            var variable1 = _sut.AddVariable(DtoHelper.CreateVariableManage("test1"));
            var variable2 = _sut.AddVariable(DtoHelper.CreateVariableManage("test2"));

            Assert.AreEqual(2, _sut.Variables.Count);
            Assert.AreSame(variable1, _sut.Variables[0]);
            Assert.AreSame(variable2, _sut.Variables[1]);
        }

        [Test]
        public void ShouldDeleteVariable()
        {
            var variable1 = Substitute.For<Variable>();
            _sut.AddRef(variable1);

            _sut.DeleteVariable(variable1);
            variable1.Received().Delete();
        }
    }
}

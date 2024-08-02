using Backend.Exceptions;
using Backend.Type;
using Backend.Variables;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Variables
{
    [TestFixture]
    [TestOf(nameof(VariablesManager))]
    [Category("Variable")]
    public class VariablesManagerTest : VariablesManager
    {
        protected override bool IsVariableDuplicate(string name)
        {
            return false;
        }

        [SetUp]
        public void Init()
        {
            Variables.Clear();
        }
        
        [Test]
        public void AddVariableVoidNameTest()
        {
            var dto = DtoHelper.CreateVariableManage(EType.Void, "test");
            Assert.Throws<InvalidVariableManageDto>(() => AddVariable(dto));

        }

        [Test]
        public void AddVariableNullNameTest()
        {
            var dto = DtoHelper.CreateVariableManage(EType.Void, null);
            Assert.Throws<InvalidVariableManageDto>(() => AddVariable(dto));

        }

        [Test]
        public void AddVariableSameNameTest()
        {
            var dto = DtoHelper.CreateVariableManage();
            AddVariable(dto);

            Assert.True(IsDuplicateName(dto.VariableName));
        }

        [Test]
        public void ShouldCreate2Variables()
        {
            var variable1 = AddVariable(DtoHelper.CreateVariableManage("test1"));
            var variable2 = AddVariable(DtoHelper.CreateVariableManage("test2"));

            Assert.AreEqual(2, Variables.Count);
            Assert.AreSame(variable1, Variables[0]);
            Assert.AreSame(variable2, Variables[1]);
        }

        [Test]
        public void ShouldDeleteVariable()
        {
            var variable1 = Substitute.For<Variable>();
            AddRef(variable1);

            DeleteVariable(variable1);
            variable1.Received().Delete();
        }
    }
}

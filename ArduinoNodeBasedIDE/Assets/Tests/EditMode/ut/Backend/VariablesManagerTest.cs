using Backend;
using Backend.Type;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;
using Tests.EditMode.ut.Backend.mocks;

namespace Tests.EditMode.ut.Backend
{
    [TestFixture]
    [TestOf(nameof(VariablesManager))]
    [Category("Variable")]
    public class VariablesManagerTest
    {
        private VariablesManager _sut;

        [SetUp]
        public void Init()
        {
            _sut = new VariablesManager();
        }

        [Test]
        public void IsDtoValidVoidType()
        {
            Assert.IsFalse(_sut.IsDtoValid(VariableHelper.CreateDto(EType.Void, "test")));
        }

        [Test]
        public void IsDtoValidTrue()
        {
            Assert.IsTrue(_sut.IsDtoValid(VariableHelper.CreateDto()));
        }

        [Test]
        public void IsDtoValidDuplicate()
        {
            var dto = VariableHelper.CreateDto();
            _sut.AddVariable(dto);

            Assert.IsFalse(_sut.IsDtoValid(dto));
        }

        [Test]
        public void ShouldCreate2Variables()
        {
            var variable1 = _sut.AddVariable(VariableHelper.CreateDto("test1"));
            var variable2 = _sut.AddVariable(VariableHelper.CreateDto("test2"));

            Assert.AreEqual(2, _sut.Variables.Count);
            Assert.AreSame(variable1, _sut.Variables[0]);
            Assert.AreSame(variable2, _sut.Variables[1]);
        }

        [Test]
        public void ShouldDeleteVariable()
        {
            var variable1 = Substitute.For<VariableMock>();
            _sut.AddRef(variable1);

            _sut.DeleteVariable(variable1);
            variable1.Received().Delete();
        }
    }
}

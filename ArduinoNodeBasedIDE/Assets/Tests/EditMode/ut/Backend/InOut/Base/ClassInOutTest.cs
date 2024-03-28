using System.Collections.Generic;
using Backend.Exceptions.InOut;
using Backend.InOut;
using Backend.InOut.Base;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.InOut.Base
{
    [TestFixture]
    [TestOf(typeof(ClassInOut))]
    [Category("InOut")]
    public class ClassInOutTest
    {
        private ClassInOut _classIn;

        [SetUp]
        public void Setup()
        {
            _classIn = (ClassInOut) InOutHelper.CreateBaseInOut(InOutType.Class, InOutSide.Input, classType: TypeHelper.CreateClassTypeMock("good"));
        }

        private static readonly List<IInOut> _wrong = new()
        {
            InOutHelper.CreateBaseInOut(InOutType.Class, classType: TypeHelper.CreateClassTypeMock("wrong")),
            InOutHelper.CreateBaseInOut(InOutType.Primitive),
            InOutHelper.CreateBaseInOut(InOutType.String),
            InOutHelper.CreateBaseInOut(InOutType.Flow),
            InOutHelper.CreateBaseInOut(InOutType.Void)
        };

        [Test]
        [TestCaseSource(nameof(_wrong))]
        public void WrongConnectionTypeException(IInOut output)
        {
            Assert.Throws<WrongConnectionTypeException>(() => _classIn.Connect(output));
        }

        private static readonly List<IInOut> _ok = new()
        {
            InOutHelper.CreateBaseInOut(InOutType.Class, classType: TypeHelper.CreateClassTypeMock("good")),
        };

        [Test]
        [TestCaseSource(nameof(_ok))]
        public void ConnectionOk(IInOut output)
        {
            //given
            //when
            _classIn.Connect(output);
            //then
            InOutHelper.ExpectAreConnected(_classIn, output);
        }
    }
}
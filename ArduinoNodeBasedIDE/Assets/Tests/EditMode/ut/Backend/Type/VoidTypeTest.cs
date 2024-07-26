using System.Collections.Generic;
using Backend.Type;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Type
{
    [TestFixture]
    [TestOf(typeof(VoidType))]
    [Category("Type")]
    public class VoidTypeTest
    {
        private VoidType _voidType;

        [SetUp]
        public void Init()
        {
            _voidType = new VoidType();
        }

        private static readonly List<(IType, bool)> _castParam = new()
        {
            (MockHelper.CreateClassType("wrong"), false),
            (new PrimitiveType(EType.Int), false),
            (new StringType(), false),
            (new VoidType(), false),
        };

        [Test]
        [TestCaseSource(nameof(_castParam))]
        public void CanBeCastFalse((IType, bool) param)
        {
            //given
            var (type, expect) = param;
            //when
            //then 
            Assert.AreEqual(expect, _voidType.CanBeCast(type));
        }

        [Test]
        public void Equal()
        {
            //given
            var voidType1 = new VoidType();
            var voidType2 = new VoidType();
            //when
            //then
            Assert.AreEqual(voidType1, voidType2);
            Assert.True(voidType1 == voidType2);
        }
    }
}

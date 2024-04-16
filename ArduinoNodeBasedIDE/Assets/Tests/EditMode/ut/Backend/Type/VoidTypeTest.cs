using System.Collections.Generic;
using Backend.API;
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

        private static readonly List<IType> _castFalse = new()
        {
            TypeHelper.CreateClassTypeMock("wrong"),
            new PrimitiveType(EType.Int),
            new StringType(),
            new VoidType(),
        };
        
        [Test]
        [TestCaseSource(nameof(_castFalse))]
        public void CanBeCastFalse(IType type)
        {
            //given
            //when
            //then 
            Assert.False(_voidType.CanBeCast(type));
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
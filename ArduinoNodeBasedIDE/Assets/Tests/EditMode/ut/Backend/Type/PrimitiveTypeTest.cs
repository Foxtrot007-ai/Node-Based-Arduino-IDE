using System.Collections.Generic;
using Backend.Exceptions;
using Backend.Type;
using NUnit.Framework;

namespace Tests.EditMode.ut.Backend.Type
{
    [TestFixture]
    [TestOf(typeof(PrimitiveType))]
    [Category("Type")]
    public class PrimitiveTypeTest
    {

        private static List<EType> _notPrimitive = new()
        {
            EType.Class,
            EType.String,
            EType.Void,
        };

        [Test]
        [TestCaseSource(nameof(_notPrimitive))]
        public void CreateWrongTypeException(EType type)
        {
            //given
            //when
            var exception = Assert.Throws<NotPrimitiveTypeException>(() => _ = new PrimitiveType(type));
            //then
            Assert.AreEqual(type + " is not primitive type.", exception.Message);
        }

        private static List<EType> _okPrimitive = new()
        {
            EType.Bool,
            EType.Int,
            EType.Short,
            EType.Long,
            EType.LongLong,
            EType.Double,
            EType.Float,
        };

        [Test]
        [TestCaseSource(nameof(_okPrimitive))]
        public void CreateOk(EType type)
        {
            //given
            //when
            var primitiveType = new PrimitiveType(type);
            //then
            Assert.AreEqual(type, primitiveType.EType);
        }

        [Test] 
        public void NotEqual()
        {
            //given
            var primitiveType1 = new PrimitiveType(EType.Int);
            var primitiveType2 = new PrimitiveType(EType.Double);
            //when
            //then
            Assert.AreNotEqual(primitiveType1, primitiveType2);
            Assert.False(primitiveType1 == primitiveType2);
        }

        [Test]
        [TestCaseSource(nameof(_okPrimitive))]
        public void Equal(EType type)
        {
            //given
            var primitiveType1 = new PrimitiveType(type);
            var primitiveType2 = new PrimitiveType(type);
            //when
            //then
            Assert.AreEqual(primitiveType1, primitiveType2);
            Assert.True(primitiveType1 == primitiveType2);
        }
    }
}
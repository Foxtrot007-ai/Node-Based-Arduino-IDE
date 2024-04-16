using System.Collections.Generic;
using Backend.API;
using Backend.Exceptions;
using Backend.Type;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Type
{
    [TestFixture]
    [TestOf(typeof(PrimitiveType))]
    [Category("Type")]
    public class PrimitiveTypeTest
    {

        private PrimitiveType _primitiveType;

        [SetUp]
        public void Init()
        {
            _primitiveType = new PrimitiveType(EType.Int);
        }
        
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
        
        private static readonly List<IType> _castFalse = new()
        {
            TypeHelper.CreateClassTypeMock("test"),
            new VoidType(),
        };
        
        [Test]
        [TestCaseSource(nameof(_castFalse))]
        public void CanBeCastFalse(IType type)
        {
            //given
            //when
            //then 
            Assert.False(_primitiveType.CanBeCast(type));
        }
        
        private static readonly List<IType> _castTrue = new()
        {
            new PrimitiveType(EType.LongLong),
            new StringType(),
        };

        [Test]
        [TestCaseSource(nameof(_castTrue))]
        public void CanBeCastTrue(IType type)
        {
            //given
            //when
            //then
            Assert.True(_primitiveType.CanBeCast(type));
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
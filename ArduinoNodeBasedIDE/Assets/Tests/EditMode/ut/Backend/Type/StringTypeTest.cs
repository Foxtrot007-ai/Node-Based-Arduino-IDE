using System.Collections.Generic;
using Backend.API;
using Backend.Type;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Type
{
    
    [TestFixture]
    [TestOf(typeof(StringType))]
    [Category("Type")]
    public class StringTypeTest
    {
        private StringType _stringType;

        [SetUp]
        public void Init()
        {
            _stringType = new StringType();
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
            Assert.False(_stringType.CanBeCast(type));
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
            Assert.True(_stringType.CanBeCast(type));
        }
        
        [Test]
        public void Equal()
        {
            //given
            var stringType1 = new StringType();
            var stringType2 = new StringType();
            //when
            //then
            Assert.AreEqual(stringType1, stringType2);
            Assert.True(stringType1 == stringType2);
        }
    }
}
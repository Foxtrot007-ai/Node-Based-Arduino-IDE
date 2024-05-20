using System.Collections.Generic;
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
        
        private static readonly List<(IType, bool)> _castParam = new()
        {
            (TypeHelper.CreateClassTypeMock("test"), false),
            (new VoidType(), false),
            (new PrimitiveType(EType.LongLong), true),
            (new StringType(), true),
        };
        
        [Test]
        [TestCaseSource(nameof(_castParam))]
        public void CanBeCastParamTest((IType, bool) param)
        {
            //given
            var (type, expect) = param;
            //when
            //then 
            Assert.AreEqual(expect, _stringType.CanBeCast(type));
        }
        
        private static readonly List<(IType, bool)> _adpterParam = new()
        {
            (TypeHelper.CreateClassTypeMock("test"), false),
            (new VoidType(), false),
            (new PrimitiveType(EType.LongLong), true),
            (new StringType(), false),
        };
        
        [Test]
        [TestCaseSource(nameof(_adpterParam))]
        public void IsAdapterNeedParamTest((IType, bool) param)
        {
            //given
            var (type, expect) = param;
            //when
            //then 
            Assert.AreEqual(expect, _stringType.IsAdapterNeed(type));
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
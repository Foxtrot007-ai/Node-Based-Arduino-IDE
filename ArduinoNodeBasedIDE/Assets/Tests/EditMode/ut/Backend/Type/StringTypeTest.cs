using Backend.Type;
using NUnit.Framework;

namespace Tests.EditMode.ut.Backend.Type
{
    
    [TestFixture]
    [TestOf(typeof(StringType))]
    [Category("Type")]
    public class StringTypeTest
    {

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
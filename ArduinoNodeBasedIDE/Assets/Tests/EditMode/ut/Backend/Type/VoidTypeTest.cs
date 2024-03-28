using Backend.Type;
using NUnit.Framework;

namespace Tests.EditMode.ut.Backend.Type
{
    [TestFixture]
    [TestOf(typeof(VoidType))]
    [Category("Type")]
    public class VoidTypeTest
    {

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
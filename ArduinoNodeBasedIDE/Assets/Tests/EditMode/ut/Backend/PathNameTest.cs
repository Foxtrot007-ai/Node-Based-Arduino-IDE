using Backend;
using NUnit.Framework;

namespace Tests.EditMode.ut.Backend
{
    [TestFixture]
    [TestOf(nameof(PathName))]
    public class PathNameTest
    {

        [Test]
        public void ConstructorTest()
        {
            Assert.AreEqual("ROOT-1/TEST-12", 
                            new PathName(new PathName("ROOT-1"), "TEST", 12).ToString());
        }
        
        [Test]
        [TestCase("ROOT-12")]
        [TestCase("ROOT-12/TEST-24")]
        public void GetId(string str)
        {
            var path = new PathName(str);
            Assert.AreEqual(12, path.GetId());
        }

        [Test]
        [TestCase("ROOT-12")]
        [TestCase("ROOT-12/TEST-21")]
        public void GetClassNamePath(string str)
        {
            var path = new PathName(str);
            Assert.AreEqual("ROOT", path.GetClassName());
        }
        
        [Test]
        [TestCase("ROOT-12")]
        [TestCase("ROOT-12/TEST-22")]
        public void GetFirstPath(string str)
        {
            var path = new PathName(str);
            Assert.AreEqual("ROOT-12", path.GetFirstPath().ToString());
        }
        
        [Test]
        public void GetNextPathNull()
        {
            var path = new PathName("ROOT-12");
            Assert.AreEqual(null, path.GetNextPath());
        }
        
        [Test]
        public void GetNextPath1()
        {
            var path = new PathName("ROOT-12/TEST-22");
            Assert.AreEqual("TEST-22", path.GetNextPath().ToString());
        }
        
        [Test]
        public void GetNextPath2()
        {
            var path = new PathName("ROOT-12/TEST-23/TEST-34/TEST-45");
            Assert.AreEqual("TEST-23/TEST-34/TEST-45", path.GetNextPath().ToString());
        }
        
    }
}

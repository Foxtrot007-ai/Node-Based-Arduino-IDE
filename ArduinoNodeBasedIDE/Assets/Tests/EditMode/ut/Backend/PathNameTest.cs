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
        public void GetId1()
        {
            var path = new PathName("ROOT-12");
            Assert.AreEqual(12, path.GetId());
        }

        [Test]
        public void GetId2()
        {
            var path = new PathName("ROOT-12/TEST-21");
            Assert.AreEqual(21, path.GetId());
        }

        [Test]
        public void GetClassName1()
        {
            var path = new PathName("ROOT-12");
            Assert.AreEqual("ROOT", path.GetClassName());
        }

        [Test]
        public void GetClassName2()
        {
            var path = new PathName("ROOT-12/TEST-21");
            Assert.AreEqual("TEST", path.GetClassName());
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

        [Test]
        public void GetLast()
        {
            var path = new PathName("ROOT-12");
            Assert.AreEqual("ROOT-12", path.GetLastPath().ToString());
        }

        [Test]
        public void GetLast1()
        {
            var path = new PathName("ROOT-12/TEST-22");
            Assert.AreEqual("TEST-22", path.GetLastPath().ToString());
        }

        [Test]
        public void GetLast2()
        {
            var path = new PathName("ROOT-12/TEST-23/TEST-34/TEST-45");
            Assert.AreEqual("TEST-45", path.GetLastPath().ToString());
        }

        [Test]
        public void GetParentNull()
        {
            var path = new PathName("ROOT-12");
            Assert.AreEqual(null, path.GetParent());
        }

        [Test]
        public void GetParent1()
        {
            var path = new PathName("ROOT-12/TEST-22");
            Assert.AreEqual("ROOT-12", path.GetParent().ToString());
        }

        [Test]
        public void GetParent2()
        {
            var path = new PathName("ROOT-12/TEST-23/TEST-34/TEST-45");
            Assert.AreEqual("ROOT-12/TEST-23/TEST-34", path.GetParent().ToString());
        }
    }
}

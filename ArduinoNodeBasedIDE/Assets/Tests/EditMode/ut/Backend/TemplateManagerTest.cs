using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Backend;
using Backend.Template.Json;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;
using Tests.EditMode.ut.Backend.Mocks;

namespace Tests.EditMode.ut.Backend
{
    [TestFixture]
    [TestOf(nameof(TemplateManager))]
    [Category("Template")]
    public class TemplateManagerTest
    {
        private MockFileSystem _fileSystem;
        private IDirectory _functionsDir;
        private IDirectory _classesDir;
        private const int BuildInNumber = 12;
        
        [SetUp]
        public void Init()
        {
            _fileSystem = new MockFileSystem();
            _fileSystem.AddDirectory("testDir/classes");
            _fileSystem.AddDirectory("testDir/functions");
        }

        [Test]
        public void ConstructorEmptyDirs()
        {
            var sut = new TemplateManagerMock(_fileSystem);
            
            Assert.AreEqual(BuildInNumber, sut.Templates.Count);
        }

        [Test]
        public void Constructor()
        {
            
        }
    }
}

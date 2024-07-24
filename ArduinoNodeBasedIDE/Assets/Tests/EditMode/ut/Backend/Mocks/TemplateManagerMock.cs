using System.IO.Abstractions;
using Backend;

namespace Tests.EditMode.ut.Backend.Mocks
{
    public class TemplateManagerMock : TemplateManager
    {   
        public TemplateManagerMock(IFileSystem fileSystem) : base(fileSystem, "testDir")
        {
        }
    }
}

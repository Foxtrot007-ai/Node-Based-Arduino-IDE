using System.Collections.Generic;
using Backend;
using Backend.API;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Mocks.IO;

namespace Tests.EditMode.ut.Backend
{
    [TestFixture]
    [TestOf(nameof(CodeManager))]
    [Category("Code")]
    public class CodeManagerTest
    {
        private CodeManager _sut;
        private Variable _variableMock;

        [SetUp]
        public void Init()
        {
            _sut = new CodeManager();
            _variableMock = Substitute.For<Variable>();
        }
        [Test]
        public void CopyConstructorTest()
        {
            _sut.SetVariableStatus(_variableMock, CodeManager.VariableStatus.Set);

            var copyManager = new CodeManager(_sut);

            _sut.AddLibrary("test");

            Assert.AreSame(_sut.Includes, copyManager.Includes);
            Assert.AreEqual(_sut.Includes, copyManager.Includes);

            Assert.AreNotSame(_sut.Variables, copyManager.Variables);
            Assert.AreEqual(1, copyManager.Variables.Count);

            Assert.AreNotSame(_sut.CodeLines, copyManager.CodeLines);
        }

        [Test]
        public void GetVariableNotExistTest()
        {
            Assert.AreEqual(CodeManager.VariableStatus.Unknown, _sut.GetVariableStatus(_variableMock));
        }

        [Test]
        public void SetAndGetVariableTest([Values] CodeManager.VariableStatus variableStatus)
        {
            _sut.SetVariableStatus(_variableMock, variableStatus);

            Assert.AreEqual(variableStatus, _sut.GetVariableStatus(_variableMock));
        }

        [Test]
        public void AddLineTest()
        {
            _sut.AddLine("test");

            Assert.AreEqual("test", _sut.CodeLines[0]);
        }

        [Test]
        public void AddLinesSingleLine()
        {
            var line = new List<string> { "test" };

            _sut.AddLines(line);

            Assert.AreEqual("test", _sut.CodeLines[0]);
        }

        [Test]
        public void AddLinesMultipleTest()
        {
            var lines = new List<string> { "test1", "test2" };

            _sut.AddLines(lines);

            Assert.AreEqual("{", _sut.CodeLines[0]);
            Assert.AreEqual("test1", _sut.CodeLines[1]);
            Assert.AreEqual("test2", _sut.CodeLines[2]);
            Assert.AreEqual("}", _sut.CodeLines[3]);
        }

        [Test]
        public void BuildParamTest()
        {
            var any1 = Substitute.For<TypeIOMock>();
            any1.MakeConnect();
            any1.ToCodeParamReturn(_sut, "test1");

            var any2 = Substitute.For<TypeIOMock>();
            any2.MakeConnect();
            any2.ToCodeParamReturn(_sut, "test2");

            var any3 = Substitute.For<TypeIOMock>();
            any3.MakeConnect();
            any3.ToCodeParamReturn(_sut, "test3");

            Assert.AreEqual("test1, test2, test3",
                            _sut.BuildParamCode(new List<IConnection>() { any1, any2, any3 }));
        }
    }
}

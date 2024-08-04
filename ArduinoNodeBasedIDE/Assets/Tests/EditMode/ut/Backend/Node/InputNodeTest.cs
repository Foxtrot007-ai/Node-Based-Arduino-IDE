using System.Collections.Generic;
using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Node
{
    [TestFixture]
    [TestOf(nameof(InputNode))]
    [Category("Node")]
    public class InputNodeTest : BaseNodeTestSetup
    {
        private InputNode _sut;

        [SetUp]
        public void Init()
        {
            _sut = new InputNode(_buildInTemplateMock);

            PrepareBaseSetup(_sut);
            SetInOutMock<InputNode>("_output", _auto1);
        }

        private void SetValue(string value)
        {
            MockHelper.SetPropertyValue<InputNode, string>(_sut, "Value", value);
        }

        private static List<EType> _numbers = new()
        {
            EType.Int,
            EType.Short,
            EType.Long,
            EType.LongLong,
        };

        [Test]
        [TestCaseSource(nameof(_numbers))]
        public void SetValueNumber(EType eType)
        {
            _auto1.MyType.EType.Returns(eType);

            _sut.SetValue("1234");
        }

        [Test]
        [TestCaseSource(nameof(_numbers))]
        public void SetValueNumberNoDisconnect(EType eType)
        {
            SetValue("123");
            _auto1.MyType.EType.Returns(eType);

            _sut.ConnectNotify(_auto1._connectedMock);
            _auto1.DidNotReceive().Disconnect();
        }

        [Test]
        [TestCase("false")]
        [TestCase("true")]
        public void SetValueBool(string val)
        {
            _auto1.MyType.EType.Returns(EType.Bool);

            _sut.SetValue(val);
        }

        [Test]
        [TestCase("false")]
        [TestCase("true")]
        public void SetValueBoolNoDisconnect(string val)
        {
            SetValue(val);
            _auto1.MyType.EType.Returns(EType.Bool);

            _sut.ConnectNotify(_auto1._connectedMock);
            _auto1.DidNotReceive().Disconnect();
        }

        [Test]
        [TestCase(EType.Double)]
        [TestCase(EType.Float)]
        public void SetValueFDNumber(EType eType)
        {
            _auto1.MyType.EType.Returns(eType);

            _sut.SetValue("12,12");
        }

        [Test]
        [TestCase(EType.Double)]
        [TestCase(EType.Float)]
        public void SetValueFDNumberNoDisconnect(EType eType)
        {
            SetValue("12,12");
            _auto1.MyType.EType.Returns(eType);

            _sut.ConnectNotify(_auto1._connectedMock);
            _auto1.DidNotReceive().Disconnect();
        }

        [Test]
        [TestCase("false")]
        [TestCase("true")]
        [TestCase("123")]
        [TestCase("1,23")]
        [TestCase("1.da23")]
        public void SetValueString(string val)
        {
            _auto1.MyType.EType.Returns(EType.String);

            _sut.SetValue(val);
        }

        [Test]
        [TestCase("false")]
        [TestCase("true")]
        [TestCase("123")]
        [TestCase("1,23")]
        [TestCase("1.da23")]
        public void SetValueStringNoDisconnect(string val)
        {
            SetValue(val);
            _auto1.MyType.EType.Returns(EType.String);

            _sut.ConnectNotify(_auto1._connectedMock);
            _auto1.DidNotReceive().Disconnect();
        }

        [Test]
        [TestCaseSource(nameof(_numbers))]
        [TestCase(EType.Bool)]
        public void SetValueNoNumber(EType eType)
        {
            _auto1.MyType.EType.Returns(eType);

            Assert.Throws<WrongConnectionTypeException>(() => _sut.SetValue("abxc"));
            Assert.Throws<WrongConnectionTypeException>(() => _sut.SetValue("12,32"));
        }

        [Test]
        [TestCase(EType.Double)]
        [TestCase(EType.Float)]
        public void SetValueNoFDNumber(EType eType)
        {
            _auto1.MyType.EType.Returns(eType);

            Assert.Throws<WrongConnectionTypeException>(() => _sut.SetValue("abxc"));
            Assert.Throws<WrongConnectionTypeException>(() => _sut.SetValue("12.32"));
        }

        [Test]
        [TestCaseSource(nameof(_numbers))]
        [TestCase(EType.Bool)]
        [TestCase(EType.Double)]
        [TestCase(EType.Float)]
        public void SetValueNoNumberDisconnect(EType eType)
        {
            SetValue("abc");
            _auto1.MyType.EType.Returns(eType);

            Assert.Throws<WrongConnectionTypeException>(() => _sut.ConnectNotify(_auto1._connectedMock));

            _auto1.Received().Disconnect();
        }
    }
}

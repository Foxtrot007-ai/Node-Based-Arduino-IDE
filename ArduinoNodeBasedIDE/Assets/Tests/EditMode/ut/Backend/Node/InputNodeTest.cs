using System.Collections.Generic;
using Backend.API.DTO;
using Backend.Exceptions;
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
            SetInOutMock<InputNode>("_output", _typeOut3);
            SetOutputsList(_typeOut3);
            SetValue("test");
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
        [TestCase(EType.Float)]
        [TestCase(EType.Double)]
        [TestCase(EType.Bool)]
        [TestCase(EType.String)]
        public void ValidDtoEmptyTrue(EType eType)
        {
            var dto = new InputNodeValueDto
            {
                Type = MockHelper.CreateType(eType),
                Value = "",
            };
            Assert.True(dto.IsDtoValid());
        }

        [Test]
        [TestCaseSource(nameof(_numbers))]
        public void ValidDtoNumberTrue(EType eType)
        {
            var dto = new InputNodeValueDto
            {
                Type = MockHelper.CreateType(eType),
                Value = "123",
            };
            Assert.True(dto.IsDtoValid());
        }


        [Test]
        [TestCase("false")]
        [TestCase("true")]
        public void ValidDtoBoolTrue(string val)
        {
            var dto = new InputNodeValueDto
            {
                Type = MockHelper.CreateType(EType.Bool),
                Value = val,
            };
            Assert.True(dto.IsDtoValid());
        }

        [Test]
        [TestCase(EType.Double)]
        [TestCase(EType.Float)]
        public void ValidDtoFDNumberTrue(EType eType)
        {
            var dto = new InputNodeValueDto
            {
                Type = MockHelper.CreateType(eType),
                Value = "12,3",
            };
            Assert.True(dto.IsDtoValid());
        }

        [Test]
        [TestCase("false")]
        [TestCase("true")]
        [TestCase("123")]
        [TestCase("1,23")]
        [TestCase("1.da23")]
        public void ValidDtoStringTrue(string val)
        {
            var dto = new InputNodeValueDto
            {
                Type = MockHelper.CreateType(EType.String),
                Value = val,
            };
            Assert.True(dto.IsDtoValid());
        }

        [Test]
        public void ValidDtoVoidFalse()
        {
            var dto = new InputNodeValueDto
            {
                Type = MockHelper.CreateType(EType.Void),
                Value = "any",
            };
            Assert.False(dto.IsDtoValid());
        }

        [Test]
        public void ValidDtoClassFalse()
        {
            var dto = new InputNodeValueDto
            {
                Type = MockHelper.CreateType(EType.Class),
                Value = "any",
            };
            Assert.False(dto.IsDtoValid());
        }

        [Test]
        [TestCaseSource(nameof(_numbers))]
        public void ValidDtoNumberFalse(EType eType)
        {
            var dto = new InputNodeValueDto
            {
                Type = MockHelper.CreateType(eType),
                Value = "12,34",
            };
            Assert.False(dto.IsDtoValid());
        }

        [Test]
        [TestCase("test")]
        [TestCase("1")]
        [TestCase("123,43")]
        public void ValidDtoNumberFalse(string val)
        {
            var dto = new InputNodeValueDto
            {
                Type = MockHelper.CreateType(EType.Bool),
                Value = val,
            };
            Assert.False(dto.IsDtoValid());
        }

        [Test]
        [TestCase(EType.Double)]
        [TestCase(EType.Float)]
        public void ValidDtoFDNumberFalse(EType eType)
        {
            var dto = new InputNodeValueDto
            {
                Type = MockHelper.CreateType(eType),
                Value = "12.34h",
            };
            Assert.False(dto.IsDtoValid());
        }

        [Test]
        public void SetValueInvalidDto()
        {
            var dto = new InputNodeValueDto
            {
                Type = MockHelper.CreateType(EType.Bool),
                Value = "gdsrt",
            };

            Assert.Throws<InvalidInputNodeValueDto>(() => _sut.SetValue(dto));

            _typeOut3.DidNotReceiveWithAnyArgs().ChangeType(default);
            Assert.AreEqual("test", _sut.Value);
        }

        [Test]
        public void SetValueOk()
        {
            var type = MockHelper.CreateType(EType.Float);
            var dto = new InputNodeValueDto
            {
                Type = type,
                Value = "12,54",
            };

            _sut.SetValue(dto);

            _typeOut3.Received().ChangeType(type);
            Assert.AreEqual("12,54", _sut.Value);
        }

        [Test]
        [TestCaseSource(nameof(_numbers))]
        public void ToCodeParamEmptyNumber(EType eType)
        {
            SetValue("");
            _typeOut3.MyType.EType.Returns(eType);

            Assert.AreEqual("0", _sut.ToCodeParam(_codeManagerMock));
        }

        [Test]
        [TestCase(EType.Bool, "false")]
        [TestCase(EType.Double, "0")]
        [TestCase(EType.Float, "0")]
        [TestCase(EType.String, "\"\"")]
        public void ToCodeParamEmpty(EType eType, string expect)
        {
            SetValue("");
            _typeOut3.MyType.EType.Returns(eType);

            Assert.AreEqual(expect, _sut.ToCodeParam(_codeManagerMock));
        }

        [Test]
        [TestCaseSource(nameof(_numbers))]
        public void ToCodeParamNumber(EType eType)
        {
            SetValue("1234");
            _typeOut3.MyType.EType.Returns(eType);

            Assert.AreEqual("1234", _sut.ToCodeParam(_codeManagerMock));
        }

        [Test]
        [TestCase(EType.Bool, "false")]
        [TestCase(EType.Double, "124,35")]
        [TestCase(EType.Float, "124,35")]
        public void ToCodeParam(EType eType, string expect)
        {
            SetValue(expect);
            _typeOut3.MyType.EType.Returns(eType);

            Assert.AreEqual(expect, _sut.ToCodeParam(_codeManagerMock));
        }

        [Test]
        public void ToCodeParamString()
        {
            SetValue("test");
            _typeOut3.MyType.EType.Returns(EType.String);

            Assert.AreEqual("\"test\"", _sut.ToCodeParam(_codeManagerMock));
        }
    }
}

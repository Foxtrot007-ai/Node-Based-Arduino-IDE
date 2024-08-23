using System;
using System.Collections.Generic;
using Backend.API;
using Backend.IO;
using Backend.MyFunction;
using Backend.Node;
using Backend.Type;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Node
{
    [TestFixture]
    [TestOf(nameof(UserFunctionNode))]
    [Category("Node")]
    [Category("Function")]
    public class UserFunctionNodeTest : BaseNodeTestSetup
    {
        private UserFunction _userFunctionMock;

        private UserFunctionNode _sut;

        [SetUp]
        public void Init()
        {
            _userFunctionMock = Substitute.For<UserFunction>();
            _userFunctionMock.InputList.Variables.Returns(new List<IVariable>());
            var type = MockHelper.CreateType();
            _userFunctionMock.OutputType.Returns(type);

            _sut = new UserFunctionNode(_userFunctionMock);

            PrepareBaseSetup(_sut);

        }

        private void PrepareVoidSetup()
        {
            SetInputsList(_prevMock, _type1, _type2);
            SetOutputsList(_nextMock);
        }

        private void PrepareNonVoidSetup()
        {
            SetInputsList(_type1, _type2);
            SetOutputsList(_typeOut3);
        }

        [Test]
        public void VoidConstructorTest()
        {
            var outType = MockHelper.CreateType(EType.Void);
            _userFunctionMock.OutputType.Returns(outType);

            var type1 = MockHelper.CreateType();
            var type2 = MockHelper.CreateType(EType.Bool);
            var list = new List<IVariable>
            {
                MockHelper.CreateVariable(type1),
                MockHelper.CreateVariable(type2),
            };
            _userFunctionMock.InputList.Variables.Returns(list);

            var sut = new UserFunctionNode(_userFunctionMock);

            Assert.AreEqual(3, sut.InputsList.Count);
            Assert.IsInstanceOf<FlowIO>(sut.InputsList[0]);
            ExpectHelper.TypeEqual(type1, sut.InputsList[1]);
            ExpectHelper.TypeEqual(type2, sut.InputsList[2]);

            Assert.AreEqual(1, sut.OutputsList.Count);
            Assert.IsInstanceOf<FlowIO>(sut.OutputsList[0]);
            _userFunctionMock.Received().AddRef(sut);
        }

        [Test]
        public void NonVoidConstructorTest()
        {
            var outType = MockHelper.CreateType(EType.String);
            _userFunctionMock.OutputType.Returns(outType);

            var type1 = MockHelper.CreateType();
            var type2 = MockHelper.CreateType(EType.Bool);
            var list = new List<IVariable>
            {
                MockHelper.CreateVariable(type1),
                MockHelper.CreateVariable(type2),
            };
            _userFunctionMock.InputList.Variables.Returns(list);

            var sut = new UserFunctionNode(_userFunctionMock);

            Assert.AreEqual(2, sut.InputsList.Count);
            ExpectHelper.TypeEqual(type1, sut.InputsList[0]);
            ExpectHelper.TypeEqual(type2, sut.InputsList[1]);

            Assert.AreEqual(1, sut.OutputsList.Count);
            ExpectHelper.TypeEqual(outType, sut.OutputsList[0]);

            _userFunctionMock.Received().AddRef(sut);
        }

        [Test]
        public void NonVoidAddParamTest()
        {
            PrepareNonVoidSetup();

            var type = MockHelper.CreateType(EType.String);
            var newParam = MockHelper.CreateVariable(type);

            _sut.AddParam(newParam);

            EqualSizeInput(3);
            EqualInput(_type1, 0);
            EqualInput(_type2, 1);
            EqualTypeInput(type, 2);

            EqualSizeOutput(1);
            EqualOutput(_typeOut3, 0);
        }
        
        [Test]
        public void VoidAddParamTest()
        {
            PrepareVoidSetup();

            var type = MockHelper.CreateType(EType.String);
            var newParam = MockHelper.CreateVariable(type);

            _sut.AddParam(newParam);

            EqualSizeInput(4);
            EqualInput(_prevMock, 0);
            EqualInput(_type1, 1);
            EqualInput(_type2, 2);
            EqualTypeInput(type, 3);

            EqualSizeOutput(1);
            EqualOutput(_nextMock, 0);
        }

        [Test]
        public void NonVoidChangeParamTest()
        {
            PrepareNonVoidSetup();

            var type = MockHelper.CreateType(EType.String);
            var newParam = MockHelper.CreateVariable(type);

            _sut.ChangeParam(0, newParam);

            EqualSizeInput(2);
            EqualInput(_type1, 0);
            EqualInput(_type2, 1);

            EqualSizeOutput(1);
            EqualOutput(_typeOut3, 0);
            _type1.Received().ChangeType(newParam.Type);
        }
        
        [Test]
        public void VoidChangeParamTest()
        {
            PrepareVoidSetup();

            var type = MockHelper.CreateType(EType.String);
            var newParam = MockHelper.CreateVariable(type);

            _sut.ChangeParam(0, newParam);

            EqualSizeInput(3);
            EqualInput(_prevMock, 0);
            EqualInput(_type1, 1);
            EqualInput(_type2, 2);

            EqualSizeOutput(1);
            EqualOutput(_nextMock, 0);
            _type1.Received().ChangeType(newParam.Type);
        }
        
        [Test]
        public void VoidRemoveParamTest()
        {
            PrepareVoidSetup();

            _sut.RemoveParam(0);

            _prevMock.DidNotReceive().Delete();
            _type1.Received().Delete();
            _type2.DidNotReceive().Delete();
            _nextMock.DidNotReceive().Delete();
        }

        [Test]
        public void NonVoidRemoveParamTest()
        {
            PrepareNonVoidSetup();

            _sut.RemoveParam(0);

            _prevMock.DidNotReceive().Delete();
            _type1.Received().Delete();
            _type2.DidNotReceive().Delete();
            _nextMock.DidNotReceive().Delete();
        }

        [Test]
        public void ChangeTypeToVoidTest()
        {
            PrepareNonVoidSetup();

            var type = MockHelper.CreateType(EType.Void);

            _sut.ChangeOutputType(type);

            EqualSizeInput(3);
            EqualInput(_prevMock, 0);
            EqualInput(_type1, 1);
            EqualInput(_type2, 2);

            EqualSizeOutput(1);
            EqualOutput(_nextMock, 0);
            _typeOut3.Received().Delete();
        }

        [Test]
        public void ChangeVoidToTypeTest()
        {
            PrepareVoidSetup();

            var type = MockHelper.CreateType(EType.String);

            _sut.ChangeOutputType(type);

            _prevMock.Received().Disconnect();
            _nextMock.Received().Disconnect();

            EqualSizeInput(2);
            EqualInput(_type1, 0);
            EqualInput(_type2, 1);

            EqualSizeOutput(1);
            EqualTypeOutput(type, 0);
        }

        [Test]
        public void ChangeTypeToTypeTest()
        {
            PrepareNonVoidSetup();

            var type = MockHelper.CreateType(EType.String);

            _sut.ChangeOutputType(type);

            _prevMock.DidNotReceive().Disconnect();
            _nextMock.DidNotReceive().Disconnect();

            EqualSizeInput(2);
            EqualInput(_type1, 0);
            EqualInput(_type2, 1);

            EqualSizeOutput(1);
            EqualOutput(_typeOut3, 0);
            _typeOut3.Received().ChangeType(type);
        }

        [Test]
        public void VoidToCodeParamThrowsTest()
        {
            PrepareVoidSetup();
            Assert.Throws<NotImplementedException>(() => _sut.ToCodeParam(_codeManagerMock));
        }

        [Test]
        public void VoidToCodeTest()
        {
            PrepareVoidSetup();

            _userFunctionMock.Name.Returns("name");
            _type1.ToCodeParamReturn(_codeManagerMock, "test1");
            _type2.ToCodeParamReturn(_codeManagerMock, "test2");

            _sut.ToCode(_codeManagerMock);

            _codeManagerMock.Received().AddLine("name(test1, test2);");
            _codeManagerMock.DidNotReceiveWithAnyArgs().AddLibrary(default);
            ExpectNextToCode();
        }

        [Test]
        public void NonVoidToCodeThrowsTest()
        {
            PrepareNonVoidSetup();
            Assert.Throws<NotImplementedException>(() => _sut.ToCode(_codeManagerMock));
        }

        [Test]
        public void NonVoidToCodeTest()
        {
            PrepareNonVoidSetup();

            _userFunctionMock.Name.Returns("name");
            _type1.ToCodeParamReturn(_codeManagerMock, "test1");
            _type2.ToCodeParamReturn(_codeManagerMock, "test2");

            var code = _sut.ToCodeParam(_codeManagerMock);

            Assert.AreEqual("name(test1, test2)", code);
            _codeManagerMock.DidNotReceiveWithAnyArgs().AddLibrary(default);
        }

        [Test]
        public void DeleteTest()
        {
            PrepareVoidSetup();

            _sut.Delete();

            _userFunctionMock.Received().DeleteRef(_sut);
        }
    }
}

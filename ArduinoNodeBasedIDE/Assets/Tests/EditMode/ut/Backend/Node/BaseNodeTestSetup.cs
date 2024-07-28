using System.Linq;
using System.Reflection;
using Backend;
using Backend.API;
using Backend.Connection;
using Backend.IO;
using Backend.Node;
using Backend.Template;
using Backend.Type;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;
using Tests.EditMode.ut.Backend.Mocks.IO;

namespace Tests.EditMode.ut.Backend.Node
{
    public class BaseNodeTestSetup
    {
        protected CodeManager _codeManagerMock;
        protected FlowIOMock _prevMock;
        protected FlowIOMock _nextMock;
        protected BuildInTemplate _buildInTemplateMock;

        protected TypeIOMock _type1, _type2, _type3;
        protected AutoIOMock _auto1;
        protected TypeIOMock _class1;

        private BaseNode _sut;

        public void PrepareBaseSetup(BaseNode sut)
        {
            _buildInTemplateMock = Substitute.For<BuildInTemplate>();
            _codeManagerMock = Substitute.For<CodeManager>();
            _prevMock = MockHelper.CreateFlowIO();
            _nextMock = MockHelper.CreateFlowIO();
            _type1 = MockHelper.CreateTypeIO();
            _type2 = MockHelper.CreateTypeIO();
            _type3 = MockHelper.CreateTypeIO();
            _auto1 = MockHelper.CreateAutoIO();
            _class1 = MockHelper.CreateClassIO();

            _sut = sut;

            SetFlowMocks();
            MakeAllConnected();
        }

        public void MakeAllConnected()
        {
            _prevMock.MakeConnect();
            _nextMock.MakeConnect();
            _type1.MakeConnect();
            _type2.MakeConnect();
            _type3.MakeConnect();
            _auto1.MakeConnect();
            _class1.MakeConnect();
        }
        public void SetFlowMocks()
        {
            SetInOutMock<BaseNode>("_prevNode", _prevMock);
            SetInOutMock<BaseNode>("_nextNode", _nextMock);
        }

        public void SetInOutMock<T>(string name, BaseIO baseIOMock)
        {
            typeof(T)
                .GetField(name, BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(_sut, baseIOMock);
        }

        public void SetInputsList(params IConnection[] inOutMocks)
        {
            typeof(BaseNode)
                .GetProperty("InputsList", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(_sut, inOutMocks.ToList(), null);
        }

        public void SetOutputsList(params IConnection[] inOutMocks)
        {
            typeof(BaseNode)
                .GetProperty("OutputsList", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(_sut, inOutMocks.ToList(), null);
        }

        public void MakeFlowConnected()
        {
            _prevMock.MakeConnect();
            _nextMock.MakeConnect();
        }

        public void ExpectNextToCode()
        {
            _nextMock.ExpectToCode(_codeManagerMock);
        }

        public void EqualSizeInput(int size)
        {
            Assert.AreEqual(size, _sut.InputsList.Count);
        }

        public void EqualSizeOutput(int size)
        {
            Assert.AreEqual(size, _sut.OutputsList.Count);
        }

        public void EqualInput(BaseIO baseIO, int index)
        {
            Assert.AreEqual(baseIO, _sut.InputsList[index]);
        }

        public void EqualTypeInput(IType type, int index)
        {
            ExpectHelper.TypeEqual(type, _sut.InputsList[index]);
        }

        public void EqualOutput(BaseIO baseIO, int index)
        {
            Assert.AreEqual(baseIO, _sut.OutputsList[index]);
        }

        public void EqualTypeOutput(IType type, int index)
        {
            ExpectHelper.TypeEqual(type, _sut.OutputsList[index]);
        }
    }
}

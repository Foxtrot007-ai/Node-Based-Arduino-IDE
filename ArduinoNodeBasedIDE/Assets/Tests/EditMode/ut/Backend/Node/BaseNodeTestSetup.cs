using System.Linq;
using System.Reflection;
using Backend;
using Backend.API;
using Backend.Connection;
using Backend.Node;
using Backend.Template;
using Backend.Type;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;
using Tests.EditMode.ut.Backend.Mocks.Connection;

namespace Tests.EditMode.ut.Backend.Node
{
    public class BaseNodeTestSetup
    {
        protected CodeManager _codeManagerMock;
        protected FlowInOutMock _prevMock;
        protected FlowInOutMock _nextMock;
        protected BuildInTemplate _buildInTemplateMock;

        protected AnyInOutMock _any1, _any2, _any3;
        protected AutoInOutMock _auto1;
        protected ClassInOutMock _class1;

        private BaseNode _sut;

        public void PrepareBaseSetup(BaseNode sut)
        {
            _buildInTemplateMock = Substitute.For<BuildInTemplate>();
            _codeManagerMock = Substitute.For<CodeManager>();
            _prevMock = MockHelper.CreateFLowInOut();
            _nextMock = MockHelper.CreateFLowInOut();
            _any1 = MockHelper.CreateAnyInOut();
            _any2 = MockHelper.CreateAnyInOut();
            _any3 = MockHelper.CreateAnyInOut();
            _auto1 = MockHelper.CreateAutoInOut();
            _class1 = MockHelper.CreateClassInOut();

            _sut = sut;
            
            SetFlowMocks();
            MakeAllConnected();
        }

        public void MakeAllConnected()
        {
            _prevMock.MakeConnect();
            _nextMock.MakeConnect();
            _any1.MakeConnect();
            _any2.MakeConnect();
            _any3.MakeConnect();
            _auto1.MakeConnect();
            _class1.MakeConnect();
        }
        public void SetFlowMocks()
        {
            SetInOutMock<BaseNode>("_prevNode", _prevMock);
            SetInOutMock<BaseNode>("_nextNode", _nextMock);
        }

        public void SetInOutMock<T>(string name, InOut inOutMock)
        {
            typeof(T)
                .GetField(name, BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(_sut, inOutMock);
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
        
        public void EqualInput(InOut inOut, int index)
        {
            Assert.AreEqual(inOut, _sut.InputsList[index]);
        }

        public void EqualTypeInput(IType type, int index)
        {
            ExpectHelper.TypeEqual(type, _sut.InputsList[index]);
        }
        
        public void EqualOutput(InOut inOut, int index)
        {
            Assert.AreEqual(inOut, _sut.OutputsList[index]);
        }
        
        public void EqualTypeOutput(IType type, int index)
        {
            ExpectHelper.TypeEqual(type, _sut.OutputsList[index]);
        }
    }
}

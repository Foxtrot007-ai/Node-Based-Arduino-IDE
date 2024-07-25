using System.Collections.Generic;
using System.Reflection;
using Backend;
using Backend.API;
using Backend.Connection;
using Backend.Node;
using Backend.Template;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Mocks.Connection;

namespace Tests.EditMode.ut.Backend.Node
{
    [TestFixture]
    public class BaseNodeTestSetup
    {
        protected CodeManager _codeManagerMock;
        protected FlowInOutMock _prevMock;
        protected FlowInOutMock _nextMock;
        protected InOutMock _inOutMock;
        protected BuildInTemplate _buildInTemplateMock;


        [SetUp]
        public virtual void Init()
        {
            _codeManagerMock = Substitute.For<CodeManager>();
            _prevMock = Substitute.For<FlowInOutMock>();
            _prevMock.InOutType.Returns(InOutType.Flow);
            _nextMock = Substitute.For<FlowInOutMock>();
            _nextMock.InOutType.Returns(InOutType.Flow);
            _inOutMock = Substitute.For<InOutMock>();
            _buildInTemplateMock = Substitute.For<BuildInTemplate>();

        }

        public void SetFlowMocks(BaseNode baseNode)
        {
            SetInOutMock<BaseNode>(baseNode, "_prevNode", _prevMock);
            SetInOutMock<BaseNode>(baseNode, "_nextNode", _nextMock);
        }

        public void SetInOutMock<T>(BaseNode node, string name, InOut inOutMock)
        {
            typeof(T)
                .GetField(name, BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(node, inOutMock);
        }

        public void SetInputsList(BaseNode node, List<IConnection> inOutMocks)
        {
            typeof(BaseNode)
                .GetProperty("InputsList", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(node, inOutMocks, null);
        }

        public void SetOutputsList(BaseNode node, List<IConnection> inOutMocks)
        {
            typeof(BaseNode)
                .GetProperty("OutputsList", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(node, inOutMocks, null);
        }

        public AnyInOutMock CreateAnyInOutMock()
        {
            var inOut = Substitute.For<AnyInOutMock>();
            inOut.InOutType.Returns(InOutType.Dynamic);
            return inOut;
        }

        public ClassInOutMock CreateClassInOutMock()
        {
            var inOut = Substitute.For<ClassInOutMock>();
            inOut.InOutType.Returns(InOutType.Class);
            return inOut;
        }

        public AutoInOutMock CreateAutoInOutMock()
        {
            var inOut = Substitute.For<AutoInOutMock>();
            inOut.InOutType.Returns(InOutType.Auto);
            return inOut;
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
    }
}

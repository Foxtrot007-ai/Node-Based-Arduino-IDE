using System;
using System.Collections.Generic;
using System.Linq;
using Backend.API;
using Backend.Connection;
using Backend.Exceptions;
using Backend.IO;

namespace Backend.Node
{
    public abstract class BaseNode : INode
    {
        public virtual string NodeName { get; protected set; }
        public bool IsDeleted { get; private set; }
        public virtual NodeType NodeType { get; protected set; }
        public virtual string CreatorId { get; } = "0";
        public virtual List<IConnection> InputsList { get; private set; } = new();
        public virtual List<IConnection> OutputsList { get; private set; } = new();

        protected FlowIO _prevNode;
        protected FlowIO _nextNode;

        protected BaseNode(string creatorId) : this()
        {
            CreatorId = creatorId;
        }
        protected BaseNode()
        {
            _prevNode = new FlowIO(this, IOSide.Input, "prev");
            _nextNode = new FlowIO(this, IOSide.Output, "next");
        }

        public virtual void Delete()
        {
            InputsList.ForEach(x => ((BaseIO)x).Delete());
            OutputsList.ForEach(x => ((BaseIO)x).Delete());
            IsDeleted = true;
        }

        private void CheckIfConnected(BaseIO baseIO)
        {
            if (!baseIO.IsOptional && baseIO.Connected == null)
            {
                throw new InOutMustBeConnectedException(baseIO);
            }
        }

        protected void ConnectedToCode(CodeManager codeManager, BaseIO baseIO)
        {
            ((BaseIO)baseIO.Connected).ParentNode.ToCode(codeManager);
        }

        protected string ConnectedToCodeParam(CodeManager codeManager, BaseIO baseIO)
        {
            return ((BaseIO)baseIO.Connected).ParentNode.ToCodeParam(codeManager);
        }
        protected void CheckToCode()
        {
            InputsList.ForEach(x => CheckIfConnected((BaseIO)x));
            OutputsList.ForEach(x => CheckIfConnected((BaseIO)x));
        }

        protected virtual void MakeCode(CodeManager codeManager)
        {
            throw new NotImplementedException();
        }
        protected virtual string MakeCodeParam(CodeManager codeManager)
        {
            throw new NotImplementedException();
        }

        public virtual void ToCode(CodeManager codeManager)
        {
            CheckToCode();
            MakeCode(codeManager);
            ConnectedToCode(codeManager, _nextNode);
        }

        public virtual string ToCodeParam(CodeManager codeManager)
        {
            CheckToCode();
            return MakeCodeParam(codeManager);
        }

        protected void AddInputs(params BaseIO[] inputs)
        {
            foreach (var input in inputs)
            {
                InputsList.Add(input);
            }
        }

        protected void AddOutputs(params BaseIO[] outputs)
        {
            foreach (var output in outputs)
            {
                OutputsList.Add(output);
            }
        }

        protected virtual void RemoveInOut(IConnection iConnection)
        {
            var inOut = (BaseIO)iConnection;
            var list = inOut.Side == IOSide.Input ? InputsList : OutputsList;
            list.Remove(inOut);
            inOut.Delete();
        }

        protected void AddFlowInputs()
        {
            InputsList.Insert(0, _prevNode);
            OutputsList.Insert(0, _nextNode);
        }

        protected void RemoveFlowInputs()
        {
            if (InputsList.Count != 0 && InputsList[0].IOType == IOType.Flow)
            {
                InputsList[0].Disconnect();
                InputsList.RemoveAt(0);
            }

            if (OutputsList.Count != 0 && OutputsList[0].IOType == IOType.Flow)
            {
                OutputsList[0].Disconnect();
                OutputsList.RemoveAt(0);
            }
        }

        protected List<IConnection> GetWithoutFlow(List<IConnection> list)
        {
            if (list.Count == 0)
                return list;
            return list[0].IOType == IOType.Flow ? list.Skip(1).ToList() : list;
        }

        protected bool IsFlow()
        {
            return OutputsList[0].IOType == IOType.Flow;
        }
    }
}

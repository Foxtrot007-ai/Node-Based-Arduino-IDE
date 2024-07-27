using System;
using System.Collections.Generic;
using System.Linq;
using Backend.API;
using Backend.Connection;
using Backend.Exceptions;

namespace Backend.Node
{
    public abstract class BaseNode : INode
    {
        public virtual string NodeName { get; protected set; }
        public bool IsDeleted { get; private set; }
        public virtual NodeType NodeType { get; protected set; }
        public virtual List<IConnection> InputsList { get; private set; } = new();
        public virtual List<IConnection> OutputsList { get; private set; } = new();

        protected FlowInOut _prevNode;
        protected FlowInOut _nextNode;

        protected BaseNode()
        {
            _prevNode = new FlowInOut(this, InOutSide.Input, "prev");
            _nextNode = new FlowInOut(this, InOutSide.Output, "next");
        }

        public virtual void Delete()
        {
            InputsList.ForEach(x => ((InOut)x).Delete());
            OutputsList.ForEach(x => ((InOut)x).Delete());
            IsDeleted = true;
        }

        private void CheckIfConnected(InOut inOut)
        {
            if (!inOut.IsOptional && inOut.Connected == null)
            {
                throw new InOutMustBeConnectedException(inOut);
            }
        }

        protected void ConnectedToCode(CodeManager codeManager, InOut inOut)
        {
            ((InOut)inOut.Connected).ParentNode.ToCode(codeManager);
        }

        protected string ConnectedToCodeParam(CodeManager codeManager, InOut inOut)
        {
            return ((InOut)inOut.Connected).ParentNode.ToCodeParam(codeManager);
        }
        protected void CheckToCode()
        {
            InputsList.ForEach(x => CheckIfConnected((InOut)x));
            OutputsList.ForEach(x => CheckIfConnected((InOut)x));
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

        protected void AddInputs(params InOut[] inputs)
        {
            foreach (var input in inputs)
            {
                InputsList.Add(input);
            }
        }

        protected void AddOutputs(params InOut[] outputs)
        {
            foreach (var output in outputs)
            {
                OutputsList.Add(output);
            }
        }

        protected virtual void RemoveInOut(IConnection iConnection)
        {
            var inOut = (InOut)iConnection;
            var list = inOut.Side == InOutSide.Input ? InputsList : OutputsList;
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
            if (InputsList.Count != 0 && InputsList[0].InOutType == InOutType.Flow)
            {
                InputsList[0].Disconnect();
                InputsList.RemoveAt(0);
            }

            if (OutputsList.Count != 0 && OutputsList[0].InOutType == InOutType.Flow)
            {
                OutputsList[0].Disconnect();
                OutputsList.RemoveAt(0);
            }
        }

        protected List<IConnection> GetWithoutFlow(List<IConnection> list)
        {
            if (list.Count == 0)
                return list;
            return list[0].InOutType == InOutType.Flow ? list.Skip(1).ToList() : list;
        }

        protected bool IsFlow()
        {
            return OutputsList[0].InOutType == InOutType.Flow;
        }
    }
}

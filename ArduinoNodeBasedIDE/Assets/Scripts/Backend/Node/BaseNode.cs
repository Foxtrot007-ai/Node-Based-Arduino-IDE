using System;
using System.Collections.Generic;
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
        public virtual List<IConnection> InputsList { get; }
        public virtual List<IConnection> OutputsList { get; }

        protected FlowInOut _prevNode;
        protected FlowInOut _nextNode;

        protected BaseNode()
        {
            _prevNode = new FlowInOut(this, InOutSide.Input, "prev");
            _nextNode = new FlowInOut(this, InOutSide.Output, "next");
            InputsList = new List<IConnection>();
            OutputsList = new List<IConnection>();
        }

        public virtual void Delete()
        {
            InputsList.ForEach(x => ((InOut)x).Delete());
            OutputsList.ForEach(x => ((InOut)x).Delete());
            IsDeleted = true;
        }

        protected void CheckFlowConnected()
        {
            CheckIfConnected(_prevNode);
            CheckIfConnected(_nextNode);
        }
        protected void CheckIfConnected(InOut inOut)
        {
            if (inOut.Connected == null)
            {
                throw new InOutMustBeConnectedException(inOut);
            }
        }

        protected void ConnectedToCode(CodeManager codeManager, InOut inOut)
        {
            inOut.ConnectedInOut.ParentNode.ToCode(codeManager);
        }

        protected void NextToCode(CodeManager codeManager)
        {
            ConnectedToCode(codeManager, _nextNode);
        }

        protected string ConnectedToCodeParam(CodeManager codeManager, InOut inOut)
        {
            return inOut.ConnectedInOut.ParentNode.ToCodeParam(codeManager);
        }
        protected abstract void CheckToCode();
        public virtual void ToCode(CodeManager codeManager)
        {
            throw new NotImplementedException();
        }
        public virtual string ToCodeParam(CodeManager codeManager)
        {
            throw new NotImplementedException();
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
            foreach (var input in outputs)
            {
                OutputsList.Add(input);
            }
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
    }
}

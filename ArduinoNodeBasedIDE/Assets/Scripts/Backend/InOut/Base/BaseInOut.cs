using System;
using System.Collections.Generic;
using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;

namespace Backend.InOut.Base
{
    public abstract class BaseInOut : IInOut
    {
        public ConnectionPoint UIPoint { get; set; }
        public IPlaceHolderNodeType ParentNode { get; }
        public IInOut Connected { get; set; }

        public abstract IMyType MyType { get; }
        public InOutSide Side { get; }
        public InOutType InOutType { get; }
        public virtual string InOutName => MyType.TypeName;
        IConnection IConnection.Connected => Connected;


        protected BaseInOut(IPlaceHolderNodeType parentNode, InOutSide side, InOutType inOutType)
        {
            ParentNode = parentNode;
            Side = side;
            Connected = null;
            InOutType = inOutType;
        }

        public void Connect(IConnection iConnection)
        {
            if (iConnection is not IInOut inOut)
            {
                throw new ArgumentNullException();
            }

            CheckIsConnected(inOut);
            CheckSide(inOut);
            CheckSelfConnection(inOut);
            CheckInOutType(inOut);
            //TODO CheckAdapter()
            CheckCycle(inOut);
            inOut.Connected = this;
            Connected = inOut;
        }

        public void Reconnect(IInOut inOut)
        {
            try
            {
                CheckInOutType(inOut);
                //CheckAdapter()
                //CheckSide and CheckCycle no need bcs only type of connection change 
                inOut.Connected = this;
                Connected = inOut;
            }
            catch (InOutException)
            {
            }
        }

        public void Disconnect()
        {
            Connected.Connected = null;
            this.Connected = null;
        }

        private void CheckIsConnected(IInOut inOut)
        {
            if (Connected is not null || inOut.Connected is not null)
            {
                throw new AlreadyConnectedException();
            }
        }

        private void CheckSide(IInOut iInOut)
        {
            if (Side == iInOut.Side)
            {
                throw new SameSideException();
            }
        }

        private void CheckSelfConnection(IInOut iInOut)
        {
            if (iInOut.ParentNode == ParentNode)
            {
                throw new SelfConnectionException();
            }
        }

        private void CheckInputCycle(IInOut inOut)
        {
            Stack<IInOut> inPoints = new Stack<IInOut>(ParentNode.InputsListInOut);
            while (inPoints.Count > 0)
            {
                IPlaceHolderNodeType newParent = inPoints.Pop().Connected?.ParentNode;
                if (newParent is null)
                {
                    continue;
                }

                if (newParent == inOut.ParentNode)
                {
                    throw new CycleException();
                }

                foreach (var newInput in newParent.InputsListInOut)
                {
                    inPoints.Push(newInput);
                }
            }
        }

        private void CheckOutputCycle(IInOut inOut)
        {
            Stack<IInOut> outPoints = new Stack<IInOut>(ParentNode.OutputsListInOut);
            while (outPoints.Count > 0)
            {
                IPlaceHolderNodeType newParent = outPoints.Pop().Connected?.ParentNode;
                if (newParent is null)
                {
                    continue;
                }

                if (newParent == inOut.ParentNode)
                {
                    throw new CycleException();
                }

                foreach (var newOutput in newParent.OutputsListInOut)
                {
                    outPoints.Push(newOutput);
                }
            }
        }

        private void CheckCycle(IInOut inOut)
        {
            CheckInputCycle(inOut);
            CheckOutputCycle(inOut);
        }

        protected virtual void CheckInOutType(IInOut iInOut)
        {
            if (iInOut.InOutType is InOutType.Dynamic or InOutType.Void)
            {
                throw new WrongConnectionTypeException();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;
using Castle.Core.Internal;

namespace Backend.InOut
{
    public abstract class BaseInOut : IInOut
    {
        public ConnectionPoint UIPoint { get; set; }
        public IPlaceHolderNodeType ParentNode { get; }
        public IInOut Connected { get; set; }

        public InOutSide Side { get; }
        public InOutType InOutType { get; }
        public abstract string InOutName { get; }
        IConnection IConnection.Connected => Connected;


        protected BaseInOut(IPlaceHolderNodeType parentNode, InOutSide side, InOutType inOutType)
        {
            ParentNode = parentNode;
            Side = side;
            Connected = null;
            InOutType = inOutType;
        }

        public virtual void Connect(IConnection iConnection)
        {
            if (iConnection is not IInOut inOut)
            {
                throw new ArgumentNullException();
            }

            CheckIsConnected(inOut);
            CheckSide(inOut);
            CheckSelfConnection(inOut);
            CheckCycle(inOut);
            inOut.Connected = this;
            Connected = inOut;
        }

        public virtual void Reconnect(IInOut inOut)
        {
            //CheckSide and CheckCycle no need bcs only type of connection change 
            inOut.Connected = this;
            Connected = inOut;
        }

        public virtual void Disconnect()
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
        
        private void CheckCycle(IInOut inOut)
        {
            var visited = new HashSet<IPlaceHolderNodeType> { ParentNode };
            var toVisit = new Stack<IPlaceHolderNodeType>();
            toVisit.Push(ParentNode);

            while (!toVisit.IsNullOrEmpty())
            {
                var parent = toVisit.Pop();
                if (parent == inOut.ParentNode)
                {
                    throw new CycleException();
                }
                
                foreach (var iter in parent.InputsListInOut.Concat(parent.OutputsListInOut))
                {
                    var newParent = iter.Connected?.ParentNode;
                    if (newParent is not null && visited.Add(newParent))
                    {
                        toVisit.Push(newParent);
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;
using Castle.Core.Internal;

namespace Backend.Connection
{
    public abstract class InOut : IConnection
    {
        public ConnectionPoint UIPoint { get; set; }
        public IPlaceHolderNodeType ParentNode { get; }
        private InOut _connected;

        public InOutSide Side { get; }
        public InOutType InOutType { get; }
        public abstract string InOutName { get; }
        public IConnection Connected => _connected;


        protected InOut(IPlaceHolderNodeType parentNode, InOutSide side, InOutType inOutType)
        {
            ParentNode = parentNode;
            Side = side;
            _connected = null;
            InOutType = inOutType;
        }

        public void Connect(IConnection iConnection)
        {
            var inOut = (InOut)iConnection;
            Check(inOut);

            inOut._connected = this;
            _connected = inOut;
        }

        public virtual void Reconnect(InOut inOut)
        {
            //CheckSide and CheckCycle no need bcs only type of connection change 
            inOut._connected = this;
            _connected = inOut;
        }

        public virtual void Disconnect()
        {
            _connected._connected = null;
            _connected = null;
        }

        protected virtual void Check(InOut inOut)
        {
            if (inOut is null)
            {
                throw new ArgumentNullException();
            }

            CheckIsConnected(inOut);
            CheckSide(inOut);
            CheckSelfConnection(inOut);
            CheckCycle(inOut);
        }

        private void CheckIsConnected(InOut inOut)
        {
            if (_connected is not null || inOut._connected is not null)
            {
                throw new AlreadyConnectedException();
            }
        }

        private void CheckSide(InOut iInOut)
        {
            if (Side == iInOut.Side)
            {
                throw new SameSideException();
            }
        }

        private void CheckSelfConnection(InOut iInOut)
        {
            if (iInOut.ParentNode == ParentNode)
            {
                throw new SelfConnectionException();
            }
        }

        private void CheckCycle(InOut inOut)
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
                    var newParent = iter._connected?.ParentNode;
                    if (newParent is not null && visited.Add(newParent))
                    {
                        toVisit.Push(newParent);
                    }
                }
            }
        }
        
    }
}

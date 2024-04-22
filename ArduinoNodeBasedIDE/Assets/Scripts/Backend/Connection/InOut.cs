using System;
using System.Collections.Generic;
using System.Linq;
using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;
using Castle.Core.Internal;
using UnityEngine;

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
            PreCheck(iConnection);
            var input = Side == InOutSide.Input ? this : (InOut)iConnection;
            var output = Side == InOutSide.Output ? this : (InOut)iConnection;
            // Connect output to input
            try
            {
                output.BeforeConnectHandler(input);
                input.BeforeConnectHandler(output);

                output.Check(input);
                
                output.ConnectHandler(input);
                input.ConnectHandler(output);
                
                output.AfterConnectHandler(input);
                input.AfterConnectHandler(output);
            }
            catch(Exception exception)
            {
                output.ErrorConnectHandler(input, exception);
                input.ErrorConnectHandler(output, exception);
                throw;
            }
        }

        protected virtual void BeforeConnectHandler(InOut inOut)
        {
            ;
        }

        private void ConnectHandler(InOut inOut)
        {
            _connected = inOut;
        }

        protected virtual void AfterConnectHandler(InOut inOut)
        {
            ;
        }

        protected virtual void ErrorConnectHandler(InOut inOut, Exception exception)
        {
            ;
        }

        public virtual void Disconnect()
        {
            var inOut = _connected;

            try
            {
                BeforeDisconnectHandler(inOut);
                inOut.BeforeDisconnectHandler(this);
            
                DisconnectHandler(inOut);
                inOut.DisconnectHandler(this);
                
                AfterDisconnectHandler(inOut);
                inOut.AfterDisconnectHandler(this);
            }
            catch(Exception exception)
            {
                ErrorDisconnectHandler(inOut, exception);
                inOut.ErrorDisconnectHandler(this, exception);
                throw;
            }
        }
        
        protected virtual void BeforeDisconnectHandler(InOut inOut)
        {
            ;
        }

        private void DisconnectHandler(InOut inOut)
        {
            _connected = null;
        }

        protected virtual void AfterDisconnectHandler(InOut inOut)
        {
            ;
        }

        protected virtual void ErrorDisconnectHandler(InOut inOut, Exception exception)
        {
            ;
        }
        
        protected virtual void PreCheck(IConnection iConnection)
        {
            if (iConnection is not InOut inOut)
            {
                throw new ArgumentNullException(null, "Connect argument cannot be null");
            }
            
            CheckIsConnected(inOut);
            CheckSide(inOut);
            CheckSelfConnection(inOut);
            CheckCycle(inOut);
        }

        protected virtual void Check(InOut inOut)
        {
            ;
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

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
        public virtual ConnectionPoint UIPoint { get; set; }
        public BaseNode ParentNode { get; }
        public virtual InOut ConnectedInOut { get; private set; }
        public InOutSide Side { get; }
        public abstract InOutType InOutType { get; }
        public abstract string InOutName { get; }
        public bool IsDeleted { get; private set; }

        public virtual IConnection Connected => ConnectedInOut;
        private List<ISubscribeInOut> _subscribe;
        protected InOut(BaseNode parentNode, InOutSide side)
        {
            ParentNode = parentNode;
            Side = side;
            IsDeleted = false;
            ConnectedInOut = null;
            _subscribe = new List<ISubscribeInOut>();
        }

        public virtual void Connect(IConnection iConnection)
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
            ConnectedInOut = inOut;
        }

        protected virtual void AfterConnectHandler(InOut inOut)
        {
            _subscribe.ForEach(x => x.ConnectNotify(this));
        }

        protected virtual void ErrorConnectHandler(InOut inOut, Exception exception)
        {
            ;
        }

        public virtual void Disconnect()
        {
            var inOut = ConnectedInOut;

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
            ConnectedInOut = null;
        }

        protected virtual void AfterDisconnectHandler(InOut inOut)
        {
            _subscribe.ForEach(x => x.DisconnectNotify(this));
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

        protected virtual void Check(InOut input)
        {
            ;
        }

        protected void ReCheck()
        {
            if (ConnectedInOut is null)
                return;
            
            var output = Side == InOutSide.Output ? this : (MyTypeInOut) ConnectedInOut;
            var input = Side == InOutSide.Input ? this : (MyTypeInOut) ConnectedInOut;
            try
            {
                output.Check(input);
            }
            catch (InOutException e)
            {
                Disconnect();
                throw;
            }
        }

        private void CheckIsConnected(InOut inOut)
        {
            if (ConnectedInOut is not null || inOut.ConnectedInOut is not null)
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
            var visited = new HashSet<BaseNode> { ParentNode };
            var toVisit = new Stack<BaseNode>();
            toVisit.Push(ParentNode);

            while (!toVisit.IsNullOrEmpty())
            {
                var parent = toVisit.Pop();
                if (parent == inOut.ParentNode)
                {
                    throw new CycleException();
                }

                foreach (InOut iter in parent.InputsList.Concat(parent.OutputsList))
                {
                    var newParent = iter.ConnectedInOut?.ParentNode;
                    if (newParent is not null && visited.Add(newParent))
                    {
                        toVisit.Push(newParent);
                    }
                }
            }
        }

        public virtual void Subscribe(ISubscribeInOut subscribeInOut)
        {
            _subscribe.Add(subscribeInOut);
        }

        public virtual void Unsubscribe(ISubscribeInOut subscribeInOut)
        {
            _subscribe.Remove(subscribeInOut);
        }

        public virtual void Delete()
        {
            Disconnect();
            IsDeleted = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;
using Castle.Core.Internal;

namespace Backend.Connection
{
    public abstract class BaseIO : IConnection
    {
        public virtual ConnectionPoint UIPoint { get; set; }
        public BaseNode ParentNode { get; }
        private BaseIO _connected;
        public IOSide Side { get; }
        public abstract IOType IOType { get; }
        public abstract string IOName { get; }
        public bool IsDeleted { get; private set; }
        public virtual bool IsOptional { get; }

        public virtual IConnection Connected => _connected;
        private List<ISubscribeIO> _subscribe;
        protected BaseIO(BaseNode parentNode, IOSide side, bool isOptional = false)
        {
            ParentNode = parentNode;
            Side = side;
            IsDeleted = false;
            IsOptional = isOptional;
            _connected = null;
            _subscribe = new List<ISubscribeIO>();
        }

        public virtual void Connect(IConnection iConnection)
        {
            PreCheck(iConnection);
            var input = Side == IOSide.Input ? this : (BaseIO)iConnection;
            var output = Side == IOSide.Output ? this : (BaseIO)iConnection;
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

        protected virtual void BeforeConnectHandler(BaseIO baseIO)
        {
            ;
        }

        private void ConnectHandler(BaseIO baseIO)
        {
            _connected = baseIO;
        }

        protected virtual void AfterConnectHandler(BaseIO baseIO)
        {
            _subscribe.ForEach(x => x.ConnectNotify(this));
        }

        protected virtual void ErrorConnectHandler(BaseIO baseIO, Exception exception)
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

        protected virtual void BeforeDisconnectHandler(BaseIO baseIO)
        {
            ;
        }

        private void DisconnectHandler(BaseIO baseIO)
        {
            _connected = null;
        }

        protected virtual void AfterDisconnectHandler(BaseIO baseIO)
        {
            _subscribe.ForEach(x => x.DisconnectNotify(this));
        }

        protected virtual void ErrorDisconnectHandler(BaseIO baseIO, Exception exception)
        {
            ;
        }

        protected virtual void PreCheck(IConnection iConnection)
        {
            if (iConnection is not BaseIO inOut)
            {
                throw new ArgumentNullException(null, "Connect argument cannot be null");
            }

            CheckIsConnected(inOut);
            CheckSide(inOut);
            CheckSelfConnection(inOut);
            CheckCycle(inOut);
        }

        protected virtual void Check(BaseIO input)
        {
            ;
        }

        protected void ReCheck()
        {
            if (_connected is null)
                return;

            var output = Side == IOSide.Output ? this : (TypeIO)_connected;
            var input = Side == IOSide.Input ? this : (TypeIO)_connected;
            try
            {
                output.Check(input);
            }
            catch(IOException e)
            {
                Disconnect();
                throw;
            }
        }

        private void CheckIsConnected(BaseIO baseIO)
        {
            if (_connected is not null || baseIO._connected is not null)
            {
                throw new AlreadyConnectedException();
            }
        }

        private void CheckSide(BaseIO baseIO)
        {
            if (Side == baseIO.Side)
            {
                throw new SameSideException();
            }
        }

        private void CheckSelfConnection(BaseIO baseIO)
        {
            if (baseIO.ParentNode == ParentNode)
            {
                throw new SelfConnectionException();
            }
        }

        private void CheckCycle(BaseIO baseIO)
        {
            var visited = new HashSet<BaseNode> { ParentNode };
            var toVisit = new Stack<BaseNode>();
            toVisit.Push(ParentNode);

            while (!toVisit.IsNullOrEmpty())
            {
                var parent = toVisit.Pop();
                if (parent == baseIO.ParentNode)
                {
                    throw new CycleException();
                }

                foreach (BaseIO iter in parent.InputsList.Concat(parent.OutputsList))
                {
                    var newParent = iter._connected?.ParentNode;
                    if (newParent is not null && visited.Add(newParent))
                    {
                        toVisit.Push(newParent);
                    }
                }
            }
        }

        public virtual void Subscribe(ISubscribeIO subscribeIO)
        {
            _subscribe.Add(subscribeIO);
        }

        public virtual void Unsubscribe(ISubscribeIO subscribeIO)
        {
            _subscribe.Remove(subscribeIO);
        }

        public virtual void Delete()
        {
            Disconnect();
            IsDeleted = true;
        }
    }
}

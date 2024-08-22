using System.Collections.Generic;
using Backend.API;
using Backend.Node;
using Backend.Type;

namespace Backend.IO
{
    public class ChainIO : AutoIO
    {
        public ChainIO Prev { get; private set; }
        public ChainIO Next { get; private set; }
        private bool _autoResize;
        private bool _singleType;

        public ChainIO(BaseNode parentNode, IOSide side, bool autoResize = true, bool isOptional = true, IType oneType = null) : base(
            parentNode,
            side,
            true,
            isOptional)
        {
            _autoResize = autoResize;
            if (oneType != null)
            {
                _singleType = true;
                WasMyTypeSet = true;
                _myType = oneType;
            }
        }

        private void TryRemoveMe()
        {
            // Head
            if (Prev is null)
            {
                return;
            }

            // First input
            if (Prev.Side == IOSide.Output)
            {
                // Only 1 input
                if (Next is null)
                {
                    return;
                }

                // Only 2 inputs
                if (Next.Next is null)
                {
                    return;
                }
            }
            //
            // if (Prev.Prev is null)
            // {
            //     return;
            // }
            //
            // if (Prev.Prev.Side == IOSide.Output)
            // {
            //     // Only 2 inputs
            //     if (Next is null)
            //     {
            //         return;
            //     }
            // }

            // Remove me from list
            if (Next is not null)
            {
                Next.Prev = Prev;
            }
            Prev.Next = Next;
        }

        protected override void AfterDisconnectHandler(BaseIO baseIO)
        {
            // Head 
            if (Prev is null)
            {
                ResetMyType();
                base.AfterDisconnectHandler(baseIO);
                return;
            }

            // I might be leader
            if (!Prev.WasMyTypeSet)
            {
                ResetMyType();
            }

            base.AfterDisconnectHandler(baseIO);
            if (_autoResize)
            {
                TryRemoveMe();
            }
        }

        protected override void AfterConnectHandler(BaseIO baseIO)
        {
            // Tail
            if (_autoResize && Next == null)
            {
                AppendChain(new ChainIO(ParentNode, Side, _autoResize, true, _singleType ? _myType : null));
                if (WasMyTypeSet)
                {
                    Next.ChangeType(MyType);
                }
            }

            // Nothing to do
            if (baseIO is AutoIO { MyType: null })
            {
                base.AfterConnectHandler(baseIO);
                return;
            }

            // I might be new leader
            if (!WasMyTypeSet)
            {
                var typeIO = (TypeIO)baseIO;
                ChangeType(typeIO.MyType);
            }
            base.AfterConnectHandler(baseIO);
        }

        public override void ResetMyType()
        {
            if (_singleType)
            {
                return;
            }

            // I am not new leader
            if (Connected is null or AutoIO { WasMyTypeSet: false })
            {
                base.ResetMyType();
                Next?.ResetMyType();
            }
            else
            {
                // I might be new leader
                var typeIO = (TypeIO)Connected;
                ChangeType(typeIO.MyType);
            }
        }

        public override void ChangeType(IType iMyType)
        {
            if (_singleType)
            {
                return;
            }

            base.ChangeType(iMyType);
            Next?.ChangeType(iMyType);
        }

        public void AppendChain(ChainIO chainIO)
        {
            Next = chainIO;
            chainIO.Prev = this;
        }

        public List<IConnection> ToList()
        {
            var list = new List<IConnection>();
            var iter = this;
            while (iter is not null)
            {
                list.Add(iter);
                iter = iter.Next;
            }
            return list;
        }

        public override void UpdateType(IType type)
        {
            if (_singleType)
            {
                return;
            }

            base.UpdateType(type);
            // I am leader
            if (Prev is null || !Prev.WasMyTypeSet)
            {
                ChangeType(type);
            }
        }
    }
}

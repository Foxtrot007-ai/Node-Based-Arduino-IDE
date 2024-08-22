using System;
using System.Collections.Generic;
using Backend.API;
using Backend.Exceptions;
using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.IO
{
    public class TypeIO : BaseIO
    {
        protected IType _myType;
        public virtual IType MyType => _myType;
        public override IOType IOType => HelperIO.ETypeToInOut(MyType.EType);
        public override string IOName => MyType.TypeName;
        private List<ISubscribeIO> _subscribe = new();

        public TypeIO(BaseNode parentNode, IOSide side, IType myType, bool isOptional = false) :
            base(parentNode, side, isOptional)
        {
            _myType = myType;
        }

        protected override void PreCheck(IConnection iConnection)
        {
            if (iConnection is not TypeIO)
            {
                throw new WrongConnectionTypeException();
            }
            base.PreCheck(iConnection);
        }

        protected override void Check(BaseIO input)
        {
            base.Check(input);
            var typeInput = (TypeIO)input;
            if (typeInput is AutoIO autoIO)
            {
                if (!autoIO.CanBeType(MyType))
                    throw new WrongConnectionTypeException();
                if (!autoIO.WasMyTypeSet)
                    return;
            }
            CheckCast(typeInput);
            CheckAdapter(typeInput);
        }

        private void CheckCast(TypeIO typeInput)
        {
            if (!MyType.CanBeCast(typeInput.MyType))
            {
                throw new CannotBeCastException();
            }
        }

        private void CheckAdapter(TypeIO typeInput)
        {
            if (MyType.IsAdapterNeed(typeInput.MyType))
            {
                throw new AdapterNeedException();
            }
        }

        protected override void AfterConnectHandler(BaseIO baseIO)
        {
            _subscribe.ForEach(x => x.ConnectNotify(this));
        }

        protected override void AfterDisconnectHandler(BaseIO baseIO)
        {
            _subscribe.ForEach(x => x.DisconnectNotify(this));
        }

        public virtual void Subscribe(ISubscribeIO subscribeIO)
        {
            _subscribe.Add(subscribeIO);
        }

        public virtual void Unsubscribe(ISubscribeIO subscribeIO)
        {
            _subscribe.Remove(subscribeIO);
        }

        public virtual void ChangeType(IType iType)
        {
            if (iType is null)
            {
                throw new ArgumentNullException(null, "Cannot change type to null.");
            }

            if (Side == IOSide.Input && iType.EType == EType.Void)
            {
                throw new WrongTypeException("Cannot change type to void for input side.");
            }
            _myType = iType;

            if (Connected is AutoIO autoIO)
            {
                autoIO.UpdateType(iType);
            }
            _subscribe.ForEach(x => x.TypeChangeNotify(this));
            ReCheck();
        }
    }
}

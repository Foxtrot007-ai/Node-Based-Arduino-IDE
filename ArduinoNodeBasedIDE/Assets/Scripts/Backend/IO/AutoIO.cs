using Backend.API;
using Backend.Exceptions;
using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.IO
{
    public class AutoIO : TypeIO
    {
        public bool WasMyTypeSet { get; protected set; }
        public override string IOName => MyType is null ? "Auto" : base.IOName;
        public override IOType IOType => MyType is null ? IOType.Auto : base.IOType;
        protected bool _canBeClass;

        public AutoIO(BaseNode parentNode, IOSide side, bool canBeClass = true, bool isOptional = false) : base(parentNode, side, null, isOptional)
        {
            _canBeClass = canBeClass;
        }

        public bool CanBeType(IMyType type)
        {
            return _canBeClass || type.EType != EType.Class;
        }

        protected override void Check(BaseIO input)
        {
            if (!CanBeType(((TypeIO)input).MyType))
            {
                throw new WrongConnectionTypeException();
            }

            if (!WasMyTypeSet)
            {
                return;
            }

            base.Check(input);
        }

        protected override void BeforeConnectHandler(BaseIO baseIO) //remove?
        {
            base.BeforeConnectHandler(baseIO);
            TypeIO typeIO = (TypeIO)baseIO;
            if (!WasMyTypeSet)
            {
                _myType = typeIO.MyType;
            }
        }

        protected override void AfterDisconnectHandler(BaseIO baseIO) //remove?
        {
            base.AfterDisconnectHandler(baseIO);
            if (!WasMyTypeSet)
            {
                _myType = null;
            }
        }

        public override void ChangeType(IType iMyType)
        {
            if (!CanBeType(iMyType))
            {
                throw new WrongTypeException("This IO cannot be class.");
            }

            base.ChangeType(iMyType);
            WasMyTypeSet = true;
        }

        public virtual void ResetMyType()
        {
            WasMyTypeSet = false;
            if (Connected is null)
            {
                _myType = null;
                return;
            }

            if (Connected is AutoIO { WasMyTypeSet: false } autoIO)
            {
                _myType = null;
                autoIO.UpdateType(null);
                return;
            }
            _myType = ((TypeIO)Connected).MyType;
        }

        public virtual void UpdateType(IType type)
        {
            if (!WasMyTypeSet)
            {
                _myType = type;
            }
        }
    }
}

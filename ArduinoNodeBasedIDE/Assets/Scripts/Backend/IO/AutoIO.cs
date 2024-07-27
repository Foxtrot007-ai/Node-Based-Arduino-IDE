using Backend.Connection;
using Backend.Node;
using Backend.Type;

namespace Backend.IO
{
    public class AutoIO : TypeIO
    {
        private bool _wasMyTypeSet = false;
        public override IType MyType => GetMyType();
        public override string IOName => MyType is null ? "Auto" : base.IOName;
        public override IOType IOType => MyType is null ? IOType.Auto : base.IOType;

        public AutoIO(BaseNode parentNode, IOSide side, bool isOptional = false) : base(parentNode, side, null, isOptional)
        {
        }

        protected override void Check(BaseIO input)
        {
            if (!_wasMyTypeSet)
            {
                return;
            }

            base.Check(input);
        }

        protected override void BeforeConnectHandler(BaseIO baseIO) //remove?
        {
            base.BeforeConnectHandler(baseIO);
            TypeIO typeIO = (TypeIO)baseIO;
            if (!_wasMyTypeSet)
            {
                _myType = typeIO.MyType;
            }
        }

        protected override void AfterDisconnectHandler(BaseIO baseIO) //remove?
        {
            base.AfterDisconnectHandler(baseIO);
            if (!_wasMyTypeSet)
            {
                _myType = null;
            }
        }

        public override void ChangeType(IType iMyType)
        {
            base.ChangeType(iMyType);
            _wasMyTypeSet = true;
        }

        public void ResetMyType()
        {
            _wasMyTypeSet = false;
            if (Connected is null)
            {
                _myType = null;
                return;
            }
            _myType = (Connected as TypeIO).MyType;
        }

        private IType GetMyType()
        {
            if (Connected is not AutoIO autoIO)
            {
                return _wasMyTypeSet ? _myType : (Connected as TypeIO)?.MyType;
            }

            return autoIO._wasMyTypeSet ? autoIO._myType : null;

        }
    }
}

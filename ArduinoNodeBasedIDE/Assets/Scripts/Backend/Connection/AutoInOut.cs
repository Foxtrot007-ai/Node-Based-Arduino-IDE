using Backend.Node;
using Backend.Type;

namespace Backend.Connection
{
    public class AutoInOut : AnyInOut
    {
        
        private bool _wasMyTypeSet = false;
        public override IType MyType => GetMyType();
        public override string InOutName => MyType is null ? "Auto" : base.InOutName;
        public override InOutType InOutType => MyType is null ? InOutType.Auto : base.InOutType;


        public AutoInOut(BaseNode parentNode, InOutSide side, bool isOptional = false) : base(parentNode, side, null, isOptional)
        {
        }

        protected override void Check(InOut input)
        {
            if (!_wasMyTypeSet)
            {
                return;
            }
            
            base.Check(input);
        }

        protected override void BeforeConnectHandler(InOut inOut)   //remove?
        {
            base.BeforeConnectHandler(inOut);
            TypeInOut typeInOut = (TypeInOut)inOut;
            if (!_wasMyTypeSet)
            {
                _myType = typeInOut.MyType;
            }
        }
        
        protected override void AfterDisconnectHandler(InOut inOut) //remove?
        {
            base.AfterDisconnectHandler(inOut);
            if (!_wasMyTypeSet)
            {
                _myType = null;
            }
        }

        public override void ChangeMyType(IType iMyType)
        {
            base.ChangeMyType(iMyType);
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
            _myType = (Connected as TypeInOut).MyType;
        }

        private IType GetMyType()
        {
            if (Connected is not AutoInOut autoInOut)
            {
                return _wasMyTypeSet ? _myType : (Connected as TypeInOut)?.MyType;
            }
            
            return autoInOut._wasMyTypeSet ? autoInOut._myType : null;

        }
    }
}

using System;
using Backend.API;
using Backend.Exceptions;
using Backend.Exceptions.InOut;
using Backend.IO;
using Backend.Node;
using Backend.Type;

namespace Backend.Connection
{
    public class TypeIO : BaseIO
    {
        protected IType _myType;
        public virtual IType MyType => _myType;
        public override IOType IOType => HelperIO.ETypeToInOut(MyType.EType);
        public override string IOName => MyType.TypeName;

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
                if (autoIO.MyType == null)
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
            ReCheck();
        }
    }
}

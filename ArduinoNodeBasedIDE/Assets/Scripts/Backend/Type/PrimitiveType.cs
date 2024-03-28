using Backend.API;
using Backend.Exceptions;

namespace Backend.Type
{
    public class PrimitiveType : IMyType
    {
        public EType EType { get; }
        public string TypeName => EType.ToString().ToLower();

        public static bool IsPrimitive(EType eType)
        {
            return eType is EType.Short or EType.Int or EType.Long or EType.LongLong or EType.Float or EType.Double
                or EType.Bool;
        }

        public PrimitiveType(EType primitiveType)
        {
            if (!IsPrimitive(primitiveType))
            {
                throw new NotPrimitiveTypeException(primitiveType);
            }

            EType = primitiveType;
        }

        protected bool Equals(PrimitiveType other)
        {
            return EType == other.EType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PrimitiveType) obj);
        }

        public static bool operator ==(PrimitiveType left, PrimitiveType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PrimitiveType left, PrimitiveType right)
        {
            return !Equals(left, right);
        }
        
        public override int GetHashCode()
        {
            return (int)EType;
        }
    }
}
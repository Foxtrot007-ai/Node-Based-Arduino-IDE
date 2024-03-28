using Backend.Exceptions.InOut;

namespace Backend.Type
{
    public class PrimitiveType : IType
    {
        public EType GetEType { get; }
        public string TypeName => GetEType.ToString().ToLower();

        public static bool IsPrimitive(EType eType)
        {
            return eType is EType.Short or EType.Int or EType.Long or EType.LongLong or EType.Float or EType.Double
                or EType.Bool;
        }

        public PrimitiveType(EType primitiveType)
        {
            if (!IsPrimitive(primitiveType))
            {
                throw new WrongTypeException();
            }

            GetEType = primitiveType;
        }

        protected bool Equals(PrimitiveType other)
        {
            return GetEType == other.GetEType;
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
    }
}
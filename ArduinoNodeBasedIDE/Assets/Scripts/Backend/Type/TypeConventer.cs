using System;

namespace Backend.Type
{
    public static class TypeConverter
    {
        public static EType ToEType(string type)
        {
            return Enum.TryParse(type, true, out EType eType) ? eType : EType.Class;
        }

        public static IType ToIType(string type)
        {
            var eType = ToEType(type);
            switch (eType)
            {
                case EType.Void:
                    return new VoidType();
                case EType.Class:
                    return new ClassType(type);
                case EType.String:
                    return new StringType();
                case EType.Short:
                case EType.Int:
                case EType.Long:
                case EType.LongLong:
                case EType.Float:
                case EType.Double:
                case EType.Bool:
                    return new PrimitiveType(eType);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

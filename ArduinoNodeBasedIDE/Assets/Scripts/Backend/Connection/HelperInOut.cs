using System;
using Backend.Type;

namespace Backend.Connection
{
    public static class HelperInOut
    {
        public static InOutType ETypeToInOut(EType eType)
        {
            switch (eType)
            {
                case EType.Void:
                    return InOutType.Void;
                case EType.Class:
                    return InOutType.Class;
                case EType.String:
                    return InOutType.String;
                case EType.Short:
                case EType.Int:
                case EType.Long:
                case EType.LongLong:
                case EType.Float:
                case EType.Double:
                case EType.Bool:
                    return InOutType.Primitive;
                default:
                    throw new ArgumentOutOfRangeException(nameof(eType), eType, null);
            }
        }
    }
}

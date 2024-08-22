using System;
using Backend.Type;

namespace Backend.IO
{
    public static class HelperIO
    {
        public static IOType ETypeToInOut(EType eType)
        {
            switch (eType)
            {
                case EType.Void:
                    return IOType.Void;
                case EType.Class:
                    return IOType.Class;
                case EType.String:
                    return IOType.String;
                case EType.Short:
                case EType.Int:
                case EType.Long:
                case EType.LongLong:
                case EType.Float:
                case EType.Double:
                case EType.Bool:
                    return IOType.Primitive;
                default:
                    throw new ArgumentOutOfRangeException(nameof(eType), eType, null);
            }
        }
    }
}

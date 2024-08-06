using System;
using Backend.Type;
using Castle.Core.Internal;

namespace Backend.API.DTO
{
    public record InputNodeValueDto
    {
        public IMyType Type { get; init; }
        public string Value { get; init; }

        public bool IsDtoValid()
        {
            if (Value.IsNullOrEmpty())
                return true;

            switch (Type.EType)
            {
                case EType.Void:
                case EType.Class:
                    return false;
                case EType.String:
                    return true;
                case EType.Short:
                case EType.Int:
                case EType.Long:
                case EType.LongLong:
                    return long.TryParse(Value, out _);
                case EType.Float:
                    return float.TryParse(Value, out _);
                case EType.Double:
                    return double.TryParse(Value, out _);
                case EType.Bool:
                    return bool.TryParse(Value, out _);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

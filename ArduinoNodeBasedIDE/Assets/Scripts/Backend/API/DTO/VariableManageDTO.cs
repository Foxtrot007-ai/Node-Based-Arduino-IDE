using Backend.Type;

namespace Backend.API.DTO
{
    public record VariableManageDto
    {
        public string VariableName { get; init; }
        public IMyType Type { get; init; }

        public bool IsDtoValid()
        {
            return VariableName is not null && Type.EType != EType.Void;
        }
    };
}

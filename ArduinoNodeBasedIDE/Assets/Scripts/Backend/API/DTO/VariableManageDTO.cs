namespace Backend.API.DTO
{
    public record VariableManageDto()
    {
        public string VariableName { get; init; }
        public IMyType Type { get; init; }
    };
}

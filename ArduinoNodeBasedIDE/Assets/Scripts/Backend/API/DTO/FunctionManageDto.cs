using System.Collections.Generic;

namespace Backend.API.DTO
{
    public record FunctionManageDto()
    {
        public string FunctionName { get; init; }
        public IMyType OutputType { get; init; }
    }
}

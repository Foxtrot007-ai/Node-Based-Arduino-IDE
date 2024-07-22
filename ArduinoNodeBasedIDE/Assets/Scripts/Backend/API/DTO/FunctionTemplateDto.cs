using System.Collections.Generic;
using Backend.Type;

namespace Backend.API.DTO
{
    public record FunctionTemplateDto()
    {
        public string FunctionName { get; init; }
        public string Library { get; init; }
        public List<string> InputsType { get; init; }
        public string OutputType { get; init; }
    }
}

using System.Collections.Generic;

namespace Backend.API.DTO
{
    public record FunctionTemplateDto
    {
        public string FunctionName { get; init; }
        public string Library { get; init; }
        public List<string> InputsType { get; init; }
        public string OutputType { get; init; }

        public bool IsDtoValid()
        {
            return !string.IsNullOrEmpty(FunctionName)
                   && !string.IsNullOrEmpty(Library)
                   && InputsType != null
                   && !string.IsNullOrEmpty(OutputType)
                   && InputsType.TrueForAll(x => !string.IsNullOrEmpty(x) && x != "void");
        }
    }
}

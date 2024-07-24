using System.Collections.Generic;
using Backend.Type;
using Castle.Core.Internal;

namespace Backend.API.DTO
{
    public record FunctionTemplateDto()
    {
        public string FunctionName { get; init; }
        public string Library { get; init; }
        public List<string> InputsType { get; init; }
        public string OutputType { get; init; }

        public bool IsDtoValid()
        {
            return !FunctionName.IsNullOrEmpty()
                   && !Library.IsNullOrEmpty()
                   && InputsType != null
                   && !OutputType.IsNullOrEmpty()
                   && InputsType.TrueForAll(x => !x.IsNullOrEmpty() && x != "void");
        }
    }
}

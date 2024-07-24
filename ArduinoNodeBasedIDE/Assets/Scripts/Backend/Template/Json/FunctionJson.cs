using System.Collections.Generic;
using Backend.API.DTO;

namespace Backend.Template.Json
{
    public class FunctionJson
    { 
        public string Name { get; set; }
        public List<string> InputsType { get; set; }
        public string OutputType { get; set; }

        public FunctionJson(){}
        public FunctionJson(FunctionTemplateDto functionTemplateDto)
        {
            Name = functionTemplateDto.FunctionName;
            InputsType = functionTemplateDto.InputsType;
            OutputType = functionTemplateDto.OutputType;
        }
    }
}

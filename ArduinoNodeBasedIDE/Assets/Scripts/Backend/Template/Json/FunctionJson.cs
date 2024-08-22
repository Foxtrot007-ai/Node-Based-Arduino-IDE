using System.Collections.Generic;

namespace Backend.Template.Json
{
    public class FunctionJson
    {
        public string Name { get; set; }
        public List<string> InputsType { get; set; }
        public string OutputType { get; set; }
    }
}

using System.Collections.Generic;

namespace Backend.Template.Json
{
    public class FunctionsJson
    {
        public string Library { get; set; }
        public Dictionary<long, FunctionJson> Functions { get; set; } = new();
    }
}

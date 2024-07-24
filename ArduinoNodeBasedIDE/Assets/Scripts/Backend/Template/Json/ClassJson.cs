using System.Collections.Generic;

namespace Backend.Template.Json
{
    public class ClassJson
    {
        public string ClassName { get; set; }
        public string Library { get; set; }
        public Dictionary<long, List<string>> Constructors { get; set; } = new();
        public Dictionary<long, FunctionJson> Methods { get; set; } = new();
    }
}

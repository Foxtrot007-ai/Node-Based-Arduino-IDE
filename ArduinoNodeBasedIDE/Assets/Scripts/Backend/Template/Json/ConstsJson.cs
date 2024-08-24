using System.Collections.Generic;

namespace Backend.Template.Json
{
    public class ConstsJson
    {

        public string Library { get; set; }
        public Dictionary<long, ConstJson> Consts { get; set; } = new();
    }
}

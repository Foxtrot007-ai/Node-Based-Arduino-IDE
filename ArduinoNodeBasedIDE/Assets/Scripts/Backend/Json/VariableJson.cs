using Backend.Variables;

namespace Backend.Json
{
    public class VariableJson
    {
        public string PathName { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public VariableJson()
        {
        }
        public VariableJson(Variable variable)
        {
            PathName = variable.PathName.ToString();
            Name = variable.Name;
            Type = variable.Type.TypeName;
        }
    }
}

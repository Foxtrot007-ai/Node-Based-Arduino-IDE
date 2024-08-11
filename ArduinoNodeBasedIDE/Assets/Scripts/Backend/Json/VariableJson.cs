using Backend.Variables;
using System;

namespace Backend.Json
{
    [Serializable]
    public class VariableJson
    {
        public string PathName;
        public string Name;
        public string Type;

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

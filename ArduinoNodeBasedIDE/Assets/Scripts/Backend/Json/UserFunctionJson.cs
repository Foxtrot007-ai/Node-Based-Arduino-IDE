using System.Collections.Generic;
using Backend.MyFunction;

namespace Backend.Json
{
    public class UserFunctionJson
    {
        public string PathName { get; set; }
        public string Name { get; set; }
        public string OutputType { get; set; }
        public List<VariableJson> LocalVariables { get; set; } = new();
        public List<VariableJson> ParamVariables { get; set; } = new();

        public UserFunctionJson()
        {
        }
        public UserFunctionJson(UserFunction userFunction)
        {
            PathName = userFunction.PathName.ToString();
            Name = userFunction.Name;
            OutputType = userFunction.OutputType.TypeName;
            LocalVariables = JsonHelper.VariablesToJson(userFunction.Variables);
            ParamVariables = JsonHelper.VariablesToJson(userFunction.InputList);
        }
    }
}

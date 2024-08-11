using System;
using System.Collections.Generic;
using Backend.MyFunction;

namespace Backend.Json
{
    [Serializable]
    public class UserFunctionJson
    {
        public string PathName;
        public string Name;
        public string OutputType;
    
        public List<VariableJson> LocalVariables = new();
        public List<VariableJson> ParamVariables = new();

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

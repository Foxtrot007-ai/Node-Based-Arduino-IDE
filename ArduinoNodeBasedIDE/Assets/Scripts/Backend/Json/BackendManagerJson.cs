using System;
using System.Collections.Generic;
using System.Linq;
using Backend.MyFunction;

namespace Backend.Json
{
    [Serializable]
    public class BackendManagerJson
    {
        public List<VariableJson> GlobalVariables = new();
        public List<VariableJson> SetupVariables = new();
        public List<VariableJson> LoopVariables  = new();
        public List<UserFunctionJson> UserFunctions = new();

        public BackendManagerJson()
        {
        }
        public BackendManagerJson(BackendManager backendManager)
        {
            GlobalVariables = JsonHelper.VariablesToJson(backendManager.GlobalVariables);
            SetupVariables = JsonHelper.VariablesToJson(backendManager.Setup.Variables);
            LoopVariables = JsonHelper.VariablesToJson(backendManager.Loop.Variables);
            UserFunctions = backendManager
                .Functions
                .Functions
                .Select(fun => new UserFunctionJson((UserFunction)fun))
                .ToList();
        }
    }
}

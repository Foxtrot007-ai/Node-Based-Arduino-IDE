using System.Collections.Generic;
using System.Linq;
using Backend.MyFunction;

namespace Backend.Json
{
    public class BackendManagerJson
    {
        public List<VariableJson> GlobalVariables { get; set; } = new();
        public List<VariableJson> SetupVariables { get; set; } = new();
        public List<VariableJson> LoopVariables { get; set; } = new();
        public List<UserFunctionJson> UserFunctions { get; set; } = new();

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

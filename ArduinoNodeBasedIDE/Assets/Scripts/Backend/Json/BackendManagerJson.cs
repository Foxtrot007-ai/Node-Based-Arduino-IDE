using System.Collections.Generic;
using System.Linq;
using Backend.Function;

namespace Backend.Json
{
    public class BackendManagerJson
    {
        public List<VariableJson> GlobalVariables { get; set; } = new();
        public List<VariableJson> StartVariables { get; set; } = new();
        public List<VariableJson> LoopVariables { get; set; } = new();
        public List<UserFunctionJson> UserFunctions { get; set; } = new();

        public BackendManagerJson()
        {
        }
        public BackendManagerJson(BackendManager backendManager)
        {
            GlobalVariables = JsonHelper.VariablesToJson(backendManager.GlobalVariables);
            StartVariables = JsonHelper.VariablesToJson(backendManager.Start.Variables);
            LoopVariables = JsonHelper.VariablesToJson(backendManager.Loop.Variables);
            UserFunctions = backendManager
                .Functions
                .Functions
                .Select(fun => new UserFunctionJson((UserFunction)fun))
                .ToList();
        }
    }
}

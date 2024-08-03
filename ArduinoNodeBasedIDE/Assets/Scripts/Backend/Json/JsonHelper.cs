using System.Collections.Generic;
using System.Linq;
using Backend.API;
using Backend.Variables;

namespace Backend.Json
{
    public static class JsonHelper
    {
        public static List<VariableJson> VariablesToJson(IVariablesManager variablesManager)
        {
            return variablesManager.Variables
                .Select(variable => new VariableJson((Variable)variable))
                .ToList();
        }
    }
}

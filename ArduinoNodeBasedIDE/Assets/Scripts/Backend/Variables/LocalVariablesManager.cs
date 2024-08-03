using System.Collections.Generic;
using Backend.Json;

namespace Backend.Variables
{
    public class LocalVariablesManager : VariablesManager
    {

        private Function.Function _functionManager;

        public LocalVariablesManager(Function.Function functionManager, List<VariableJson> variableJsons) : base(variableJsons)
        {
            _myPnStr = "LOCAL_VAR";
            _functionManager = functionManager;
        }

        public LocalVariablesManager(Function.Function functionManager, PathName parentPn) : base(parentPn)
        {
            _myPnStr = "LOCAL_VAR";
            _functionManager = functionManager;
        }

        public override bool IsVariableDuplicate(string name)
        {
            return _functionManager.IsVariableDuplicate(name);
        }
    }
}

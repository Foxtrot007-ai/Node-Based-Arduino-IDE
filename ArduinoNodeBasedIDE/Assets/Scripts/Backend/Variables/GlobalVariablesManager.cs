using System.Collections.Generic;
using System.Linq;
using Backend.API;
using Backend.Function;
using Backend.Json;

namespace Backend.Variables
{
    public class GlobalVariablesManager : VariablesManager
    {
        private IBackendManager _backendManager;

        public GlobalVariablesManager(IBackendManager backendManager, List<VariableJson> variableJsons) : base(variableJsons)
        {
            _myPnStr = "GLOBAL_VAR";
            _backendManager = backendManager;
        }

        public GlobalVariablesManager(IBackendManager backendManager, PathName parentPn) : base(parentPn)
        {
            _myPnStr = "GLOBAL_VAR";
            _backendManager = backendManager;
        }

        public override bool IsVariableDuplicate(string name)
        {
            return IsDuplicateName(name)
                   || ((Function.Function)_backendManager.Start).IsVariableLocalDuplicate(name)
                   || ((Function.Function)_backendManager.Loop).IsVariableLocalDuplicate(name)
                   || _backendManager.Functions.Functions.Any(fun => ((UserFunction)fun).IsVariableLocalDuplicate(name));
        }
    }
}

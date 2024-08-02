using System.Linq;
using Backend.API;
using Backend.Function;

namespace Backend.Variables
{
    public class GlobalVariablesManager : VariablesManager
    {
        private IBackendManager _backendManager;

        public GlobalVariablesManager(IBackendManager backendManager)
        {
            _backendManager = backendManager;
        }

        protected override bool IsVariableDuplicate(string name)
        {
            return IsDuplicateName(name)
                   || ((Function.Function)_backendManager.Start).IsVariableLocalDuplicate(name)
                   || ((Function.Function)_backendManager.Loop).IsVariableLocalDuplicate(name)
                   || _backendManager.Functions.Functions.Any(fun => ((UserFunction)fun).IsVariableLocalDuplicate(name));
        }
    }
}

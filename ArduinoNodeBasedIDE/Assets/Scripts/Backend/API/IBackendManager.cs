using System.Collections.Generic;
using Backend.Json;

namespace Backend.API
{
    public interface IBackendManager
    {
        /*
         * Setup function
         */
        public IFunction Setup { get; }

        /*
         * Loop function
         */
        public IFunction Loop { get; }

        /*
         * Global variables manager
         */
        public IVariablesManager GlobalVariables { get; }

        /*
         * User functions manager
         */
        public IUserFunctionsManager Functions { get; }

        /*
         * Templates manager
         */
        public ITemplatesManager Templates { get; }

        /*
         * Instance creator
         */
        public IInstanceCreator InstanceCreator { get; }
        
        /*
         * Available types
         */
        public List<IMyType> Types { get; }

        /*
         * Build code
         *
         * code will be set in savePath
         * programPath will be add in header of generated file
         * Might throw:
         *
         * IOMustBeConnected
         * VariableNotSet
         */
        public void BuildCode(string savePath, string programPath);
        
        /*
         * Will save globalVariable, function in json
         */
        public BackendManagerJson Save();
        
        /*
         * Will reset to default value and then load globalVariable, functions from json
         */
        public void Load(BackendManagerJson json);

        public void Clear();
    }
}

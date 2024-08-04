using System.Collections.Generic;

namespace Backend.API
{
    public interface IBackendManager
    {
        /*
         * Start function
         */
        public IFunction Start { get; }

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
         * Might throw:
         *
         * IOMustBeConnected
         * VariableNotSet
         */
        public void BuildCode(string path);
        
        /*
         * Save code
         * Will save globalVariable, function in file
         */
        public void Save(string path);
        
        /*
         * Load code
         * Will reset to default value and then load globalVariable, functions from file
         */
        public void Load(string path);

        public void Clear();
    }
}

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
         * Available types
         */
        public List<IMyType> Types { get; }

        /*
         * Build code
         */
        public void BuildCode();
    }
}

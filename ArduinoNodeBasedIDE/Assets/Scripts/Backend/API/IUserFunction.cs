using Backend.API.DTO;

namespace Backend.API
{
    public interface IUserFunction : IFunction
    {
        /*
         * Function outputType
         * Void if no output
         */
        public IMyType OutputType { get; }

        /*
         * List of input params
         * Empty list if 0 params
         * Param manage same as Variable
         */
        public IVariablesManager InputList { get; }

        /*
         * Local variables
         */
        public IVariablesManager VariablesManager { get; }
        
        /*
         * Change Name or Type of function
         * null if not change/or same value
         *
         * Validation for name change
         * For OutputType change when output is connected:
         *   if cannot be cast or need adapter:
         *      disconnect
         *      throw exception
         *   else
         *     do nothing (connection still exists)
         */

        public void Change(FunctionManageDto functionManageDto);

        /*
         * Create new instance of function
         */
        public INode CreateNode();

        /*
         * LogicDelete all instance of function (in other views)
         * LogicDelete all NodeBlock in function
         */
        public void Delete();
    }
}

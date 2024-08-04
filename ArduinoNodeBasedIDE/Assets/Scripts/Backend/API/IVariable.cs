using Backend.API.DTO;

namespace Backend.API
{
    public interface IVariable
    {
        public string Id { get; }

        /*
         * Return name of variable
         */
        public string Name { get; }

        /*
         * Return type of variable
         * Type cannot be Void
         */
        public IMyType Type { get; }

        /*
         * Change Name or Type of variable
         *
         * Validation for name change
         * For Type change when variable is connected:
         *   if cannot be cast or need adapter:
         *      disconnect
         *      throw exception
         *   else
         *     do nothing (connection still exists)
         */
        public void Change(VariableManageDto variableManageDto);

        /*
         * Create new instance of variable getter
         *
         * Might throw:
         *  InvalidVariableManageDto
         *  CannotBeCast
         */
        public INode CreateGetNode();

        /*
         * Create new instance of variable setter
         */
        public INode CreateSetNode();

        /*
         * Delete all instance of variable
         * (backend will set logic delete to true)
         */
        public void Delete();

        /*
         * If true IConnection is logicalDelete, need physical delete
         */
        public bool IsDelete { get; }
    }
}

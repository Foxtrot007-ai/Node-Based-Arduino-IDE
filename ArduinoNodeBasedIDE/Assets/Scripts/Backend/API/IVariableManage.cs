using Backend.API.DTO;

namespace Backend.API
{
    public interface IVariableManage
    {
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
         * null if not change/or same value
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
         * Create new instance of variable
         * Might not be INode need to rethink (maybe split to set and get?)
         */
        public INode CreateVariable();
        
        /*
         * Delete all instance of variable
         * (backend will set logic delete to true)
         */
        public void DeleteVariable();
    }
}

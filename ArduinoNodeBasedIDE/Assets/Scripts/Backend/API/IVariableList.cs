using System.Collections.Generic;
using Backend.API.DTO;

namespace Backend.API
{
    public interface IVariableList
    {
        /*
         * Get all VariablesManage as list !!!readOnly!!!
         */
        public List<IVariableManage> VariableManages { get; }
        
        /*
         * TO REMOVE
         */
        public void AddVariable(IVariableManage variableManage);
        
        /*
         * Add new variable to list
         * Will make validation like if name is valid
         */
        public IVariableManage AddVariable(VariableManageDto variableManageDto);
        
        /*
         * Find variable on list and call delete
         */
        public void DeleteVariable(IVariableManage variableManage);
    }
}

using System.Collections.Generic;
using Backend.API.DTO;

namespace Backend.API
{
    public interface IVariablesManager
    {
        /*
         * Get all VariablesManage as list !!!readOnly!!!
         */
        public List<IVariable> Variables { get; }

        /*
         * Add new variable to list
         * Will make validation like if name is valid
         *
         * Might throw:
         *  InvalidVariableManageDto
         */
        public IVariable AddVariable(VariableManageDto variableManageDto);

        /*
         * Find variable on list and call delete
         */
        public void DeleteVariable(IVariable variable);
    }
}

using System.Collections.Generic;
using Backend.API.DTO;

namespace Backend.API
{
    public interface IUserFunctionsManager
    {
        /*
         * Get all IUserFunction as list !!!readOnly!!!
         */
        public List<IUserFunction> Functions { get; }

        /*
         * Add new user function to list
         * Will make validation like if name is valid
         *
         * Will create startNode
         *
         * Might throw:
         *  InvalidFunctionManageDto
         */
        public IUserFunction AddFunction(FunctionManageDto functionManageDto);

        /*
         * Find function on list and call delete
         */
        public void DeleteFunction(IUserFunction functionManage);
    }
}

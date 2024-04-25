using System.Collections.Generic;
using Backend.API.DTO;

namespace Backend.API
{
    public interface IFunctionList
    {
        /*
         * Get all IFunctionManage as list !!!readOnly!!!
         */
        public List<IFunctionManage> FunctionManages { get; }
        
        /*
         * Add new function to list
         * Will make validation like if name is valid
         *
         * Will create startNode
         */
        public IFunctionManage AddFunction(FunctionManageDto functionManageDto);
        
        /*
         * Find function on list and call delete
         */
        public void DeleteFunction(IFunctionManage functionManage);
    }
}

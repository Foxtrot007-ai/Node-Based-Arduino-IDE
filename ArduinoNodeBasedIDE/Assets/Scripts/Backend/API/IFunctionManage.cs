using Backend.API.DTO;

namespace Backend.API
{
    public interface IFunctionManage
    {
        
        public string Name { get; }
        public IMyType OutputType { get; }
        public IVariableList InputList { get; }
        
        public void Change(FunctionManageDto functionManageDto);
        public INode CreateFunction(); // Create new instance of function
        public void DeleteFunction(); // Delete all instance of function
    }
}

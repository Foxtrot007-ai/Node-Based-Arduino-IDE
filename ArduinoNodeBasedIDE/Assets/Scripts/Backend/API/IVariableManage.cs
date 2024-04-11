using Backend.API.DTO;

namespace Backend.API
{
    public interface IVariableManage
    {
        public string Name { get; }
        public IMyType Type { get; }
        public void Change(VariableManageDto variableManageDto);

        public INode CreateVariable(); // Create new instance of variable
        public void DeleteVariable(); // Delete all instance of variable
    }
}

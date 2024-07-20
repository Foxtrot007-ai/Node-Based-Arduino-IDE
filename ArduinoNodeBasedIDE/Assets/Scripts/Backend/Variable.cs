using Backend.API;
using Backend.API.DTO;

namespace Backend
{
    public class Variable : IVariableManage
    {

        public string Name { get; }
        public IMyType Type { get; }
        public void Change(VariableManageDto variableManageDto)
        {
            throw new System.NotImplementedException();
        }
        public INode CreateGetVariable()
        {
            throw new System.NotImplementedException();
        }
        public INode CreateSetVariable()
        {
            throw new System.NotImplementedException();
        }
        public void DeleteVariable()
        {
            throw new System.NotImplementedException();
        }
    }
}

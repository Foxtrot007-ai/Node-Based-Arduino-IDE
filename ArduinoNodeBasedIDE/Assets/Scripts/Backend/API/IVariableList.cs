using System.Collections.Generic;

namespace Backend.API
{
    public interface IVariableList
    {
        public List<IVariableManage> VariableManages { get; }
        public void AddVariable(IVariableManage variableManage);
    }
}

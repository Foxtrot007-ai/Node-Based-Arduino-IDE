using System.Collections.Generic;
using Backend.API;
using Backend.API.DTO;
using Backend.Exceptions;
using Backend.Type;

namespace Backend
{
    public class VariablesManager : IVariablesManager
    {
        public List<IVariable> Variables { get; } = new();

        public virtual IVariable AddVariable(VariableManageDto variableManageDto)
        {
            if (variableManageDto.VariableName is null || !IsDtoValid(variableManageDto))
            {
                throw new InvalidVariableManageDto();
            }

            return new Variable(this, variableManageDto);
        }

        public virtual void DeleteVariable(IVariable variableManage)
        {
            var variable = (Variable)variableManage;
            Variables.Find(x => x == variable)?.Delete();
        }

        public virtual bool IsDtoValid(VariableManageDto variableManageDto)
        {
            if (variableManageDto.Type.EType == EType.Void)
            {
                return false;
            }

            var valid = Variables.Exists(x => x.Name == variableManageDto.VariableName);
            return !valid;
        }

        public virtual void AddRef(IVariable variable)
        {
            Variables.Add(variable);
        }

        public virtual void DeleteRef(IVariable variable)
        {
            Variables.Remove(variable);
        }
    }
}

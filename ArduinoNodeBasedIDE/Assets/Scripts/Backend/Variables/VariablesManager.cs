using System.Collections.Generic;
using Backend.API;
using Backend.API.DTO;
using Backend.Exceptions;

namespace Backend.Variables
{
    public abstract class VariablesManager : IVariablesManager
    {
        public virtual List<IVariable> Variables { get; } = new();

        public virtual IVariable AddVariable(VariableManageDto variableManageDto)
        {
            if (!variableManageDto.IsDtoValid() || IsVariableDuplicate(variableManageDto.VariableName))
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

        protected abstract bool IsVariableDuplicate(string name);
        
        public virtual bool IsDuplicateName(string name)
        {
            return Variables.Exists(x => x.Name == name);
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

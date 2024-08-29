using System.Collections.Generic;
using Backend.API;
using Backend.API.DTO;
using Backend.Exceptions;
using Backend.Json;

namespace Backend.Variables
{
    public abstract class VariablesManager : IVariablesManager
    {
        public virtual List<IVariable> Variables { get; } = new();
        private PathName _parentPn;
        protected string _myPnStr;
        private long _highestId = 0;

        protected VariablesManager()
        {
        }

        protected VariablesManager(List<VariableJson> variableJsons)
        {
            foreach (var variableJson in variableJsons)
            {
                var variable = new Variable(this, variableJson);
                var id = variable.PathName.GetId();
                if (id > _highestId)
                {
                    _highestId = id;
                }
            }
        }
        protected VariablesManager(PathName parentPn)
        {
            _parentPn = parentPn;
        }

        public virtual IVariable AddVariable(VariableManageDto variableManageDto)
        {
            if (!variableManageDto.IsDtoValid() || IsVariableDuplicate(variableManageDto.VariableName))
            {
                throw new InvalidVariableManageDto();
            }

            _highestId++;
            return new Variable(this, variableManageDto, new PathName(_parentPn, _myPnStr, _highestId));
        }

        public virtual void DeleteVariable(IVariable variableManage)
        {
            var variable = (Variable)variableManage;
            Variables.Find(x => x == variable)?.Delete();
        }

        public abstract bool IsVariableDuplicate(string name);

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

        public virtual void ChangeNotify(IVariable variable)
        {

        }

        public virtual IVariable GetVariableByPn(PathName pathName)
        {
            return Variables.Find(variable => ((Variable)variable).PathName.Equals(pathName));
        }
    }
}

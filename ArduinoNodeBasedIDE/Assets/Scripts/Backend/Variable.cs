using System.Collections.Generic;
using Backend.API;
using Backend.API.DTO;
using Backend.Exceptions;
using Backend.Node.BuildIn;
using Backend.Type;

namespace Backend
{
    public class Variable : IVariable
    {
        public virtual string Name { get; private set; }
        public virtual IType Type { get; private set; }
        IMyType IVariable.Type => Type;
        private List<VariableNode> _refs = new();
        private readonly VariablesManager _variablesManager;

        public Variable(VariablesManager variablesManager, VariableManageDto variableManageDto)
        {
            _variablesManager = variablesManager;
            Name = variableManageDto.VariableName;
            Type = (IType)variableManageDto.Type;
            _variablesManager.AddRef(this);
        }

        public virtual void Change(VariableManageDto variableManageDto)
        {
            if (!_variablesManager.IsDtoValid(variableManageDto))
            {
                throw new InvalidVariableManageDto();
            }

            var newName = variableManageDto.VariableName;
            if (newName is not null && !newName.Equals(Name))
            {
                Name = newName;
            }

            var newType = (IType)variableManageDto.Type;
            if (Type == newType) return;
            Type = newType;
            _refs.ForEach(x => x.ChangeType(newType));
        }

        public INode CreateGetNode()
        {
            return new GetVariableNode(this);
        }

        public INode CreateSetNode()
        {
            return new SetVariableNode(this);
        }

        public virtual void Delete()
        {
            _refs.ForEach(x => x.Delete());
            _variablesManager.DeleteRef(this);
        }

        public virtual void AddRef(VariableNode node)
        {
            _refs.Add(node);
        }

        public virtual void DeleteRef(VariableNode node)
        {
            _refs.Remove(node);
        }
    }
}

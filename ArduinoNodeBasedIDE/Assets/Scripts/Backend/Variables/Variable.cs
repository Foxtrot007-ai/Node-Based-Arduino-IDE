using System.Collections.Generic;
using System.Linq;
using Backend.API;
using Backend.API.DTO;
using Backend.Exceptions;
using Backend.Json;
using Backend.Node.BuildIn;
using Backend.Type;

namespace Backend.Variables
{
    public class Variable : IVariable
    {
        public string Id => PathName.ToString();
        public virtual string Name { get; private set; }
        public virtual IType Type { get; private set; }
        public bool IsDelete { get; protected set; }
        IMyType IVariable.Type => Type;
        private List<VariableNode> _refs = new();
        private readonly VariablesManager _variablesManager;
        public PathName PathName { get; }
        protected Variable()
        {
        }

        public Variable(VariablesManager variablesManager, VariableJson json)
        {
            _variablesManager = variablesManager;
            PathName = new PathName(json.PathName);
            Name = json.Name;
            Type = TypeConverter.ToIType(json.Type);
            _variablesManager.AddRef(this);
        }

        public Variable(VariablesManager variablesManager, VariableManageDto variableManageDto, PathName pathName)
        {
            _variablesManager = variablesManager;
            PathName = pathName;
            Name = variableManageDto.VariableName;
            Type = (IType)variableManageDto.Type;
            _variablesManager.AddRef(this);
        }

        public virtual void Change(VariableManageDto variableManageDto)
        {
            var newName = variableManageDto.VariableName;
            if (!variableManageDto.IsDtoValid() || (newName != Name && _variablesManager.IsVariableDuplicate(newName)))
            {
                throw new InvalidVariableManageDto();
            }

            Name = newName;
            var newType = (IType)variableManageDto.Type;
            if (Type == newType) return;
            Type = newType;
            _refs.ForEach(x => x.ChangeType(newType));
        }

        public INode CreateGetNode()
        {
            return new GetVariableNode(this, new PathName(PathName, "GET").ToString());
        }

        public INode CreateSetNode()
        {
            return new SetVariableNode(this, new PathName(PathName, "SET").ToString());
        }

        public virtual void Delete()
        {
            IsDelete = true;
            foreach (var node in _refs.ToList())
            {
                node.Delete();
            }
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

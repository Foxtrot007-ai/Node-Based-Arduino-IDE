using System.Collections.Generic;
using System.Linq;
using Backend.API;
using Backend.API.DTO;
using Backend.Json;
using Backend.Node;
using Backend.Type;
using Backend.Variables;

namespace Backend.MyFunction
{
    public class UserFunction : Function, IUserFunction
    {
        public virtual IVariablesManager InputList => _paramsManager;
        private UserFunctionManager _manager;
        private List<UserFunctionNode> _refs = new();
        private ParamsManager _paramsManager;

        protected UserFunction()
        {
        }

        public UserFunction(UserFunctionManager manager, IBackendManager backendManager, UserFunctionJson userFunctionJson) : base(
            backendManager,
            userFunctionJson.Name,
            new PathName(userFunctionJson.PathName),
            userFunctionJson.LocalVariables)
        {
            _manager = manager;
            OutputType = TypeConverter.ToIType(userFunctionJson.OutputType);
            _paramsManager = new ParamsManager(this, userFunctionJson.ParamVariables);
            _manager.AddRef(this);
        }

        public UserFunction(UserFunctionManager manager, IBackendManager backendManager, PathName pathName, FunctionManageDto functionManageDto) : base(
            backendManager,
            functionManageDto.FunctionName,
            pathName)
        {
            _manager = manager;
            _paramsManager = new ParamsManager(this, PathName);
            OutputType = functionManageDto.OutputType;
            _backendManager = backendManager;
            _manager.AddRef(this);
        }

        public void Change(FunctionManageDto functionManageDto)
        {
            if (Name != functionManageDto.FunctionName) 
            {
                _manager.Validate(functionManageDto);
            }

            Name = functionManageDto.FunctionName;

            if (OutputType.Equals(functionManageDto.OutputType)) return;
            
            OutputType = functionManageDto.OutputType;
            _refs.ForEach(node => node.ChangeOutputType(OutputType));
            _returnRefs.ForEach(node => node.ChangeInputType(OutputType));
        }

        public override bool IsVariableLocalDuplicate(string name)
        {
            return base.IsVariableLocalDuplicate(name) || _paramsManager.IsDuplicateName(name);
        }

        public INode CreateNode()
        {
            return new UserFunctionNode(this, PathName.ToString());
        }

        public void Delete()
        {
            IsDelete = true;
            foreach (var node in _refs.ToList())
            {
                node.Delete();
            }
            _manager.DeleteRef(this);
        }

        public void AddRef(UserFunctionNode node)
        {
            _refs.Add(node);
        }

        public void DeleteRef(UserFunctionNode node)
        {
            _refs.Remove(node);
        }

        public virtual void AddInOut(IVariable variable)
        {
            _refs.ForEach(node => node.AddParam(variable));
        }

        public virtual void DeleteInOut(int index)
        {
            _refs.ForEach(node => node.RemoveParam(index));
        }

        protected override string CreateInputs()
        {
            var inputs = InputList
                .Variables
                .Select(variable => $"{((IType)variable.Type).ToCode()} {variable.Name}");
            return string.Join(", ", inputs);
        }

        protected override CodeManager CreateCodeManager(CodeManager codeManager)
        {
            var clonedCodeManager = new CodeManager(codeManager);
            InputList.Variables
                .ForEach(variable => clonedCodeManager.SetVariableStatus(variable, CodeManager.VariableStatus.Param));
            return clonedCodeManager;
        }
    }
}

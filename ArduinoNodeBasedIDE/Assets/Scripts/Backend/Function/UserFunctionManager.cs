using System.Collections.Generic;
using Backend.API;
using Backend.API.DTO;
using Backend.Exceptions;

namespace Backend.Function
{
    public class UserFunctionManager : IUserFunctionsManager
    {
        public List<IUserFunction> Functions { get; } = new();
        private IBackendManager _backendManager;

        protected UserFunctionManager()
        {
        }

        public UserFunctionManager(IBackendManager backendManager)
        {
            _backendManager = backendManager;
        }

        public IUserFunction AddFunction(FunctionManageDto functionManageDto)
        {
            Validate(functionManageDto);
            return new UserFunction(this, _backendManager, functionManageDto);
        }

        public void DeleteFunction(IUserFunction functionManage)
        {
            DeleteRef(functionManage);
        }

        public virtual void AddRef(IUserFunction userFunction)
        {
            Functions.Add(userFunction);
        }

        public virtual void DeleteRef(IUserFunction userFunction)
        {
            Functions.Remove(userFunction);
        }

        public virtual void Validate(FunctionManageDto functionManageDto)
        {
            var function = GetWithSameName(functionManageDto.FunctionName);
            if (function.Count == 0
                || !function.Exists(x => x.InputList.Variables.Count == 0))
                return;

            throw new InvalidFunctionManageDto();
        }

        private List<IUserFunction> GetWithSameName(string name)
        {
            return Functions.FindAll(fun => fun.Name == name);
        }
    }
}

using System.Collections.Generic;
using Backend.API;
using Backend.API.DTO;
using Backend.Exceptions;
using Backend.Json;

namespace Backend.Function
{
    public class UserFunctionManager : IUserFunctionsManager
    {
        public List<IUserFunction> Functions { get; } = new();
        private IBackendManager _backendManager;
        private long _highestId = 0;
        private PathName _parentDn = new("ROOT-1");
        protected UserFunctionManager()
        {
        }

        public UserFunctionManager(IBackendManager backendManager, List<UserFunctionJson> userFunctionJsons) : this(backendManager)
        {
            foreach (var userFunctionJson in userFunctionJsons)
            {
                var function = new UserFunction(this, _backendManager, userFunctionJson);
                var id = function.PathName.GetId();
                if (id > _highestId)
                {
                    _highestId = id;
                }
            }
        }

        public UserFunctionManager(IBackendManager backendManager)
        {
            _backendManager = backendManager;
        }

        public IUserFunction AddFunction(FunctionManageDto functionManageDto)
        {
            Validate(functionManageDto);
            _highestId++;
            return new UserFunction(this,
                                    _backendManager,
                                    new PathName(_parentDn, "USER_FUNCTION", _highestId),
                                    functionManageDto);
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

        public virtual UserFunction GetFunctionByPn(PathName pathName)
        {
            return (UserFunction)Functions.Find(fun => ((UserFunction)fun).PathName.GetId() == pathName.GetId());
        }
    }
}

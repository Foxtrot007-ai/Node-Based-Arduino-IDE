using System.Collections.Generic;
using Backend.API;
using Backend.API.DTO;
using Backend.Json;
using Backend.MyFunction;

namespace Backend.Variables
{
    public class ParamsManager : VariablesManager
    {
        private UserFunction _userFunction;

        protected ParamsManager()
        {
        }

        public ParamsManager(UserFunction userFunction, List<VariableJson> variableJsons) : base(variableJsons)
        {
            _myPnStr = "PARAM_VAR";
            _userFunction = userFunction;
        }

        public ParamsManager(UserFunction userFunction, PathName parentPn) : base(parentPn)
        {
            _myPnStr = "PARAM_VAR";
            _userFunction = userFunction;
        }

        public override IVariable AddVariable(VariableManageDto variableManageDto)
        {
            var variable = (Variable)base.AddVariable(variableManageDto);
            _userFunction.AddInOut(variable);
            return variable;
        }

        public override void ChangeNotify(IVariable variable)
        {
            int index = Variables.IndexOf(variable);
            _userFunction.ChangeInOutType(index, variable);
        }

        public override bool IsVariableDuplicate(string name)
        {
            return _userFunction.IsVariableDuplicate(name);
        }

        public override void DeleteRef(IVariable variable)
        {
            var index = Variables.IndexOf(variable);
            _userFunction.DeleteInOut(index);
            base.DeleteRef(variable);
        }
    }
}

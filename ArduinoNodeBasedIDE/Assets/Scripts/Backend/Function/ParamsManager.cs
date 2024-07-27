using System.Collections.Generic;
using Backend.API;
using Backend.API.DTO;

namespace Backend.Function
{
    public class ParamsManager : VariablesManager
    {
        private UserFunction _userFunction;

        protected ParamsManager()
        {
        }
        public ParamsManager(UserFunction userFunction)
        {
            _userFunction = userFunction;
        }
        public override IVariable AddVariable(VariableManageDto variableManageDto)
        {
            var variable = (Variable)base.AddVariable(variableManageDto);
            _userFunction.AddInOut(variable);
            return variable;
        }
        public override void DeleteRef(IVariable variable)
        {
            var index = Variables.IndexOf(variable);
            _userFunction.DeleteInOut(index);
            base.DeleteRef(variable);
        }
    }
}

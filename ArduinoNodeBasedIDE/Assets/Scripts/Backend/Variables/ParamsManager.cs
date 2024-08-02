using Backend.API;
using Backend.API.DTO;
using Backend.Function;

namespace Backend.Variables
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
        
        protected override bool IsVariableDuplicate(string name)
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

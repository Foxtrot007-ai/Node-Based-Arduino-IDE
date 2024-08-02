namespace Backend.Variables
{
    public class LocalVariablesManager : VariablesManager
    {

        private Function.Function _functionManager;

        public LocalVariablesManager(Function.Function functionManager)
        {
            _functionManager = functionManager;
        }

        protected override bool IsVariableDuplicate(string name)
        {
            return _functionManager.IsVariableDuplicate(name);
        }
    }
}

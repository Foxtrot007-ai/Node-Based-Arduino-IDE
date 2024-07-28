using System.Collections.Generic;
using Backend.API;
using Backend.Function;
using Backend.Type;
using Backend.Validator;

namespace Backend
{
    public class Startup : IBackendManager
    {
        public IFunction Start { get; } = new Function.Function("start");
        public IFunction Loop { get; } = new Function.Function("loop");
        public IVariablesManager GlobalVariables { get; } = new VariablesManager();
        public IUserFunctionsManager Functions { get; } = new UserFunctionManager();
        public ITemplatesManager Templates { get; } = new TemplateManager();
        public IInstanceCreator InstanceCreator { get; }
        public List<IMyType> Types { get; } = new();

        public Startup()
        {
            foreach (var classType in ClassTypeValidator.Instance.GetAllClassTypes())
            {
                Types.Add(new ClassType(classType));
            }
        }

        public void BuildCode()
        {
            /*
             * Todo after refactor
             * Create includes
             * GlobalVariables
             * function declarations ??
             * functions with body
             * start
             * loop
             */
        }
        public void Save(string path)
        {
            throw new System.NotImplementedException();
        }
        public void Load(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}

using System.Collections.Generic;
using Backend.API;
using Backend.Function;
using Backend.Type;
using Backend.Validator;
using Backend.Variables;

namespace Backend
{
    public class BackendManager : IBackendManager
    {
        public IFunction Start { get; }
        public IFunction Loop { get; }
        public IVariablesManager GlobalVariables { get; }
        public IUserFunctionsManager Functions { get; }
        public ITemplatesManager Templates { get; } = new TemplateManager();
        public IInstanceCreator InstanceCreator { get; }
        public List<IMyType> Types { get; } = new();
        
        public BackendManager()
        {
            foreach (var classType in ClassTypeValidator.Instance.GetAllClassTypes())
            {
                Types.Add(new ClassType(classType));
            }
            Start = new Function.Function(this, "start");
            Loop = new Function.Function(this, "loop");
            GlobalVariables = new GlobalVariablesManager(this);
            Functions = new UserFunctionManager(this);
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

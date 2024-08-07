using System.Collections.Generic;
using System.IO;
using Backend.API;
using Backend.Json;
using Backend.MyFunction;
using Backend.Type;
using Backend.Validator;
using Backend.Variables;

namespace Backend
{
    public class BackendManager : IBackendManager
    {
        public IFunction Setup { get; private set; }
        public IFunction Loop { get; private set; }
        public IVariablesManager GlobalVariables { get; private set; }
        public IUserFunctionsManager Functions { get; private set; }
        public ITemplatesManager Templates { get; } = new TemplateManager();
        public IInstanceCreator InstanceCreator { get; }
        public List<IMyType> Types { get; } = new();

        public BackendManager()
        {
            foreach (var classType in ClassTypeValidator.Instance.GetAllClassTypes())
            {
                Types.Add(new ClassType(classType));
            }

            Clear();
            InstanceCreator = new InstanceCreator(this);
        }

        public void Clear()
        {
            var rootPn = new PathName("ROOT-1");
            Setup = new Function(this, "setup", new PathName(rootPn, "SETUP"));
            Loop = new Function(this, "loop", new PathName(rootPn, "LOOP"));
            GlobalVariables = new GlobalVariablesManager(this, rootPn);
            Functions = new UserFunctionManager(this);
        }

        public void BuildCode(string savePath, string programPath)
        {
            var codeManager = new CodeManager();

            // GlobalVariables
            GlobalVariables.Variables
                .ForEach(variable =>
                {
                    codeManager.AddLine(((Variable)variable).ToCode() + ";");
                    codeManager.SetVariableStatus(variable, CodeManager.VariableStatus.Global);
                });
            codeManager.AddLine("");

            // Functions declarations
            Functions
                .Functions
                .ForEach(fun => codeManager.AddLine(((UserFunction)fun).ToCodeDeclaration() + ";"));
            codeManager.AddLine("");

            // Functions with body
            Functions
                .Functions
                .ForEach(fun =>
                {
                    ((UserFunction)fun).ToCode(codeManager);
                    codeManager.AddLine("");
                });

            // Setup
            ((Function)Setup).ToCode(codeManager);
            codeManager.AddLine("");

            // Loop
            ((Function)Loop).ToCode(codeManager);

            File.WriteAllText(savePath, codeManager.BuildCode());
        }
        public BackendManagerJson Save()
        {
            return new BackendManagerJson(this);
        }

        public void Load(BackendManagerJson json)
        {
            Setup = new Function(this, json.SetupVariables);
            Loop = new Function(this, json.LoopVariables);
            GlobalVariables = new GlobalVariablesManager(this, json.GlobalVariables);
            Functions = new UserFunctionManager(this, json.UserFunctions);
        }
    }
}

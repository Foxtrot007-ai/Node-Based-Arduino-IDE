using System.Collections.Generic;
using System.IO;
using Backend.API;
using Backend.Function;
using Backend.Json;
using Backend.Type;
using Backend.Validator;
using Backend.Variables;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Backend
{
    public class BackendManager : IBackendManager
    {
        public IFunction Start { get; private set; }
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

            var rootPn = new PathName("ROOT-1");
            Start = new Function.Function(this, "start", new PathName(rootPn, "START"));
            Loop = new Function.Function(this, "loop", new PathName(rootPn, "LOOP"));
            GlobalVariables = new GlobalVariablesManager(this, rootPn);
            Functions = new UserFunctionManager(this);
            InstanceCreator = new InstanceCreator(this);
        }

        public void BuildCode(string path)
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

            // Start
            ((Function.Function)Start).ToCode(codeManager);
            codeManager.AddLine("");

            // Loop
            ((Function.Function)Loop).ToCode(codeManager);
            codeManager.AddLine("");

            File.WriteAllText(path, codeManager.BuildCode());
        }
        public void Save(string path)
        {
            var backendJson = new BackendManagerJson(this);
            var json = JsonConvert.SerializeObject(backendJson);
            File.WriteAllText(path, json);
        }
        public void Load(string path)
        {
            var json = File.ReadAllText(path);
            Debug.Log(json);
            var backendJson = JsonConvert.DeserializeObject<BackendManagerJson>(json);
            Debug.Log(backendJson);
            Start = new Function.Function(this, backendJson.StartVariables);
            Loop = new Function.Function(this, backendJson.LoopVariables);
            GlobalVariables = new GlobalVariablesManager(this, backendJson.GlobalVariables);
            Functions = new UserFunctionManager(this, backendJson.UserFunctions);
        }
    }
}

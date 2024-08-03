using System.Collections.Generic;
using Backend.API;
using Backend.Json;
using Backend.Node.BuildIn;
using Backend.Template;
using Backend.Type;
using Backend.Variables;
using UnityEngine;

namespace Backend.Function
{
    public class Function : IFunction
    {
        public INode StartNode => _startNode;
        public virtual string Name { get; protected set; }
        public string Id => PathName.ToString();
        public virtual IVariablesManager Variables => _localVariablesManager;
        public bool IsDelete { get; protected set; } = false;
        public virtual IMyType OutputType { get; protected set; } = new VoidType();

        private StartNode _startNode = new(new BuildInTemplate(0, "Start", typeof(StartNode)));
        private LocalVariablesManager _localVariablesManager;
        protected IBackendManager _backendManager;
        public PathName PathName { get; protected init; }
        protected Function()
        {
        }

        public Function(IBackendManager backendManager, List<VariableJson> variableJsons)
        {
            _backendManager = backendManager;
            _localVariablesManager = new LocalVariablesManager(this, variableJsons);
        }

        public Function(IBackendManager backendManager, string name, PathName pathName, List<VariableJson> variableJsons)
        {
            Name = name;
            PathName = pathName;
            _backendManager = backendManager;
            _localVariablesManager = new LocalVariablesManager(this, variableJsons);
        }

        public Function(IBackendManager backendManager, string name, PathName pathName)
        {
            Name = name;
            PathName = pathName;
            _backendManager = backendManager;
            _localVariablesManager = new LocalVariablesManager(this, PathName);
        }

        protected virtual string CreateInputs()
        {
            return "";
        }

        public virtual bool IsVariableDuplicate(string name)
        {
            return ((GlobalVariablesManager)_backendManager.GlobalVariables).IsDuplicateName(name)
                   || IsVariableLocalDuplicate(name);
        }

        public virtual bool IsVariableLocalDuplicate(string name)
        {
            return _localVariablesManager.IsDuplicateName(name);
        }

        protected virtual CodeManager CreateCodeManager(CodeManager codeManager)
        {
            return new CodeManager(codeManager);
        }

        public virtual string ToCodeDeclaration()
        {
            return $"{((IType)OutputType).ToCode()} {Name}({CreateInputs()})";
        }

        public virtual void ToCode(CodeManager codeManager)
        {
            codeManager.AddLine(ToCodeDeclaration());
            var clonedCodeManager = CreateCodeManager(codeManager);
            _startNode.ToCode(clonedCodeManager);

            codeManager.AddLines(clonedCodeManager.CodeLines);
        }
    }
}

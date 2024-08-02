using Backend.API;
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
        public virtual IVariablesManager Variables => _localVariablesManager;
        public bool IsDelete { get; protected set; } = false;
        public virtual IMyType OutputType { get; protected set; } = new VoidType();

        private StartNode _startNode = new(new BuildInTemplate(0, "Start", typeof(StartNode)));
        private LocalVariablesManager _localVariablesManager;
        protected IBackendManager _backendManager;
        protected Function()
        {
        }
        public Function(IBackendManager backendManager, string name)
        {
            Name = name;
            _backendManager = backendManager;
            _localVariablesManager = new LocalVariablesManager(this);
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

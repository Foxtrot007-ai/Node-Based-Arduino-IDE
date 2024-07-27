using Backend.API;
using Backend.Node.BuildIn;
using Backend.Template;
using Backend.Type;

namespace Backend.Function
{
    public class Function : IFunction
    {
        public INode StartNode => _startNode;
        public virtual string Name { get; protected set; }
        public virtual IVariablesManager Variables { get; } = new VariablesManager();
        private StartNode _startNode = new(new BuildInTemplate(0, "Start", typeof(StartNode)));
        public virtual IMyType OutputType { get; protected set; } = new VoidType();
        
        protected Function()
        {
        }
        public Function(string name)
        {
            Name = name;
        }

        protected virtual string CreateInputs()
        {
            return "";
        }

        protected virtual CodeManager CreateCodeManager(CodeManager codeManager)
        {
            return new CodeManager(codeManager);
        }
        public virtual void ToCode(CodeManager codeManager)
        {
            codeManager.AddLine($"{((IType)OutputType).ToCode()} {Name}({CreateInputs()})");
            var clonedCodeManager = CreateCodeManager(codeManager);
            _startNode.ToCode(clonedCodeManager);
            
            codeManager.AddLines(clonedCodeManager.CodeLines);
        }
    }
}

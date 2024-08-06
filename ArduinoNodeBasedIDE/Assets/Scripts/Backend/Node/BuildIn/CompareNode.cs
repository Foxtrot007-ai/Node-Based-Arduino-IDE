using System.Collections.Generic;
using Backend.API;
using Backend.Connection;
using Backend.IO;
using Backend.Template;
using Backend.Type;

namespace Backend.Node.BuildIn
{
    public class CompareNode : BuildInNode
    {
        private CompareOpTemplate _compareTemplate;
        private ChainIO _chain1;
        private ChainIO _chain2;

        public override List<IConnection> InputsList => _chain1.ToList();

        public CompareNode(CompareOpTemplate compareTemplate) : base(compareTemplate)
        {
            _compareTemplate = compareTemplate;
            _chain1 = new ChainIO(this, IOSide.Input, false, false);
            _chain2 = new ChainIO(this, IOSide.Input, false, false);

            _chain1.AppendChain(_chain2);
            AddOutputs(new TypeIO(this, IOSide.Output, new PrimitiveType(EType.Bool)));
        }

        protected override string MakeCodeParam(CodeManager codeManager)
        {
            return $"{ConnectedToCodeParam(codeManager, _chain1)} {_compareTemplate.ToCode()} {ConnectedToCodeParam(codeManager, _chain2)}";
        }
    }
}

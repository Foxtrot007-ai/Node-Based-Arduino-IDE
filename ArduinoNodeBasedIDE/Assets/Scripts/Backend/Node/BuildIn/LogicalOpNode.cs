using System.Collections.Generic;
using System.Text;
using Backend.API;
using Backend.IO;
using Backend.Template;
using Backend.Type;

namespace Backend.Node.BuildIn
{
    public class LogicalOpNode : BuildInNode
    {
        private LogicalOpTemplate _logicalOpTemplate;
        private ChainIO _chainHead;

        public override List<IConnection> InputsList => _chainHead.ToList();

        public LogicalOpNode(LogicalOpTemplate logicalOpTemplate) : base(logicalOpTemplate)
        {
            _logicalOpTemplate = logicalOpTemplate;
            _chainHead = new ChainIO(this, IOSide.Input, true, false, new PrimitiveType(EType.Bool));

            AddOutputs(new TypeIO(this, IOSide.Output, new PrimitiveType(EType.Bool)));
        }

        protected override string MakeCodeParam(CodeManager codeManager)
        {
            var builder = new StringBuilder();
            builder.Append($"{ConnectedToCodeParam(codeManager, _chainHead)}");

            var iter = _chainHead.Next;
            // Only last will be not connected
            while (iter.Connected != null)
            {
                builder.Append($" {_logicalOpTemplate.ToCode()} {ConnectedToCodeParam(codeManager, iter)}");
                iter = iter.Next;
            }
            return builder.ToString();
        }
    }
}

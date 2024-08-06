using System.Collections.Generic;
using System.Text;
using Backend.API;
using Backend.Connection;
using Backend.IO;
using Backend.Template;

namespace Backend.Node.BuildIn
{
    public class ArithmeticOpNode : BuildInNode
    {
        private ArithmeticOpTemplate _arithmeticOpTemplate;
        private ChainIO _chainHead;

        public override List<IConnection> InputsList => _chainHead.Next.ToList();

        public ArithmeticOpNode(ArithmeticOpTemplate arithmeticOpTemplate) : base(arithmeticOpTemplate)
        {
            _arithmeticOpTemplate = arithmeticOpTemplate;
            _chainHead = new ChainIO(this, IOSide.Output, true, false);
            AddOutputs(_chainHead);

            _chainHead.AppendChain(new ChainIO(this, IOSide.Input, true, false));
        }

        protected override string MakeCodeParam(CodeManager codeManager)
        {
            var builder = new StringBuilder();
            builder.Append($"{ConnectedToCodeParam(codeManager, _chainHead.Next)}");

            var iter = _chainHead.Next.Next;
            // Only last will be not connected
            while (iter.Connected != null)
            {
                builder.Append($" {_arithmeticOpTemplate.ToCode()} {ConnectedToCodeParam(codeManager, iter)}");
                iter = iter.Next;
            }
            return builder.ToString();
        }
    }
}

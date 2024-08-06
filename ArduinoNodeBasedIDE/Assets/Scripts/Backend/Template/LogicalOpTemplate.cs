using System;
using Backend.Node.BuildIn;

namespace Backend.Template
{
    public class LogicalOpTemplate : BuildInTemplate
    {
        public enum ELogicalOp
        {
            And,
            Or,
        }

        private ELogicalOp _op;

        public LogicalOpTemplate(long id, ELogicalOp op) : base(id, ToCode(op), typeof(LogicalOpNode))
        {
            _op = op;
        }

        public string ToCode()
        {
            return ToCode(_op);
        }

        private static string ToCode(ELogicalOp op)
        {
            return op switch
            {
                ELogicalOp.And => "&&",
                ELogicalOp.Or => "||",
                _ => throw new ArgumentOutOfRangeException(nameof(op), op, null),
            };
        }
    }
}

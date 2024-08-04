using System;
using Backend.Node.BuildIn;

namespace Backend.Template
{
    public class CompareOpTemplate : BuildInTemplate
    {
        public enum ECompareOp
        {
            Equal,
            NotEqual,
            Less,
            LessOrEqual,
            Greater,
            GreaterOrEqual,
        }

        private ECompareOp _op;

        public CompareOpTemplate(long id, ECompareOp op) : base(id, ToCode(op), typeof(CompareNode))
        {
            _op = op;
        }

        public string ToCode()
        {
            return ToCode(_op);
        }

        private static string ToCode(ECompareOp op)
        {
            return op switch
            {
                ECompareOp.Equal => "==",
                ECompareOp.NotEqual => "!=",
                ECompareOp.Less => "<",
                ECompareOp.LessOrEqual => "<=",
                ECompareOp.Greater => ">",
                ECompareOp.GreaterOrEqual => ">=",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}

using System;
using Backend.Node.BuildIn;

namespace Backend.Template
{
    public class ArithmeticOpTemplate : BuildInTemplate
    {
        public enum EArithmeticOp
        {
            Add,
            Sub,
            Mul,
            Div,
            Mod,
        }

        private EArithmeticOp _op;

        public ArithmeticOpTemplate(long id, EArithmeticOp op) : base(id, ToCode(op), typeof(ArithmeticOpNode))
        {
            _op = op;
        }

        public string ToCode()
        {
            return ToCode(_op);
        }

        private static string ToCode(EArithmeticOp op)
        {
            return op switch
            {

                EArithmeticOp.Add => "+",
                EArithmeticOp.Sub => "-",
                EArithmeticOp.Mul => "*",
                EArithmeticOp.Div => "/",
                EArithmeticOp.Mod => "%",
                _ => throw new ArgumentOutOfRangeException(nameof(op), op, null),
            };
        }
    }
}

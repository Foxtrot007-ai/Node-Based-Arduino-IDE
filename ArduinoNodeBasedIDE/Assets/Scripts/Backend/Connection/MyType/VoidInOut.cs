using Backend.Node;
using Backend.Type;

namespace Backend.Connection.MyType
{
    public class VoidInOut : MyTypeInOut
    {
        public VoidInOut(BaseNode parentNode, VoidType voidType)
            : base(parentNode, InOutSide.Output, voidType)
        {
        }
        
    }
}

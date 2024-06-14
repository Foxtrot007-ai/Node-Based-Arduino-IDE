using Backend.Node;
using Backend.Type;

namespace Backend.Connection.MyType
{
    public class ClassInOut : MyTypeInOut
    {
        public ClassInOut(BaseNode parentNode, InOutSide side, ClassType classType)
            : base(parentNode, side, classType)
        {
        }

    }
}

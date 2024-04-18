using Backend.Node;
using Backend.Type;

namespace Backend.Connection.MyType
{
    public class ClassInOut : MyTypeInOut<ClassType>
    {
        public ClassInOut(IPlaceHolderNodeType parentNode, InOutSide side, ClassType classType)
            : base(parentNode, side, InOutType.Class, classType)
        {
        }

    }
}

﻿using Backend.Node;
using Backend.Type;

namespace Backend.InOut.MyType
{
    public class VoidInOut : MyTypeInOut<VoidType>
    {
        public VoidInOut(IPlaceHolderNodeType parentNode, VoidType voidType)
            : base(parentNode, InOutSide.Output, InOutType.Void, voidType)
        {
        }
    }
}

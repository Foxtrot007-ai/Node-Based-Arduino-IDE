﻿using Backend.Node;
using Backend.Type;

namespace Backend.Connection.MyType
{
    public class StringInOut : MyTypeInOut
    {
        public StringInOut(IPlaceHolderNodeType parentNode, InOutSide side, StringType stringType)
            : base(parentNode, side, stringType)
        {
        }
        
    }
}

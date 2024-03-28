using System;
using Backend.InOut;
using Backend.InOut.Base;
using Backend.Node;
using Backend.Type;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.InOut.Base;

namespace Tests.EditMode.ut.Backend.Helpers
{
    public static class InOutHelper
    {
        public static BaseInOutMock CreateBaseMock(InOutSide side = InOutSide.Output,
            IPlaceHolderNodeType parent = null, InOutType inOutType = InOutType.Primitive)
        {
            parent ??= NodeHelper.CreateBaseParent();
            var inOut = new BaseInOutMock(parent, side, inOutType);
            NodeHelper.Add(parent, inOut, side);
            return inOut;
        }

        public static BaseInOut CreateBaseInOut(InOutType inOutType, InOutSide side = InOutSide.Output,
            IPlaceHolderNodeType parent = null, ClassType classType = null)
        {
            parent ??= NodeHelper.CreateBaseParent();
            classType ??= TypeHelper.CreateClassTypeMock("test");

            switch (inOutType)
            {
                case InOutType.Flow:
                    return new FlowInOut(parent, side, "test");
                case InOutType.Void:
                    return new VoidInOut(parent, new VoidType());
                case InOutType.Class:
                    return new ClassInOut(parent, side, classType);
                case InOutType.Primitive:
                    return new PrimitiveInOut(parent, side, (PrimitiveType) TypeHelper.CreateType(EType.Int));
                case InOutType.String:
                    return new StringInOut(parent, side, new StringType());
                default:
                    throw new ArgumentOutOfRangeException(nameof(inOutType), inOutType, null);
            }
        }
        
        public static void Connect(IInOut inOut1, IInOut inOut2)
        {
            inOut2.Connected = inOut1;
            inOut1.Connected = inOut2;
            ExpectAreConnected(inOut1, inOut2);
        }
        public static void ExpectAreConnected(IInOut inOut1, IInOut inOut2)
        {
            Assert.AreSame(inOut1, inOut2.Connected);
            Assert.AreSame(inOut2, inOut1.Connected);
        }

        public static void ExpectAreNotConnected(IInOut inOut1, IInOut inOut2)
        {
            if (inOut1.Connected is null || inOut2.Connected is null) return;

            Assert.AreNotSame(inOut1, inOut2.Connected);
            Assert.AreSame(inOut2, inOut1.Connected);
        }
    }
}
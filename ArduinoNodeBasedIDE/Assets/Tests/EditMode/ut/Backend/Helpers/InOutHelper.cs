using System;
using Backend.API;
using Backend.InOut;
using Backend.InOut.MyType;
using Backend.Node;
using Backend.Type;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.InOut;

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

        public static MyTypeInOutMock CreateMyTypeInOutMock(InOutSide side = InOutSide.Output,
            IPlaceHolderNodeType parent = null, IType myType = null, InOutType inOutType = InOutType.Primitive)
        {
            parent ??= NodeHelper.CreateBaseParent();
            myType ??= TypeHelper.CreateMyTypeMock();
            var inOut = new MyTypeInOutMock(parent, side, inOutType, myType);
            NodeHelper.Add(parent, inOut, side);
            return inOut;
        }

        public static FlowInOut CreateFlowInOut(InOutSide side = InOutSide.Output, IPlaceHolderNodeType parent = null, string name = "test")
        {
            parent ??= NodeHelper.CreateBaseParent();
            var inOut = new FlowInOut(parent, side, name);
            NodeHelper.Add(parent, inOut, side);
            return inOut;
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

        public static void ExpectNullConnected(params IInOut[] list)
        {
            foreach (var inOut in list)
            {
                Assert.Null(inOut.Connected);
            }
        }

        public static void ExpectAreNotConnected(IInOut inOut1, IInOut inOut2)
        {
            if (inOut1.Connected is null || inOut2.Connected is null) return;

            Assert.AreNotSame(inOut1, inOut2.Connected);
            Assert.AreSame(inOut2, inOut1.Connected);
        }

    }
}

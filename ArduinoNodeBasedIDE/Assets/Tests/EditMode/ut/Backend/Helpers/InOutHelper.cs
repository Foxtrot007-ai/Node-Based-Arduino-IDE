using Backend.Connection;
using Backend.Node;
using Backend.Type;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Connection;

namespace Tests.EditMode.ut.Backend.Helpers
{
    public static class InOutHelper
    {
        public static InOutMock CreateBaseMock(InOutSide side = InOutSide.Output,
            IPlaceHolderNodeType parent = null, InOutType inOutType = InOutType.Primitive)
        {
            parent ??= NodeHelper.CreateBaseParent();
            var inOut = new InOutMock(parent, side, inOutType);
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

        public static void ExpectAreConnected(InOut inOut1, InOut inOut2)
        {
            Assert.AreSame(inOut1, inOut2.Connected);
            Assert.AreSame(inOut2, inOut1.Connected);
        }

        public static void ExpectNullConnected(params InOut[] list)
        {
            foreach (var inOut in list)
            {
                Assert.Null(inOut.Connected);
            }
        }

        public static void ExpectAreNotConnected(InOut inOut1, InOut inOut2)
        {
            if (inOut1.Connected is null || inOut2.Connected is null) return;

            Assert.AreNotSame(inOut1, inOut2.Connected);
            Assert.AreSame(inOut2, inOut1.Connected);
        }

    }
}

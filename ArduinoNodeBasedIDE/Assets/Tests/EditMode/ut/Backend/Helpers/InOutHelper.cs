using Backend.Connection;
using Backend.Connection.MyType;
using Backend.Type;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Connection;
using Tests.EditMode.ut.Backend.Mocks;
using Tests.EditMode.ut.Backend.Mocks.Connection;

namespace Tests.EditMode.ut.Backend.Helpers
{
    public static class InOutHelper
    {
        public static InOutMock CreateBaseMock(InOutSide side = InOutSide.Output,
            BaseNodeMock parent = null)
        {
            parent ??= Substitute.ForPartsOf<BaseNodeMock>();
            var inOut = new InOutMock(parent, side);
            parent.Add(inOut, side);
            return inOut;
        }

        public static MyTypeInOutMock CreateMyTypeInOutMock(InOutSide side = InOutSide.Output,
            BaseNodeMock parent = null, IType myType = null)
        {
            parent ??= Substitute.ForPartsOf<BaseNodeMock>();
            myType ??= TypeHelper.CreateMyTypeMock();
            var inOut = new MyTypeInOutMock(parent, side, myType);
            parent.Add(inOut, side);
            return inOut;
        }

        public static AnyInOut CreateAnyInOut(InOutSide side = InOutSide.Output, BaseNodeMock parent = null, IType myType = null)
        {
            parent ??= Substitute.ForPartsOf<BaseNodeMock>();
            myType ??= TypeHelper.CreateMyTypeMock();
            var inOut = new AnyInOut(parent, side, myType);
            parent.Add(inOut, side);
            return inOut;
        }
        
        public static FlowInOut CreateFlowInOut(InOutSide side = InOutSide.Output, BaseNodeMock parent = null, string name = "test")
        {
            parent ??= Substitute.ForPartsOf<BaseNodeMock>();
            var inOut = new FlowInOut(parent, side, name);
            parent.Add(inOut, side);
            return inOut;
        }

        public static AutoInOut CreateAutoInOut(InOutSide side = InOutSide.Output, BaseNodeMock parent = null)
        {
            parent ??= Substitute.ForPartsOf<BaseNodeMock>();
            var inOut = new AutoInOut(parent, side);
            parent.Add(inOut, side);
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

        public static void Connect(MyTypeInOut input, MyTypeInOut output)
        {
            output.MyType.CanBeCast(input.MyType).Returns(true);
            output.MyType.IsAdapterNeed(input.MyType).Returns(false);
            output.Connect(input);
            ExpectAreConnected(output, input);
        }
    }
}

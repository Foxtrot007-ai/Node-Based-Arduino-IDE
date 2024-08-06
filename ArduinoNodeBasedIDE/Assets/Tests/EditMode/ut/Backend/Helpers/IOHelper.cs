using Backend.Connection;
using Backend.IO;
using Backend.Type;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Mocks;
using Tests.EditMode.ut.Backend.Mocks.IO;

namespace Tests.EditMode.ut.Backend.Helpers
{
    public static class IOHelper
    {
        public static BaseIOMock CreateBaseMock(IOSide side = IOSide.Output,
            BaseNodeMock parent = null)
        {
            parent ??= Substitute.ForPartsOf<BaseNodeMock>();
            var inOut = new BaseIOMock(parent, side);
            parent.Add(inOut, side);
            return inOut;
        }

        public static TypeIO CreateTypeIO(IOSide side = IOSide.Output,
            BaseNodeMock parent = null, IType myType = null)
        {
            parent ??= Substitute.ForPartsOf<BaseNodeMock>();
            myType ??= MockHelper.CreateType();
            var inOut = new TypeIO(parent, side, myType);
            parent.Add(inOut, side);
            return inOut;
        }

        public static FlowIO CreateFlowIO(IOSide side = IOSide.Output, BaseNodeMock parent = null, string name = "test")
        {
            parent ??= Substitute.ForPartsOf<BaseNodeMock>();
            var inOut = new FlowIO(parent, side, name);
            parent.Add(inOut, side);
            return inOut;
        }

        public static AutoIO CreateAutoIO(IOSide side = IOSide.Output, BaseNodeMock parent = null)
        {
            parent ??= Substitute.ForPartsOf<BaseNodeMock>();
            var inOut = new AutoIO(parent, side);
            parent.Add(inOut, side);
            return inOut;
        }

        public static void ExpectAreConnected(BaseIO inOut1, BaseIO inOut2)
        {
            Assert.AreSame(inOut1, inOut2.Connected);
            Assert.AreSame(inOut2, inOut1.Connected);
        }

        public static void ExpectNullConnected(params BaseIO[] list)
        {
            foreach (var inOut in list)
            {
                Assert.Null(inOut.Connected);
            }
        }

        public static void ExpectAreNotConnected(BaseIO inOut1, BaseIO inOut2)
        {
            if (inOut1.Connected is null || inOut2.Connected is null) return;

            Assert.AreNotSame(inOut1, inOut2.Connected);
            Assert.AreSame(inOut2, inOut1.Connected);
        }

        public static void AllowConnect(TypeIO output, IType type)
        {
            output.MyType.CanBeCast(type).Returns(true);
            output.MyType.IsAdapterNeed(type).Returns(false);
        }

        public static void Connect(TypeIO input, TypeIO output)
        {
            AllowConnect(output, input.MyType);
            output.Connect(input);
            ExpectAreConnected(output, input);
        }

        public static void ConnectAuto(AutoIO input, TypeIO output)
        {
            AllowConnect(output, output.MyType);
            output.Connect(input);
            ExpectAreConnected(output, input);
        }
    }
}

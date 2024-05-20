using System.Collections.Generic;
using Backend.Connection;
using Backend.Node;
using NSubstitute;

namespace Tests.EditMode.ut.Backend.Helpers
{
    public static class NodeHelper
    {
        public static IPlaceHolderNodeType CreateBaseParent()
        {
            var parent = Substitute.For<IPlaceHolderNodeType>();
            parent.InputsListInOut.Returns(new List<InOut> { });
            parent.OutputsListInOut.Returns(new List<InOut> { });
            return parent;
        }

        public static void SetInputs(IPlaceHolderNodeType parent, List<InOut> inputs)
        {
            parent.InputsListInOut.Returns(inputs);
        }

        public static void SetOutputs(IPlaceHolderNodeType parent, List<InOut> outputs)
        {
            parent.OutputsListInOut.Returns(outputs);
        }

        public static void AddInputs(IPlaceHolderNodeType parent, List<InOut> inputs)
        {
            var temp = parent.InputsListInOut;
            temp.AddRange(inputs);
            parent.InputsListInOut.Returns(temp);
        }

        public static void AddInputs(IPlaceHolderNodeType parent, InOut input)
        {
            var temp = parent.InputsListInOut;
            temp.Add(input);
            parent.InputsListInOut.Returns(temp);
        }

        public static void AddOutputs(IPlaceHolderNodeType parent, InOut output)
        {
            var temp = parent.OutputsListInOut;
            temp.Add(output);
            parent.OutputsListInOut.Returns(temp);
        }

        public static void Add(IPlaceHolderNodeType parent, InOut inOut, InOutSide side)
        {
            if (side == InOutSide.Input)
                AddInputs(parent, inOut);
            else if (side == InOutSide.Output) AddOutputs(parent, inOut);
        }
    }
}
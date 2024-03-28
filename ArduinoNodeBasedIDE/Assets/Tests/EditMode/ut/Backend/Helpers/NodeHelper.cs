using System.Collections.Generic;
using Backend.InOut;
using Backend.Node;
using NSubstitute;

namespace Tests.EditMode.ut.Backend.Helpers
{
    public static class NodeHelper
    {
        public static IPlaceHolderNodeType CreateBaseParent()
        {
            var parent = Substitute.For<IPlaceHolderNodeType>();
            parent.InputsListInOut.Returns(new List<IInOut> { });
            parent.OutputsListInOut.Returns(new List<IInOut> { });
            return parent;
        }

        public static void SetInputs(IPlaceHolderNodeType parent, List<IInOut> inputs)
        {
            parent.InputsListInOut.Returns(inputs);
        }

        public static void SetOutputs(IPlaceHolderNodeType parent, List<IInOut> outputs)
        {
            parent.OutputsListInOut.Returns(outputs);
        }

        public static void AddInputs(IPlaceHolderNodeType parent, List<IInOut> inputs)
        {
            var temp = parent.InputsListInOut;
            temp.AddRange(inputs);
            parent.InputsListInOut.Returns(temp);
        }

        public static void AddInputs(IPlaceHolderNodeType parent, IInOut input)
        {
            var temp = parent.InputsListInOut;
            temp.Add(input);
            parent.InputsListInOut.Returns(temp);
        }

        public static void AddOutputs(IPlaceHolderNodeType parent, IInOut output)
        {
            var temp = parent.OutputsListInOut;
            temp.Add(output);
            parent.OutputsListInOut.Returns(temp);
        }

        public static void Add(IPlaceHolderNodeType parent, IInOut inOut, InOutSide side)
        {
            if (side == InOutSide.Input)
                AddInputs(parent, inOut);
            else if (side == InOutSide.Output) AddOutputs(parent, inOut);
        }
    }
}
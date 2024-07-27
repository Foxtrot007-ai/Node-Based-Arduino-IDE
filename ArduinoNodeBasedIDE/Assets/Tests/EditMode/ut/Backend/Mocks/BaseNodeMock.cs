using Backend.Connection;
using Backend.Node;

namespace Tests.EditMode.ut.Backend.Mocks
{
    public class BaseNodeMock : BaseNode
    {
        public void AddInputs(BaseIO input)
        {
            InputsList.Add(input);
        }

        public void AddOutputs(BaseIO output)
        {
            OutputsList.Add(output);
        }

        public void Add(BaseIO baseIO, IOSide side)
        {
            switch (side)
            {
                case IOSide.Input:
                    AddInputs(baseIO);
                    break;
                case IOSide.Output:
                    AddOutputs(baseIO);
                    break;
            }
        }
    }
}

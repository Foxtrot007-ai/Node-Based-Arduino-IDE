using Backend.Connection;
using Backend.Node;

namespace Tests.EditMode.ut.Backend.Mocks
{
    public class BaseNodeMock : BaseNode
    {
        public void AddInputs(InOut input)
        {
            InputsList.Add(input);
        }

        public void AddOutputs(InOut output)
        {
            OutputsList.Add(output);
        }

        public void Add(InOut inOut, InOutSide side)
        {
            switch (side)
            {
                case InOutSide.Input:
                    AddInputs(inOut);
                    break;
                case InOutSide.Output:
                    AddOutputs(inOut);
                    break;
            }
        }
        protected override void CheckToCode()
        {
            throw new System.NotImplementedException();
        }
        public override string ToCode()
        {
            throw new System.NotImplementedException();
        }
    }   
}

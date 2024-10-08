using Backend.API.DTO;

namespace Backend.API
{
    public interface IInputNode : INode
    {
        public string Value { get; }
        public IMyType Type { get; }
        /*
         * Might throw:
         * InvalidInputNodeValueDto
         * WrongConnectionType
         */
        public void SetValue(InputNodeValueDto inputNodeValueDto);
    }
}

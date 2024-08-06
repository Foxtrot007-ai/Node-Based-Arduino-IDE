using Backend.API.DTO;

namespace Backend.API
{
    public interface IInputNode : INode
    {
        public string Value { get; }
        /*
         * DEPRECATED
         */
        public void SetValue(string value);
        
        /*
         * Might throw:
         * InvalidInputNodeValueDto
         * WrongConnectionType
         */
        public void SetValue(InputNodeValueDto inputNodeValueDto);
    }
}

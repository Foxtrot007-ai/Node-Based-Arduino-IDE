namespace Backend.API
{
    public interface IInputNode : INode
    {
        public string Value { get; }
        /*
         * Might throw:
         * WrongType
         */
        public void SetValue(string value);
    }
}

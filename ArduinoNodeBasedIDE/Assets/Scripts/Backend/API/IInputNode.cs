namespace Backend.API
{
    public interface IInputNode : INode
    {
        public string Value { get; }
        public void SetValue(string value);
    }
}

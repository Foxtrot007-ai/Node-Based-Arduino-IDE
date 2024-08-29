namespace Backend.API
{
    public interface IInstanceCreator
    {
        public INode CreateNodeInstance(string id, IFunction function);
    }
}

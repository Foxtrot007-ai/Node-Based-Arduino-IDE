namespace Backend.API
{
    public interface IInstanceCreator
    {
        // DEPRECATED
        public INode CreateNodeInstance(string id);
        public INode CreateNodeInstance(string id, IFunction function);
    }
}

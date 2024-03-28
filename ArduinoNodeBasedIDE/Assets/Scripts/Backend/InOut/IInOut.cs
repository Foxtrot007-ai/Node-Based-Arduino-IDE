using Backend.Node;

namespace Backend.InOut
{
    public interface IInOut : IConnection
    {
        public new IInOut Connected { get; set; }
        public IPlaceHolderNodeType ParentNode { get; }
    }
}

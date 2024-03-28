using Backend.InOut;

namespace Backend.Node
{
    public interface ISubscribeNode
    {
        public void ConnectNotify(IInOut inOut);
        public void DisconnectNotify(IInOut inOut);
        public void ChangeTypeNotify(IInOut inOut);
    }
}

using Backend.Connection;

namespace Backend.Node
{
    public interface ISubscribeNode
    {
        public void ConnectNotify(InOut inOut);
        public void DisconnectNotify(InOut inOut);
        public void ChangeTypeNotify(InOut inOut);
    }
}
namespace Backend.Connection
{
    public interface ISubscribeInOut
    {
        public void ConnectNotify(InOut inOut);
        public void DisconnectNotify(InOut inOut);
    }
}
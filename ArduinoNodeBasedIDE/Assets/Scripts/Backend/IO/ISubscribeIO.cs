using Backend.IO;

namespace Backend.Connection
{
    public interface ISubscribeIO
    {
        public void ConnectNotify(BaseIO baseIO);
        public void DisconnectNotify(BaseIO baseIO);
    }
}

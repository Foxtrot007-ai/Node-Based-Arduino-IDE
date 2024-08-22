namespace Backend.IO
{
    public interface ISubscribeIO
    {
        public void ConnectNotify(TypeIO typeIO);
        public void DisconnectNotify(TypeIO typeIO);
        public void TypeChangeNotify(TypeIO typeIO);
    }
}

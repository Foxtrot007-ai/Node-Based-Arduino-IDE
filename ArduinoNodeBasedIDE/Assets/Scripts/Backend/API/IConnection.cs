using Backend.Connection;

namespace Backend.API
{
    public interface IConnection
    {
        public IConnection Connected { get; }
        public InOutType InOutType { get; }
        public string InOutName { get; }
        public void Connect(IConnection iConnection); //Make connection between IConnection
        public void Disconnect(); //Remove connection between IConnection
        public ConnectionPoint UIPoint { get; set; }
    }
}
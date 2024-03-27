using System;
using Backend.InOut;

namespace Backend
{
    public interface IConnection
    {
        public IConnection Connected { get; }
        public IType MyType { get; } //need??
        public InOutSide Side { get; } //need?
        public InOutType InOutType { get; }
        public String InOutName { get; }
        public void Connect(IConnection iConnection); //Make connection between IInOut
        public void Disconnect(); //Remove connection between IInOut
        public void ChangeType(IType iType); 
        public ConnectionPoint UIPoint { get; set; }
    }
}
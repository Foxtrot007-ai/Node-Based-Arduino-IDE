using System;
using Backend.InOut;

namespace Backend
{
    public interface IConnection //for UI
    {
        public ConnectionPoint UIPoint { get; set; }
        public IConnection Connected { get; }
        public IType MyType { get; } //is need for UI??
        public InOutSide Side { get; } //is need for UI??
        public InOutType InOutType { get; }
        public String InOutName { get; }
        public void Connect(IConnection iConnection); //Make connection between IInOut
        public void Disconnect(); //Remove connection between IInOut
        public void ChangeType(IType iType); 
        
    }
}
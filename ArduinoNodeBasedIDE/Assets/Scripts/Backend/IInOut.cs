using System;
using Backend.InOut;

namespace Backend
{
    public interface IInOut
    {
        public IInOut Connected { get; }
        public IType MyType { get; } //need??
        public InOutSide Side { get; } //need?
        public InOutType InOutType { get; }
        public String InOutName { get; }
        public void Connect(IInOut iInOut);
        public void Disconnect();
        public void ChangeType(IType iType);
        public ConnectionPoint UIPoint { get; set; }
    }
}
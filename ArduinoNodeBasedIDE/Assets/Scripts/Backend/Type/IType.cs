using Backend.API;

namespace Backend.Type
{
    public interface IType : IMyType
    {
        public bool CanBeCast(IType iMyType);
        public bool IsAdapterNeed(IType iMyType); // return false if CanBeCast is false
    }
}

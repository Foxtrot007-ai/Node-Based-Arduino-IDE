using Backend.Type;

namespace Backend.API
{
    public interface IMyType
    {
        public string TypeName { get; }
        public EType EType { get; }
    }
}

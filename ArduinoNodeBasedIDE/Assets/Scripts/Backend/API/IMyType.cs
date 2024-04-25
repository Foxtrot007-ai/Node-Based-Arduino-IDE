using Backend.Type;

namespace Backend.API
{
    public interface IMyType
    {
        /*
         * Return type as string
         * For class it will return name of class
         */
        public string TypeName { get; }
        
        /*
         * Return type
         */
        public EType EType { get; }
    }
}

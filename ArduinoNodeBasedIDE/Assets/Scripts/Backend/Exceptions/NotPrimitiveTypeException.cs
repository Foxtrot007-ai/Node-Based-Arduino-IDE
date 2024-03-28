using Backend.Type;

namespace Backend.Exceptions
{
    public class NotPrimitiveTypeException : WrongTypeException
    {

        public NotPrimitiveTypeException(EType eType) : base(eType + " is not primitive type.")
        {
        }
    }
}

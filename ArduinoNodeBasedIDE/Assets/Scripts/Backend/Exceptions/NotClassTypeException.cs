namespace Backend.Exceptions
{
    public class NotClassTypeException : WrongTypeException
    {

        public NotClassTypeException(string classType) : base("\"" + classType + "\" is not class type.")
        {
        }
    }
}
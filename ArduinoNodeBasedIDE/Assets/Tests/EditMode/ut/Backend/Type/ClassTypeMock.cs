using Backend.Type;
using Backend.Validator;

namespace Tests.EditMode.ut.Backend.Type
{
    public class ClassTypeMock : ClassType
    {
        public ClassTypeMock(string classType, IClassTypeValidator validator) : base(classType, validator)
        {
        }
    }
}
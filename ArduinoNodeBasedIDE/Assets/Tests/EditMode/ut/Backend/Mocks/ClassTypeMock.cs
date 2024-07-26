using Backend.Type;
using Backend.Validator;

namespace Tests.EditMode.ut.Backend.Mocks
{
    public class ClassTypeMock : ClassType
    {
        public ClassTypeMock()
        {
        }
        public ClassTypeMock(string classType, IClassTypeValidator validator) : base(classType, validator)
        {
        }
    }
}

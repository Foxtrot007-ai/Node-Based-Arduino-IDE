using Backend.Type;
using Backend.Validator;

namespace Tests.EditMode.ut.Backend.Type
{
    public class ClassTypeMock : ClassType
    {
        private ClassTypeMock(string classType) : base(classType)
        {
        }

        public ClassTypeMock(string classType, IClassTypeValidator validator) : base(classType, validator)
        {
        }
    }
}
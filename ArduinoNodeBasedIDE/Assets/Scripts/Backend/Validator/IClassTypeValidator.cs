using System.Collections.Generic;

namespace Backend.Validator
{
    public interface IClassTypeValidator //only for test
    {
        public bool IsClassType(string className);
        public HashSet<string> GetAllClassTypes();
        public void AddClassType(string classType);
    }
}
using Backend.API.DTO;
using Backend.Node;
using Backend.Template.Json;
using Backend.Type;

namespace Backend.Template
{
    public class ClassMethodTemplate : FunctionTemplate
    {
        public virtual ClassType Class { get; }

        protected ClassMethodTemplate()
        {
        }
        public ClassMethodTemplate(long id, string library, FunctionJson functionJson, ClassType classType) : base(id, library, functionJson)
        {
            Class = classType;

            Category = classType.TypeName;
            _instanceType = typeof(ClassMethodNode);
        }
    }
}

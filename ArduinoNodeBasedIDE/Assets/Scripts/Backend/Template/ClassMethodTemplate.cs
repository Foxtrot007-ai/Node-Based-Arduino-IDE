using Backend.Node;
using Backend.Template.Json;
using Backend.Type;

namespace Backend.Template
{
    public class ClassMethodTemplate : FunctionTemplate
    {
        public override string Name => Class.TypeName + "." + FunctionName;
        public virtual ClassType Class { get; }

        protected ClassMethodTemplate()
        {
        }
        public ClassMethodTemplate(long id, string library, FunctionJson functionJson, ClassType classType) 
            : base(library, functionJson, new PathName($"ROOT-1/TEMPLATE-1/{library.ToUpper()}-1/{classType.TypeName.ToUpper()}-1/METHOD-{id}"))
        {
            Class = classType;

            Category = classType.TypeName;
            _instanceType = typeof(ClassMethodNode);
        }
    }
}

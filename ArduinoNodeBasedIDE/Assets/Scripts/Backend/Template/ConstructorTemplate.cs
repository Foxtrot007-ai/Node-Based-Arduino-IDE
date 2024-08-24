using System.Collections.Generic;
using System.Linq;
using Backend.Node;
using Backend.Type;

namespace Backend.Template
{
    public class ClassConstructorTemplate : BaseTemplate
    {
        public override string Name => Class.TypeName;
        public virtual ClassType Class { get; }
        public virtual string Library { get; }
        public virtual List<IType> Inputs { get; }

        protected ClassConstructorTemplate()
        {
        }
        public ClassConstructorTemplate(long id, string library, List<string> inputs, ClassType classType) 
            : base(new PathName($"ROOT-1/TEMPLATE-1/{library.ToUpper()}-1/{classType.TypeName.ToUpper()}-1/CONSTRUCTOR-{id}"))
        {
            Class = classType;
            Library = library;
            Inputs = inputs.Select(TypeConverter.ToIType).ToList();

            Category = classType.TypeName;
            _instanceType = typeof(ClassConstructorNode);
        }
    }
}

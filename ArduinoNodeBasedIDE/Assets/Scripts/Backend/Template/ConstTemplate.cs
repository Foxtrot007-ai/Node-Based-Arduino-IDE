using Backend.Node;
using Backend.Template.Json;
using Backend.Type;

namespace Backend.Template
{
    public class ConstTemplate : BaseTemplate
    {
        public virtual string Library { get; }
        public override string Name { get; }
        public virtual IType Type { get; }
        public ConstTemplate(long id, string library, ConstJson json) 
            : base(new PathName($"ROOT-1/TEMPLATE-1/{library.ToUpper()}-1/CONST-{id}"))
        {
            Name = json.Name;
            Library = library;
            Type = TypeConverter.ToIType(json.Type);
            _instanceType = typeof(ConstNode);
        }
    }
}

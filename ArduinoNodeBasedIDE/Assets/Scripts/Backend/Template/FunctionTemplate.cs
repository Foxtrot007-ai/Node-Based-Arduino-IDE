using System.Collections.Generic;
using System.Linq;
using Backend.API.DTO;
using Backend.Node;
using Backend.Template.Json;
using Backend.Type;

namespace Backend.Template
{
    public class FunctionTemplate : BaseTemplate
    {
        public virtual string Library { get; }
        public virtual List<IType> InputsTypes { get; }
        public virtual IType OutputType { get; }

        protected FunctionTemplate(){}
        public FunctionTemplate(long id, string library, FunctionJson functionJson) : base(id, functionJson.Name)
        {
            Library = library;
            OutputType = TypeConverter.ToIType(functionJson.OutputType);
            InputsTypes = functionJson.InputsType.Select(TypeConverter.ToIType).ToList();

            Category = library;
            _instanceType = typeof(FunctionNode);
        }

        public FunctionTemplate(long id, FunctionTemplateDto functionTemplateDto) : base(id, functionTemplateDto.FunctionName)
        {
            Library = functionTemplateDto.Library;
            InputsTypes = functionTemplateDto.InputsType.Select(TypeConverter.ToIType).ToList();
            OutputType = TypeConverter.ToIType(functionTemplateDto.OutputType);

            Category = functionTemplateDto.Library;
            _instanceType = typeof(FunctionNode);
        }
    }
}

using System.Collections.Generic;
using Backend.API.DTO;

namespace Backend.API
{
    public interface ITemplatesManager
    {
        /*
         * Return list of every available templates (INodes)
         */
        public List<ITemplate> Templates { get; }

        /*
         * Add new function template
         * Save in file
         */
        public void AddFunctionTemplate(FunctionTemplateDto functionTemplateDto);

        /*
         * Add new class method template
         * If class not exist, create new class (can be use as method input)
         * Save in file
         */
        public void AddClassMethodTemplate(string className, FunctionTemplateDto functionTemplateDto);

        /*
         * Add new class constructor template
         * If class not exist, create new class (can be use as method input)
         * Save in file
         */
        public void AddClassConstructorTemplate(string className, string library, List<string> inputs);
    }
}

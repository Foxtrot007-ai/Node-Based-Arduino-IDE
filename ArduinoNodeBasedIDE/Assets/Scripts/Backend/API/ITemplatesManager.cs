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
    }
}

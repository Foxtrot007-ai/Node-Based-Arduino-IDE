using System;
using Backend.Type;

namespace Backend
{
    public interface IType
    {
        public EType GetEType { get; }
        public string TypeName { get; }
        
    }
}

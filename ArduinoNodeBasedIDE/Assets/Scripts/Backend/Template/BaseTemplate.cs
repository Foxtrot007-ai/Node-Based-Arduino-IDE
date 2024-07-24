using System;
using Backend.API;

namespace Backend.Template
{
    public abstract class BaseTemplate : ITemplate
    {
        protected System.Type _instanceType;
        public virtual string Name { get; }
        public virtual string Category { get; protected set; }
        public virtual long Id { get; }

        protected BaseTemplate(){}
        protected BaseTemplate(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public INode CreateNodeInstance()
        {
            return (INode)Activator.CreateInstance(_instanceType, this);
        }
    }
}

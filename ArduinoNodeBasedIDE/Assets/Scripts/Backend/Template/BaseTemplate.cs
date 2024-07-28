using System;
using Backend.API;

namespace Backend.Template
{
    public abstract class BaseTemplate : ITemplate
    {
        protected System.Type _instanceType;
        public virtual string Name { get; }
        public virtual string Category { get; protected set; }
        public virtual string Id { get; }

        protected BaseTemplate(){}
        protected BaseTemplate(long id, string name)
        {
            Id = id.ToString();
            Name = name;
        }

        public INode CreateNodeInstance()
        {
            return (INode)Activator.CreateInstance(_instanceType, this);
        }
    }
}

using System;
using Backend.API;

namespace Backend.Template
{
    public abstract class BaseTemplate : ITemplate
    {
        protected System.Type _instanceType;
        public virtual string Name { get; }
        public virtual string Category { get; protected set; }
        public string Id => PathName.ToString();
        public virtual PathName PathName { get; }

        protected BaseTemplate()
        {
        }
        protected BaseTemplate(long id, string name)
        {
            PathName = new PathName(new PathName("ROOT-1"), "TEMPLATE", id);
            Name = name;
        }

        public INode CreateNodeInstance()
        {
            return (INode)Activator.CreateInstance(_instanceType, this);
        }

        public virtual INode CreateNodeInstance(IFunction function)
        {
            return (INode)Activator.CreateInstance(_instanceType, this);
        }
    }
}

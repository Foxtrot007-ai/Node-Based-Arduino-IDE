using System;
using Backend.API;

namespace Backend.Template
{
    public abstract class BaseTemplate : ITemplate
    {
        protected System.Type _instanceType;
        public abstract string Name { get; }
        public virtual string Category { get; protected set; }
        public string Id => PathName.ToString();
        public virtual PathName PathName { get; }

        protected BaseTemplate()
        {
        }
        protected BaseTemplate(PathName pathName)
        {
            PathName = pathName;
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

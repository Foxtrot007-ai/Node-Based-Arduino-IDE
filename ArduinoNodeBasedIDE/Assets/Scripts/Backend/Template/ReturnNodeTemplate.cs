using Backend.API;
using Backend.MyFunction;
using Backend.Node.BuildIn;

namespace Backend.Template
{
    public class ReturnNodeTemplate : BuildInTemplate
    {

        public ReturnNodeTemplate(long id) : base(id, "Return", typeof(ReturnNode))
        {
        }

        public override INode CreateNodeInstance(IFunction function)
        {
            return new ReturnNode(this, (Function)function);
        }
    }
}

namespace Backend.Template
{
    public class BuildInTemplate : BaseTemplate
    {
        protected BuildInTemplate(){}
        public BuildInTemplate(long id, string name, System.Type instanceType) : base(id, name)
        {
            Category = "buildIn";
            _instanceType = instanceType;
        }
    }
}

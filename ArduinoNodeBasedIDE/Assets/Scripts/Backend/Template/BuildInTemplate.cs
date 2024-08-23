namespace Backend.Template
{
    public class BuildInTemplate : BaseTemplate
    {
        public override string Name { get; }
        protected BuildInTemplate()
        {
        }
        public BuildInTemplate(long id, string name, System.Type instanceType) : base(id)
        {
            Name = name;
            Category = "buildIn";
            _instanceType = instanceType;
        }
    }
}

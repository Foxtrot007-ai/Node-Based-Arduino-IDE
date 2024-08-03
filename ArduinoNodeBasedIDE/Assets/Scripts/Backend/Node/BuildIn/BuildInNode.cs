using Backend.Template;

namespace Backend.Node.BuildIn
{
    public class BuildInNode : BaseNode
    {

        private readonly BuildInTemplate _buildInTemplate;
        public override string NodeName => _buildInTemplate.Name;
        public override string CreatorId => _buildInTemplate.Id;

        protected BuildInNode()
        {
        }
        public BuildInNode(BuildInTemplate buildInTemplate)
        {
            _buildInTemplate = buildInTemplate;
        }
    }
}

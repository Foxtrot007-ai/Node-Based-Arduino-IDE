using Backend.Template;

namespace Backend.Node.BuildIn
{
    public class BuildInNode : BaseNode
    {

        private readonly BuildInTemplate _buildInTemplate;
        public override string NodeName => _buildInTemplate.Name;
        
        public BuildInNode(BuildInTemplate buildInTemplate)
        {
            _buildInTemplate = buildInTemplate;
        }
    }
}

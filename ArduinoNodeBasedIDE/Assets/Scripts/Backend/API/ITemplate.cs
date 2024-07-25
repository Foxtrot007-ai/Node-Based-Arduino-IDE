namespace Backend.API
{
    public interface ITemplate
    {
        /*
         * INode name
         */
        public string Name { get; }

        /*
         * INode category might be useful in future
         */
        public string Category { get; }

        /*
         * Return unique id of template
         */
        public long Id { get; }

        /*
         * Create new INode instance
         */
        public INode CreateNodeInstance();
    }
}

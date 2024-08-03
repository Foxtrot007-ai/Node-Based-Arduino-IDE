namespace Backend.API
{
    public interface IFunction
    {
        public string Id { get; }

        /*
         * Start node of function flow
         */
        public INode StartNode { get; }

        /*
         * Function name
         */
        public string Name { get; }

        /*
         * Local variables
         */
        public IVariablesManager Variables { get; }

        /*
         * If true IConnection is logicalDelete, need physical delete
         */
        public bool IsDelete { get; }
    }
}

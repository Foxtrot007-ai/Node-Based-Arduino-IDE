namespace Backend.API
{
    public interface IFunction
    {
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
        
        public bool IsDelete { get; }
    }
}

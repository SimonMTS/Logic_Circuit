using Logic_Circuit.Models.BaseNodes;

namespace Logic_Circuit.Models.Factories
{
    /// <summary>
    /// Determines nodeType, then calls the appropriate factory to produce that node.
    /// </summary>
    public class NodeFactory : INodeFactory
    {
        private readonly INodeFactory inputNodeFactory = new InputNodeFactory();
        private readonly INodeFactory outputNodeFactory = new OutputNodeFactory();
        private readonly INodeFactory circuitNodeFactory = new CircuitNodeFactory();
        private readonly INodeFactory nandNodeFactory = new NandNodeFactory();

        public INode GetNode(string name, string type)
        {
            if (type.Equals("INPUT_HIGH") || type.Equals("INPUT_LOW"))
            {
                return inputNodeFactory.GetNode(name, type);
            }
            else if (type.Equals("PROBE"))
            {
                return outputNodeFactory.GetNode(name, type);
            }
            else if (type.Equals("NAND"))
            {
                return nandNodeFactory.GetNode(name, type);
            }
            else
            {
                return circuitNodeFactory.GetNode(name, type);
            }
        }
    }
}

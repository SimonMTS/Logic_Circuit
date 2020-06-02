using Logic_Circuit.Models.BaseNodes;

namespace Logic_Circuit.Models.Factories
{
    /// <summary>
    /// Returns a new OutputNode.
    /// </summary>
    public class OutputNodeFactory : INodeFactory
    {
        public INode GetNode(string name, string type)
        {
            return new OutputNode(name);
        }
    }
}

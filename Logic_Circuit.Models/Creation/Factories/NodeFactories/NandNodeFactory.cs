using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Nodes;

namespace Logic_Circuit.Models.Factories
{
    /// <summary>
    /// Returns a new NandNode.
    /// </summary>
    public class NandNodeFactory : INodeFactory
    {
        public INode GetNode(string name, string type)
        {
             return new NandNode(name);
        }
    }
}

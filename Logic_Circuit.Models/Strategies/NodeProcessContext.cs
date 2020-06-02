using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Strategies.NodeProcessStrategies;

namespace Logic_Circuit.Models.Strategies
{
    /// <summary>
    /// Applies the selected nodeProcessStrategy.
    /// </summary>
    public class NodeProcessContext
    {
        private readonly INodeProcessStrategy nodeProcessStrategy;

        public NodeProcessContext(INodeProcessStrategy nodeProcessStrategy)
        {
            this.nodeProcessStrategy = nodeProcessStrategy;
        }

        public bool[] ProcessInput(CircuitNode node)
        {
            return nodeProcessStrategy.ProcessInput(node);
        }
    }
}

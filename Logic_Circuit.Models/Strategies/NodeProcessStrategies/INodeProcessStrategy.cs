using Logic_Circuit.Models.BaseNodes;

namespace Logic_Circuit.Models.Strategies.NodeProcessStrategies
{
    /// <summary>
    /// Defines a function for processing subCircuits, in CircuitNodes.
    /// </summary>
    public interface INodeProcessStrategy
    {
        bool[] ProcessInput(CircuitNode node);
    }
}

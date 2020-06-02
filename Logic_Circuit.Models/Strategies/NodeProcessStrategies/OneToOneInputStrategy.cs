using Logic_Circuit.Models.BaseNodes;
using System.Linq;

namespace Logic_Circuit.Models.Strategies.NodeProcessStrategies
{
    /// <summary>
    /// Processes a subCircuit with one input and one output.
    /// </summary>
    public class OneToOneInputStrategy : INodeProcessStrategy
    {
        public bool[] ProcessInput(CircuitNode node)
        {
            node.Circuit.Reset();

            (node.Circuit.InputNodes.First().Value).Value = node.Inputs.First().Process()[0];

            return new bool[] { node.Circuit.OutputNodes.First().Value.Process()[0] };
        }
    }
}

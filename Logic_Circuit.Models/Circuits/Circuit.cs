using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Nodes.NodeInputTypes;
using System.Collections.Generic;

namespace Logic_Circuit.Models.Circuits
{
    /// <summary>
    /// Contains input, output, and internal nodes.
    /// </summary>
    public class Circuit : IClonable<Circuit>
    {
        public string Name { get; set; }

        public Dictionary<string, InputNode> InputNodes { get; private set; } = new Dictionary<string, InputNode>();
        public Dictionary<string, OutputNode> OutputNodes { get; private set; } = new Dictionary<string, OutputNode>();

        public Dictionary<string, INode> Nodes { get; private set; } = new Dictionary<string, INode>();

        public void Reset()
        {
            foreach (InputNode inputNode in InputNodes.Values)
            {
                inputNode.Reset();
            }
        }

        public Circuit Clone()
        {
            CircuitBuilder circuitBuilder = new CircuitBuilder();
            circuitBuilder.SetName(Name);

            foreach (var node in Nodes)
            {
                circuitBuilder.AddNode(node.Value.Name, node.Value.Type);
            }

            foreach (var node in Nodes)
            {
                if (node.Value is ISingleInput)
                {
                    circuitBuilder.AddConnection(((ISingleInput)node.Value).Input.Name, node.Value.Name);
                }
                else if (node.Value is IMultipleInputs)
                {
                    foreach (var inputNode in ((IMultipleInputs)node.Value).Inputs)
                    {
                        circuitBuilder.AddConnection(inputNode.Name, node.Value.Name);
                    }
                }
            }

            return circuitBuilder.GetCircuit();
        }
    }
}

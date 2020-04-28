using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.Circuits
{
    public class Circuit : IClonable<Circuit>
    {
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

            foreach (var node in Nodes)
            {
                circuitBuilder.AddNode(node.Value.Name, node.Value.Type);
            }

            foreach (var connectionString in parser.GetConnectionString(fileName))
            {
                foreach (string outputNode in connectionString.outputs)
                {
                    circuitBuilder.AddConnection(connectionString.input, outputNode);
                }
            }

            return circuitBuilder.GetCircuit();
        }
    }
}

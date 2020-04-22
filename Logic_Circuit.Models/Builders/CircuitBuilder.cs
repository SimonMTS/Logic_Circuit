using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.Circuits
{
    public class CircuitBuilder
    {
        private Circuit circuit = new Circuit();
        private NodeFactory nodeFactory = new NodeFactory();

        public void AddNode(string nodeName, string nodeType)
        {
            INode node = nodeFactory.GetNode(nodeName, nodeType);

            if (node is OutputNode) circuit.OutputNodes.Add(nodeName, (OutputNode)node);
            if (node is InputNode) circuit.InputNodes.Add(nodeName, (InputNode)node);

            circuit.Nodes.Add(nodeName, node);
        }

        public void AddConnection(string inputNode, string outputNode)
        {
            INode input = circuit.Nodes[inputNode];
            INode output = circuit.Nodes[outputNode];

            if (output is CircuitNode)
            {
                ((CircuitNode)output).Inputs.Add(input);
            }
            else
            {
                ((OutputNode)output).Input = input;
            }
        }

        public Circuit GetCircuit()
        {
            return circuit;
        }
    }
}

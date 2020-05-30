﻿using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Factories;
using Logic_Circuit.Models.Nodes;
using Logic_Circuit.Models.Nodes.NodeInputTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.Circuits
{
    public class CircuitBuilder
    {
        private readonly Circuit circuit = new Circuit();
        private readonly NodeFactory nodeFactory = new NodeFactory();

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

            if (output is IMultipleInputs)
            {
                ((IMultipleInputs)output).Inputs.Add(input);
            }
            else
            {
                ((ISingleInput)output).Input = input;
            }
        }

        public void SetName(string name)
        {
            circuit.Name = name;
        }

        public Circuit GetCircuit()
        {
            if (circuit.Name == null)
            {
                throw new InvalidOperationException();
            }

            return circuit;
        }
    }
}

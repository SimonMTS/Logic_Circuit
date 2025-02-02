﻿using Logic_Circuit.Models.BaseNodes;
using System.Collections.Generic;
using System.Linq;

namespace Logic_Circuit.Models.Strategies.NodeProcessStrategies
{
    /// <summary>
    /// Processes a subCircuit with N inputs and one output.
    /// </summary>
    public class NToOneInputStrategy : INodeProcessStrategy
    {
        public bool[] ProcessInput(CircuitNode node)
        {
            node.Circuit.Reset();

            List<bool> inputValuesList = new List<bool>();
            for (int i = 0; i < node.Inputs.Count; i++)
            {
                if (node.Inputs[i] is CircuitNode)
                {
                    int ret = ((CircuitNode)node.Inputs[i]).RetrievedInputsIncr(node.Name + i);

                    inputValuesList.Add(node.Inputs[i].Process()[ret]);
                }
                else
                {
                    inputValuesList.Add(node.Inputs[i].Process()[0]);
                }
            }
            bool[] inputValues = inputValuesList.ToArray();

            int j = 0;
            foreach (InputNode inputNode in node.Circuit.InputNodes.Values)
            {
                if (inputValues.Length <= j) break;

                inputNode.Value = inputValues[j];
                j++;
            }

            return new bool[] { node.Circuit.OutputNodes.First().Value.Process()[0] };
        }
    }
}

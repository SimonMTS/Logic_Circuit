using Logic_Circuit.Models.BaseNodes;
using System.Collections.Generic;

namespace Logic_Circuit.Models.Strategies.NodeProcessStrategies
{
    /// <summary>
    /// Processes a subCircuit with N inputs and N outputs.
    /// </summary>
    public class NToNInputStrategy : INodeProcessStrategy
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

            List<bool> outputArray = new List<bool>();
            foreach (OutputNode outputNode in node.Circuit.OutputNodes.Values)
            {
                outputArray.Add(outputNode.Process()[0]);
            }

            return outputArray.ToArray();
        }
    }
}

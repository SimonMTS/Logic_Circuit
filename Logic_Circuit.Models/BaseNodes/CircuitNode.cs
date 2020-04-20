using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.BaseNodes
{
    public class CircuitNode : INode
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Circuit Circuit { get; set; }

        public List<INode> Inputs { get; set; } = new List<INode>();

        public bool Process()
        {
            if ( Type.Equals("NAND") )
            {
                foreach (INode input in Inputs)
                {
                    if (!input.Process())
                    {
                        return true;
                    }
                }

                return false;
            }

            if (Inputs.Count <= Circuit.InputNodes.Count)
            {
                Circuit.Reset();

                bool[] inputValues = new bool[Inputs.Count];
                for (int i = 0; i < Inputs.Count; i++)
                {
                    inputValues[i] = Inputs[i].Process();
                }

                int j = 0;
                foreach (InputNode node in Circuit.InputNodes.Values)
                {
                    if (inputValues.Length <= j) break;

                    node.Value = inputValues[j];
                    j++;
                }

                return Circuit.OutputNodes.Values.First().Process();
            }


            throw new NotImplementedException();
        }
    }
}

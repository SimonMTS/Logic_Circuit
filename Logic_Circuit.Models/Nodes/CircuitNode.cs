using Logic_Circuit.Models.Circuits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Logic_Circuit.Models.BaseNodes
{
    public class CircuitNode : INode
    {
        public string Name { get; set; }
        public int RealDepth { get => CalcRealDepth(); set { } }
        public string Type { get; set; }
        public Circuit Circuit { get; set; }
        public List<INode> Inputs { get; set; } = new List<INode>();

        private Dictionary<string, int> RetrievedInputs = new Dictionary<string, int>();

        public CircuitNode(string name, string type, Circuit circuit)
        {
            Name = name;
            Type = type;
            Circuit = circuit;
        }

        private int CalcRealDepth()
        {
            int highestInternal = 0;
            if (Circuit != null)
            {
                foreach (INode input in Circuit.OutputNodes.Values)
                {
                    if (input.RealDepth > highestInternal) highestInternal = input.RealDepth;
                }
            }

            int highest = 0;
            foreach (INode input in Inputs)
            {
                if (input.RealDepth > highest) highest = input.RealDepth;
            }

            if (Circuit == null) highest++;

            return highestInternal + highest;
        }

        public int RetrievedInputsIncr(string name)
        {
            if (Circuit == null || Circuit.OutputNodes.Count == 1) return 0;

            if (!RetrievedInputs.ContainsKey(name))
            {
                RetrievedInputs.Add(name, RetrievedInputs.Count);
            }

            return RetrievedInputs[name];
        }

        public bool[] Process()
        {
            if ( Type.Equals("NAND") )
            {
                foreach (INode input in Inputs)
                {
                    foreach (bool res in input.Process())
                    {
                        if (!res)
                        {
                            return new bool[] { true };
                        }
                    }

                }

                return new bool[] { false };
            }

            if (Inputs.Count <= Circuit.InputNodes.Count)
            {
                Circuit.Reset();

                List<bool> inputValuesList = new List<bool>();
                for (int i = 0; i < Inputs.Count; i++)
                {
                    if (Inputs[i] is CircuitNode)
                    {
                        int ret = ((CircuitNode)Inputs[i]).RetrievedInputsIncr(this.Name+i);

                        inputValuesList.Add(Inputs[i].Process()[ret]);
                    }
                    else
                    {
                        inputValuesList.Add(Inputs[i].Process()[0]);
                    }
                }
                bool[] inputValues = inputValuesList.ToArray();

                int j = 0;
                foreach (InputNode node in Circuit.InputNodes.Values)
                {
                    if (inputValues.Length <= j) break;

                    node.Value = inputValues[j];
                    j++;
                }

                List<bool> outputArray = new List<bool>();
                foreach (OutputNode outputNode in Circuit.OutputNodes.Values)
                {
                    outputArray.Add(outputNode.Process()[0]);
                }

                return outputArray.ToArray();
            }


            throw new NotImplementedException();
        }

        public Brush GetDisplayableValue(Brush ifTrue, Brush ifFalse, Brush ifMixed)
        {
            bool[] res = Process();

            if (res.Length == 1)
            {
                return res[0] ? ifTrue : ifFalse;
            }
            else
            {
                bool allTheSame = true;

                for (int i = 1; i < res.Length; i++)
                {
                    if (res[i] != res[i - 1])
                    {
                        allTheSame = false;
                        break;
                    }
                }

                if (allTheSame)
                {
                    return res[0] ? ifTrue : ifFalse;
                }
                else
                {
                    return ifMixed;
                }
            }
        }
    }
}

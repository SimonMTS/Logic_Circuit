using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Nodes.NodeInputTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Logic_Circuit.Models.Nodes
{
    public class NandNode : IMultipleInputs
    {
        public string Name { get; set; }
        public string Type { get => "NAND"; set { } }
        public int RealDepth { get => CalcRealDepth(); set { } }
        public List<INode> Inputs { get; set; } = new List<INode>();

        public NandNode(string name)
        {
            Name = name;
        }

        private int CalcRealDepth()
        {
            int highest = 0;
            foreach (INode input in Inputs)
            {
                if (input.RealDepth > highest) highest = input.RealDepth;
            }

            return highest + 1;
        }

        public bool[] Process()
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

        public Brush GetDisplayableValue(Brush ifTrue, Brush ifFalse, Brush ifMixed)
        {
            bool[] res = Process();

            return res[0] ? ifTrue : ifFalse;
        }

        public INode Clone()
        {
            return new NandNode(this.Name);
        }
    }
}

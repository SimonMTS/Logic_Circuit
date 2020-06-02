using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Nodes.NodeInputTypes;
using System.Collections.Generic;
using System.Windows.Media;

namespace Logic_Circuit.Models.Nodes
{
    /// <summary>
    /// The only real logic node, representing a NAND-GATE.
    /// </summary>
    public class NandNode : IMultipleInputs
    {
        public string Name { get; set; }
        public string UID { get; set; }
        public string Type { get => "NAND"; set { } }
        public int RealDepth { get => CalcRealDepth(); set { } }
        public List<INode> Inputs { get; set; } = new List<INode>();

        public NandNode(string name)
        {
            Name = name;
            UID = "name(" + name + ")_UID(" + Cache.GetUniqueInt() + ")";
        }

        private int CalcRealDepth()
        {
            int result;
            if ((result = Cache.GetDepth(UID)) != int.MinValue)
            {
                return result;
            }

            int highest = 0;
            foreach (INode input in Inputs)
            {
                if (input.RealDepth > highest) highest = input.RealDepth;
            }

            Cache.PushDepth(UID, highest + 1);
            return highest + 1;
        }

        public bool[] Process()
        {
            bool[] result;
            if ((result = Cache.Get(UID)) != null)
            {
                return result;
            }

            foreach (INode input in Inputs)
            {
                foreach (bool res in input.Process())
                {
                    if (!res)
                    {
                        Cache.Push(UID, new bool[] { true });
                        return new bool[] { true };
                    }
                }

            }

            Cache.Push(UID, new bool[] { false });
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

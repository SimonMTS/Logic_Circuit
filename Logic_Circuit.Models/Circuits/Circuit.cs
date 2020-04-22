using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.Circuits
{
    public class Circuit : IClonable
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
    }
}

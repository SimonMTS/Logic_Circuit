using Logic_Circuit.Models.BaseNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models
{
    public class Circuit
    {
        public static string[] SubCircuits;

        public Dictionary<string, InputNode> InputNodes { get; private set; } = new Dictionary<string, InputNode>();
        public Dictionary<string, OutputNode> OutputNodes { get; private set; } = new Dictionary<string, OutputNode>();

        public Dictionary<string, INode> Nodes { get; private set; } = new Dictionary<string, INode>();


        public void AddNode( InputNode node )
        {
            Nodes.Add(node.Name, node);
            InputNodes.Add(node.Name, node);
        }

        public void AddNode( OutputNode node )
        {
            Nodes.Add(node.Name, node);
            OutputNodes.Add(node.Name, node);
        }

        public void AddNode( CircuitNode node )
        {
            Nodes.Add(node.Name, node);
        }

        public void Reset()
        {
            foreach (InputNode inputNode in InputNodes.Values)
            {
                inputNode.Reset();
            }
        }
    }
}

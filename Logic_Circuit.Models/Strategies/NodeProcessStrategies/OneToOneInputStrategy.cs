using Logic_Circuit.Models.BaseNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.Strategies.NodeProcessStrategies
{
    class OneToOneInputStrategy : INodeProcessStrategy
    {
        public bool[] ProcessInput(CircuitNode node)
        {
            node.Circuit.Reset();

            (node.Circuit.InputNodes.First().Value).Value = node.Inputs.First().Process()[0];

            return new bool[] { node.Circuit.OutputNodes.First().Value.Process()[0] };
        }
    }
}

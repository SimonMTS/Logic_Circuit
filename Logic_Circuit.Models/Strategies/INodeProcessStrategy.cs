using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Strategies.NodeProcessStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.Strategies
{
    class NodeProcessContext
    {
        private INodeProcessStrategy _nodeProcessStrategy;

        public NodeProcessContext(INodeProcessStrategy nodeProcessStrategy)
        {
            this._nodeProcessStrategy = nodeProcessStrategy;
        }

        public bool[] ProcessInput(CircuitNode node)
        {
            return _nodeProcessStrategy.ProcessInput(node);
        }
    }
}

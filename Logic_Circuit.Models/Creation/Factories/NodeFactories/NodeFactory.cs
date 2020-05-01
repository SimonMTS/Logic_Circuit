using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Factories.NodeFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.Factories
{
    class NodeFactory : INodeFactory
    {
        private readonly INodeFactory inputNodeFactory = new InputNodeFactory();
        private readonly INodeFactory outputNodeFactory = new OutputNodeFactory();
        private readonly INodeFactory circuitNodeFactory = new CircuitNodeFactory();
        private readonly INodeFactory nandNodeFactory = new NandNodeFactory();

        public INode GetNode(string name, string type)
        {
            if (type.Equals("INPUT_HIGH") || type.Equals("INPUT_LOW"))
            {
                return inputNodeFactory.GetNode(name, type);
            }
            else if (type.Equals("PROBE"))
            {
                return outputNodeFactory.GetNode(name, type);
            }
            else if (type.Equals("NAND"))
            {
                return nandNodeFactory.GetNode(name, type);
            }
            else
            {
                return circuitNodeFactory.GetNode(name, type);
            }
        }
    }
}

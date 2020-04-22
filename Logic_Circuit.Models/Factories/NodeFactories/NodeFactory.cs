using Logic_Circuit.Models.BaseNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.Factories
{
    class NodeFactory : INodeFactory
    {
        InputNodeFactory inputNodeFactory = new InputNodeFactory();
        OutputNodeFactory outputNodeFactory = new OutputNodeFactory();
        CircuitNodeFactory circuitNodeFactory = new CircuitNodeFactory();

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
            else
            {
                return circuitNodeFactory.GetNode(name, type);
            }
        }
    }
}

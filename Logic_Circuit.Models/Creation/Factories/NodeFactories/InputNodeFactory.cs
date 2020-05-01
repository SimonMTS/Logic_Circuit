using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Circuit.Models.BaseNodes;

namespace Logic_Circuit.Models.Factories
{
    class InputNodeFactory : INodeFactory
    {
        public INode GetNode(string name, string type)
        {
            return new InputNode(
                name, 
                type.Contains("HIGH"), 
                type.Contains("HIGH")
            );
        }
    }
}

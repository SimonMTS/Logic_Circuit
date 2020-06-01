using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.Factories
{
    public class NandNodeFactory : INodeFactory
    {
        public INode GetNode(string name, string type)
        {
             return new NandNode(name);
        }
    }
}

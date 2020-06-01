using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Circuit.Models.BaseNodes;

namespace Logic_Circuit.Models.Factories
{
    public class OutputNodeFactory : INodeFactory
    {
        public INode GetNode(string name, string type)
        {
            return new OutputNode(name);
        }
    }
}

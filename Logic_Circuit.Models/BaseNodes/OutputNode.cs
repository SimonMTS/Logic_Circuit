using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.BaseNodes
{
    public class OutputNode : INode
    {
        public string Name { get; set; }

        public INode Input { get; set; }

        public bool Process()
        {
            return Input.Process();
        }
    }
}

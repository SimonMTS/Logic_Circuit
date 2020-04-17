using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.BaseNodes
{
    public interface INode
    {
        string Name { get; set; }

        bool Process();
    }
}

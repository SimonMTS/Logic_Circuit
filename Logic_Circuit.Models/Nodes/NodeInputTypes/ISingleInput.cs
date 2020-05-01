using Logic_Circuit.Models.BaseNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.Nodes.NodeInputTypes
{
    public interface ISingleInput: INode
    {
        INode Input { get; set; }
    }
}

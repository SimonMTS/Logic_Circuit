using Logic_Circuit.Models.BaseNodes;
using System.Collections.Generic;

namespace Logic_Circuit.Models.Nodes.NodeInputTypes
{
    /// <summary>
    /// Defines a Node with multiple input nodes.
    /// </summary>
    public interface IMultipleInputs: INode
    {
        List<INode> Inputs { get; set; }
    }
}

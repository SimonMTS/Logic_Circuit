using Logic_Circuit.Models.BaseNodes;

namespace Logic_Circuit.Models.Nodes.NodeInputTypes
{
    /// <summary>
    /// Defines a node with a single input node.
    /// </summary>
    public interface ISingleInput: INode
    {
        INode Input { get; set; }
    }
}

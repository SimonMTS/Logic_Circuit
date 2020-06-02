using Logic_Circuit.Models.BaseNodes;

namespace Logic_Circuit.Models.Factories
{
    /// <summary>
    /// Defines functions that all NodeFactories share.
    /// </summary>
    public interface INodeFactory
    {
        INode GetNode(string name, string type);
    }
}

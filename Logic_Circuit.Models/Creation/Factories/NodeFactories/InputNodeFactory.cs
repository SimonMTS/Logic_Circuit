using Logic_Circuit.Models.BaseNodes;

namespace Logic_Circuit.Models.Factories
{
    /// <summary>
    /// Returns a new InputNode, with correct value/defaultValue.
    /// </summary>
    public class InputNodeFactory : INodeFactory
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

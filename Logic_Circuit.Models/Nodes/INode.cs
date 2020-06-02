using Logic_Circuit.Models.Circuits;
using System.Windows.Media;

namespace Logic_Circuit.Models.BaseNodes
{
    /// <summary>
    /// Defines properties/functions of every node.
    /// </summary>
    public interface INode : IClonable<INode>
    {
        string Name { get; set; }
        string Type { get; set; }
        int RealDepth { get; set; }

        bool[] Process();

        Brush GetDisplayableValue(Brush ifTrue, Brush ifFalse, Brush ifMixed);
    }
}

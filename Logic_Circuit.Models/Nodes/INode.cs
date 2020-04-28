using Logic_Circuit.Models.Circuits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Logic_Circuit.Models.BaseNodes
{
    public interface INode : IClonable<INode>
    {
        string Name { get; set; }
        string Type { get; set; }
        int RealDepth { get; set; }

        bool[] Process();

        Brush GetDisplayableValue(Brush ifTrue, Brush ifFalse, Brush ifMixed);
    }
}

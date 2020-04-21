using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Logic_Circuit.Models.BaseNodes
{
    public class OutputNode : INode
    {
        public string Name { get; set; }
        public int RealDepth { get => Input.RealDepth; set { } }

        public INode Input { get; set; }

        public bool[] Process()
        {
            if (Input is CircuitNode)
            {
                int ret = ((CircuitNode)Input).RetrievedInputsIncr(this.Name);

                return new bool[] { Input.Process()[ret] };
            }
            else
            {
                return new bool[] { Input.Process()[0] };
            }
        }

        public Brush GetDisplayableValue(Brush ifTrue, Brush ifFalse, Brush ifMixed)
        {
            return Process()[0] ? ifTrue : ifFalse;
        }
    }
}

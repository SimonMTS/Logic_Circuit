using Logic_Circuit.Models.Nodes.NodeInputTypes;
using System.Windows.Media;

namespace Logic_Circuit.Models.BaseNodes
{
    /// <summary>
    /// A node that defines the outputs of a circuit.
    /// </summary>
    public class OutputNode : ISingleInput
    {
        public string Name { get; set; }
        public string UID { get; set; }
        public string Type { get => "PROBE"; set { } }
        public int RealDepth { get => Input.RealDepth; set { } }

        public INode Input { get; set; }

        public OutputNode(string name)
        {
            Name = name;
            UID = "name(" + name + ")_UID(" + Cache.GetUniqueInt() + ")";
        }

        public bool[] Process()
        {
            bool[] result;
            if ((result = Cache.Get(UID)) != null)
            {
                return result;
            }

            if (Input is CircuitNode)
            {
                int ret = ((CircuitNode)Input).RetrievedInputsIncr(this.Name);

                result = new bool[] { Input.Process()[ret] };
                Cache.Push(UID, result);
                return result;
            }
            else
            {
                result = new bool[] { Input.Process()[0] };
                Cache.Push(UID, result);
                return result;
            }
        }

        public Brush GetDisplayableValue(Brush ifTrue, Brush ifFalse, Brush ifMixed)
        {
            return Process()[0] ? ifTrue : ifFalse;
        }

        public INode Clone()
        {
            return new OutputNode(this.Name);
        }
    }
}

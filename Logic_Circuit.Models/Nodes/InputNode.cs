using System.Windows.Media;

namespace Logic_Circuit.Models.BaseNodes
{
    /// <summary>
    /// A node whose value can be changed by the user, or by a circuitNode if it is contained in a subCircuit.
    /// </summary>
    public class InputNode : INode
    {
        public string Name { get; set; }
        public string Type { get => Value ? "INPUT_HIGH" : "INPUT_LOW"; set { } }
        public int RealDepth { get => 0; set { } }

        public bool Value { get; set; }
        public bool DefaultValue { get; set; }

        public InputNode(string name, bool value, bool defaultValue)
        {
            Name = name;
            Value = value;
            DefaultValue = defaultValue;
        }

        public bool[] Process()
        {
            return new bool[] { Value };
        }

        public Brush GetDisplayableValue(Brush ifTrue, Brush ifFalse, Brush ifMixed)
        {
            return Value ? ifTrue : ifFalse;
        }

        public void Reset()
        {
            Value = DefaultValue;
        }

        public INode Clone()
        {
            return new InputNode(this.Name, this.DefaultValue, this.DefaultValue);
        }
    }
}

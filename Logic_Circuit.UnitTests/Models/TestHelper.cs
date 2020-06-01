using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Circuits;
using Logic_Circuit.Models.Factories;
using Logic_Circuit.Parser.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Logic_Circuit.UnitTests.Models
{
    class TestHelper
    {
        public static Circuit GetFullAdderCircuit()
        {
            SetTestPaths();
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            return CircuitFactory.GetFromFile(filePath + "../../../../Circuits/Circuit1_FullAdder.txt").circuit;
        }

        public static void SetTestPaths()
        {
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Validator.InternalCircuitNamesForTests = new string[] {
                "INPUT_HIGH", "INPUT_LOW", "PROBE", "NAND", "OR", "AND", "NOT"
            };

            CircuitNodeFactory.DifferentPathForTests = filePath + "../../../../Internal_Circuits/";
        }
    }

    class FakeNode : INode
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int RealDepth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        private readonly bool Value;

        public FakeNode(bool v)
        {
            Value = v;
        }

        public INode Clone()
        {
            throw new NotImplementedException();
        }

        public Brush GetDisplayableValue(Brush ifTrue, Brush ifFalse, Brush ifMixed)
        {
            throw new NotImplementedException();
        }

        public bool[] Process()
        {
            return new bool[] { Value };
        }
    }
}

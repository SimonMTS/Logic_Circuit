using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Circuits;
using Logic_Circuit.Parser;

namespace Logic_Circuit.Models.Factories
{
    public class CircuitNodeFactory : INodeFactory
    {
        public INode GetNode(string name, string type)
        {
            string currentDir;

            if (DifferentPathForTests == null) {
                currentDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                currentDir += "../../../../Internal_Circuits/";
            }
            else
            {
                currentDir = DifferentPathForTests;
            }

            var circuit = CircuitFactory.GetFromFile(currentDir + type + ".txt");

            return new CircuitNode(
                name,
                type,
                circuit.circuit
            );
        }

        public static string DifferentPathForTests = null;
    }
}

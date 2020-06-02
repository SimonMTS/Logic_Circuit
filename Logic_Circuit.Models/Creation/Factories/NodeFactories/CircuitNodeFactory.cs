using Logic_Circuit.Models.BaseNodes;

namespace Logic_Circuit.Models.Factories
{

    /// <summary>
    /// Returns a new CircuitNode, with a Circuit retrieved by CircuitFactory.
    /// </summary>
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

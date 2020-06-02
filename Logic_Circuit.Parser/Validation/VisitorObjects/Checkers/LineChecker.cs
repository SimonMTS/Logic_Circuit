using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Logic_Circuit.Parser.Validation.VisitorObjects
{
    /// <summary>
    /// Checks the syntax of a line, and existence of used nodeTypes.
    /// </summary>
    public class LineChecker : ValidationVisitor
    {
        private readonly List<string> NodeNames = new List<string>();

        public override (bool success, string validationError) VisitConnectionLine(ConnectionLine connectionLine)
        {
            // valid line syntax
            var regex = new Regex(@"(^#.*$)|((^\w+):(\s*)(\w+,)*(\w+;$))");
            if (!regex.Match(connectionLine.Line).Success)
            {
                return (false, "Line doesn't conform to syntax: '" + connectionLine.Line + "'");
            }

            // nodeName validation
            string nodeName = connectionLine.Line.Split(':')[0];
            if (!NodeNames.Contains(nodeName))
            {
                return (false, "Node wasn't defined: '" + connectionLine.Line + "'");
            }

            return (true, "");
        }

        public override (bool success, string validationError) VisitNodeLine(NodeLine nodeLine)
        {
            // valid line syntax
            var regex = new Regex(@"(^#.*$)|((^\w+):(\s*)(\w+,)*(\w+;$))");
            if (!regex.Match(nodeLine.Line).Success)
            {
                return (false, "Line doesn't conform to syntax: '" + nodeLine.Line + "'");
            }

            string type = nodeLine.Line.Split(':')[1].Trim().Replace(";", "");
            string[] types = GetInternalCircuitNames();
            if (
                !types.Contains(type)
            )
            {
                return (false, "'" + type + "' is not a valid node type.");
            }

            NodeNames.Add(nodeLine.Line.Split(':')[0]);
            return (true, "");
        }

        private string[] InternalCircuitNames = null;
        private string[] GetInternalCircuitNames()
        {
            if (InternalCircuitNames != null)
            {
                return InternalCircuitNames;
            }

            List<string> names = new List<string>();

            string currentDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            currentDir += "../../../../Internal_Circuits";

            string[] fileEntries = Directory.GetFiles(currentDir);
            foreach (string fileName in fileEntries)
            {
                names.Add(Path.GetFileName(fileName).Replace(".txt", ""));
            }

            names.Add("INPUT_HIGH");
            names.Add("INPUT_LOW");
            names.Add("PROBE");
            names.Add("NAND");

            InternalCircuitNames = names.ToArray();

            return InternalCircuitNames;
        }

        public void SetInternalCircuitNamesForTests(string[] names) {
            InternalCircuitNames = names;
        }
    }
}

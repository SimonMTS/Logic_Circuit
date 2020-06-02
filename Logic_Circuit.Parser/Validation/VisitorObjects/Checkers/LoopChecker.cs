using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Logic_Circuit.Parser.Validation.VisitorObjects
{
    /// <summary>
    /// Checks if there are any loops between this node and the output nodes.
    /// </summary>
    public class LoopChecker : ValidationVisitor
    {
        private readonly string WholeFile;

        public LoopChecker(string content)
        {
            WholeFile = content;
        }

        public override (bool success, string validationError) VisitConnectionLine(ConnectionLine connectionLine)
        {
            if (!RecurseToOutput(connectionLine.Line, connectionLine.Line, WholeFile.Split('\n').Length, 0))
            {
                return (false, "Node: '" + connectionLine.Line.Split(':')[0] + "' leads to an infinite loop.");
            }

            return (true, "");
        }

        public override (bool success, string validationError) VisitNodeLine(NodeLine nodeLine)
        {
            return (true, "");
        }

        private bool RecurseToOutput(string constantNode, string tmpNode, int maxDepth, int depth)
        {
            // node is ouptut so return true
            if (tmpNode.Contains("PROBE;")) return true;

            // node has itself as a dependency so return false
            string[] parsedTmpNode = Regex.Replace(tmpNode, @"\s+", "").Replace(";", "").Split(':');
            string[] parsedConstantNode = Regex.Replace(constantNode, @"\s+", "").Replace(";", "").Split(':');
            if (depth > 0 && parsedTmpNode[0].Equals(parsedConstantNode[0])) return false;

            // we must be stuck in an infinite loop so return false
            if (depth > maxDepth) return false;

            // node is not input so check children
            List<string> parentNodes = new List<string>();

            string[] parentNodeNames = parsedTmpNode[1].Split(',');
            string[] lines = WholeFile.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string parentNodeName in parentNodeNames)
            {
                string results = lines.Where(l => l.StartsWith(parentNodeName + ":")).ToList().Last();
                parentNodes.Add(results);
            }

            foreach (string parentNode in parentNodes)
            {
                if (!RecurseToOutput(constantNode, parentNode, maxDepth, depth + 1))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

using System.Text.RegularExpressions;

namespace Logic_Circuit.Parser.Validation.VisitorObjects
{
    /// <summary>
    /// Checks for hanging connections.
    /// </summary>
    public class HangingConnectionChecker : ValidationVisitor
    {
        private readonly string WholeFile;

        public HangingConnectionChecker(string content)
        {
            WholeFile = content;
        }

        public override (bool success, string validationError) VisitConnectionLine(ConnectionLine connectionLine)
        {
            return (true, "");
        }

        public override (bool success, string validationError) VisitNodeLine(NodeLine nodeLine)
        {
            string nodeName = nodeLine.Line.Split(':')[0];
            int occurrences = new Regex(Regex.Escape(nodeName + ":")).Matches(WholeFile).Count;

            if (
                !nodeLine.Line.Contains("PROBE") && 
                !nodeLine.Line.Contains("INPUT_HIGH") && 
                !nodeLine.Line.Contains("INPUT_LOW") && 
                occurrences < 2
            )
            {
                return (false, nodeName + " has no outputs.");
            }
            
            return (true, "");
        }
    }
}

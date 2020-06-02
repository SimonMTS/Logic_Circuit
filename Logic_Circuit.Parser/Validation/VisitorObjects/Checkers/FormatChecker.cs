using System;

namespace Logic_Circuit.Parser.Validation.VisitorObjects
{
    /// <summary>
    /// Checks the format of the file, mainly the placement of the seperation newline.
    /// </summary>
    public class FormatChecker : ValidationVisitor
    {
        private readonly string WholeFile;

        public FormatChecker(string content)
        {
            WholeFile = content;
        }

        public override (bool success, string validationError) VisitConnectionLine(ConnectionLine connectionLine)
        {
            if (!AfterEmptyLine(connectionLine.Line))
            {
                return (false, "Encountered connection before empty line. (line: '" + connectionLine.Line + "')");
            }

            return (true, "");
        }

        public override (bool success, string validationError) VisitNodeLine(NodeLine nodeLine)
        {
            if (AfterEmptyLine(nodeLine.Line))
            {
                return (false, "Encountered node after empty line. (line: '" + nodeLine.Line + "')");
            }

            return (true, "");
        }

        private bool AfterEmptyLine(string line)
        {
            string[] lines = WholeFile.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (string fileLine in lines)
            {
                if (fileLine.Equals(line))
                {
                    return false;
                }

                if (fileLine.Equals(""))
                {
                    return true;
                }
            }

            // the line wasn't in the file.
            throw new InvalidOperationException();
        }
    }
}

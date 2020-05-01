using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Parser.Validation.VisitorObjects
{
    class LoopChecker : ValidationVisitor
    {
        private readonly string WholeFile;

        public LoopChecker(string content)
        {
            WholeFile = content;
        }

        public override (bool success, string validationError) VisitConnectionLine(ConnectionLine connectionLine)
        {
            return (true, "");
        }

        public override (bool success, string validationError) VisitNodeLine(NodeLine nodeLine)
        {
            if (!RecurseToInput(nodeLine.Line, nodeLine.Line, WholeFile.Split('\n').Length, 0))
            {
                return (false, nodeLine.Line + " has itself as input."); ;
            }

            return (true, "");
        }

        private bool RecurseToInput(string constantNode, string tmpNode, int maxDepth, int depth)
        {
            return true;
        }
    }
}

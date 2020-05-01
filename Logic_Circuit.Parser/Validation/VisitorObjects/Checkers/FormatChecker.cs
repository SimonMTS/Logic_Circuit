using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Parser.Validation.VisitorObjects
{
    class FormatChecker : ValidationVisitor
    {
        public override (bool success, string validationError) VisitConnectionLine(ConnectionLine connectionLine)
        {
            return (true, "");
        }

        public override (bool success, string validationError) VisitNodeLine(NodeLine nodeLine)
        {
            return (true, "");
        }
    }
}

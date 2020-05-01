using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Parser.Validation.VisitorObjects
{
    abstract class ValidationVisitor
    {
        public abstract (bool success, string validationError) VisitNodeLine(NodeLine nodeLine);

        public abstract (bool success, string validationError) VisitConnectionLine(ConnectionLine connectionLine);
    }
}

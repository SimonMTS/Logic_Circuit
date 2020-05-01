using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Parser.Validation.VisitorObjects
{
    class NodeLine : ValidationElement
    {
        public string Line { get; private set; }

        public NodeLine(string line)
        {
            Line = line;
        }

        public override (bool success, string validationError) Accept(ValidationVisitor visitor)
        {
            return visitor.VisitNodeLine(this);
        }
    }
}

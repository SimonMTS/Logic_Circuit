using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Parser.Validation.VisitorObjects
{
    abstract class ValidationElement
    {
        public abstract (bool success, string validationError) Accept(ValidationVisitor visitor);
    }
}

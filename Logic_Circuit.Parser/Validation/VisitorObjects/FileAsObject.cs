using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Parser.Validation.VisitorObjects
{
    class FileAsObject
    {
        private List<ValidationElement> _elements = new List<ValidationElement>();

        public void Attach(ValidationElement element)
        {
            _elements.Add(element);
        }

        public void Detach(ValidationElement element)
        {
            _elements.Remove(element);
        }

        public (bool success, string validationError) Accept(ValidationVisitor visitor)
        {
            foreach (ValidationElement element in _elements)
            {
                (bool success, string validationError) res;
                if (!(res = element.Accept(visitor)).success)
                {
                    return res;
                }
            }

            return (
                success: true,
                validationError: ""
            );
        }
    }
}

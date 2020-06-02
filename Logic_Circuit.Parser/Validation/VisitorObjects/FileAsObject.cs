using System.Collections.Generic;

namespace Logic_Circuit.Parser.Validation.VisitorObjects
{
    /// <summary>
    /// An object to represent an input file, contains Lines.
    /// </summary>
    public class FileAsObject
    {
        private readonly List<ValidationElement> elements = new List<ValidationElement>();

        public void Attach(ValidationElement element)
        {
            elements.Add(element);
        }

        public void Detach(ValidationElement element)
        {
            elements.Remove(element);
        }

        public (bool success, string validationError) Accept(ValidationVisitor visitor)
        {
            foreach (ValidationElement element in elements)
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

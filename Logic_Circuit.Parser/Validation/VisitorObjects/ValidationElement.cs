
namespace Logic_Circuit.Parser.Validation.VisitorObjects
{
    /// <summary>
    /// Defines an element to be validated.
    /// </summary>
    public abstract class ValidationElement
    {
        public abstract (bool success, string validationError) Accept(ValidationVisitor visitor);
    }
}

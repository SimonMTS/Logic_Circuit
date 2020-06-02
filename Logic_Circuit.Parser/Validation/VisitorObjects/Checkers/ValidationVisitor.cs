
namespace Logic_Circuit.Parser.Validation.VisitorObjects
{
    /// <summary>
    /// Defines a visitor used to validate ValidationElements.
    /// </summary>
    public abstract class ValidationVisitor
    {
        public abstract (bool success, string validationError) VisitNodeLine(NodeLine nodeLine);

        public abstract (bool success, string validationError) VisitConnectionLine(ConnectionLine connectionLine);
    }
}

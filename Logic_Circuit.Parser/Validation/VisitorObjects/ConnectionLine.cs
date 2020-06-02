
namespace Logic_Circuit.Parser.Validation.VisitorObjects
{
    /// <summary>
    /// A line from the input file representing the declaration of connections between nodes.
    /// </summary>
    public class ConnectionLine : ValidationElement
    {
        public string Line { get; private set; }

        public ConnectionLine(string line)
        {
            Line = line;
        }

        public override (bool success, string validationError) Accept(ValidationVisitor visitor)
        {
            return visitor.VisitConnectionLine(this);
        }
    }
}

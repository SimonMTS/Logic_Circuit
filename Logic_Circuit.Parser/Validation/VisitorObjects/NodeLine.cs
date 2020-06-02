
namespace Logic_Circuit.Parser.Validation.VisitorObjects
{
    /// <summary>
    /// A line from the input file representing the declaration of a node.
    /// </summary>
    public class NodeLine : ValidationElement
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

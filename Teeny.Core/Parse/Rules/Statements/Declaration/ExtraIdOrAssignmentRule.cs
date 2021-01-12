using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.Declaration
{
    public class ExtraIdOrAssignmentRule
    {
        public ExtraIdOrAssignmentRule(TokenRecord comma, IdOrAssignmentRule idOrAssignment,
            ExtraIdOrAssignmentRule extraIdOrAssign)
        {
            Comma.Assign(comma);
            IdOrAssignment = idOrAssignment;
            ExtraIdOrAssign = extraIdOrAssign;
        }

        public ExtraIdOrAssignmentRule()
        {
        }

        public TerminalNode Comma { get; set; } = new TerminalNode(Token.Comma);
        public IdOrAssignmentRule IdOrAssignment { get; set; }
        public ExtraIdOrAssignmentRule ExtraIdOrAssign { get; set; }
    }
}
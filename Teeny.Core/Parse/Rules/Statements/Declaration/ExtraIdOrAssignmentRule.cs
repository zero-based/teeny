using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.Declaration
{
    public class ExtraIdOrAssignmentRule : BaseRule
    {
        public ExtraIdOrAssignmentRule(TerminalNode comma, IdOrAssignmentRule idOrAssignment,
            ExtraIdOrAssignmentRule extraIdOrAssign)
        {
            Comma = Guard.OneOf(() => comma, Token.Comma);
            IdOrAssignment = Guard.NonNull(() => idOrAssignment);
            ExtraIdOrAssign = extraIdOrAssign;
        }

        public ExtraIdOrAssignmentRule()
        {
        }

        public TerminalNode Comma { get; set; }
        public IdOrAssignmentRule IdOrAssignment { get; set; }
        public ExtraIdOrAssignmentRule ExtraIdOrAssign { get; set; }
    }
}
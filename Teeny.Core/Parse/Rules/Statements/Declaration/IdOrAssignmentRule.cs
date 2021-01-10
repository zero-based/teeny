using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.Declaration
{
    public class IdOrAssignmentRule
    {
        public IdOrAssignmentRule(TokenRecord identifier)
        {
            Identifier.Assign(identifier);
        }

        public IdOrAssignmentRule(AssignmentStatementRule assignmentStatement)
        {
            AssignmentStatement = assignmentStatement;
        }

        public IdOrAssignmentRule()
        {
        }

        public TerminalNode Identifier { get; set; } = new TerminalNode(Token.Identifier);
        public AssignmentStatementRule AssignmentStatement { get; set; }
    }
}
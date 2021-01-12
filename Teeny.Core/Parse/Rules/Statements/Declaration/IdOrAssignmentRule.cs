using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.Declaration
{
    public class IdOrAssignmentRule : BaseRule
    {
        public IdOrAssignmentRule(TokenRecord identifier)
        {
            Identifier.Assign(identifier);
        }

        public IdOrAssignmentRule(TokenRecord identifier, TokenRecord assignmentOperator, ExpressionRule expression)
        {
            Identifier.Assign(identifier);
            AssignmentOperator.Assign(assignmentOperator);
            Expression = expression;
        }

        public IdOrAssignmentRule()
        {
        }

        public TerminalNode Identifier { get; set; } = new TerminalNode(Token.Identifier);
        public TerminalNode AssignmentOperator { get; set; } = new TerminalNode(Token.Assignment);
        public ExpressionRule Expression { get; set; }
    }
}
using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements
{
    public class AssignmentStatementRule : BaseRule
    {
        public AssignmentStatementRule(TokenRecord identifier, TokenRecord assignmentOperator,
            ExpressionRule expression)
        {
            Identifier.Assign(identifier);
            AssignmentOperator.Assign(assignmentOperator);
            Expression = expression;
        }

        public AssignmentStatementRule()
        {
        }

        public TerminalNode Identifier { get; set; } = new TerminalNode(Token.Identifier);
        public TerminalNode AssignmentOperator { get; set; } = new TerminalNode(Token.Assignment);
        public ExpressionRule Expression { get; set; }
    }
}
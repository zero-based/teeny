using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements
{
    public class AssignmentStatementRule : StatementRule
    {
        public AssignmentStatementRule(TerminalNode identifier, TerminalNode assignmentOperator,
                                       ExpressionRule expression, TerminalNode semicolon)
        {
            Identifier = Guard.OneOf(() => identifier, Token.Identifier);
            AssignmentOperator = Guard.OneOf(() => assignmentOperator, Token.Assignment);
            Expression = expression;
            Semicolon = Guard.OneOf(() => semicolon, Token.Semicolon);
        }

        public AssignmentStatementRule()
        {
        }

        public TerminalNode Identifier { get; set; }
        public TerminalNode AssignmentOperator { get; set; }
        public ExpressionRule Expression { get; set; }
        public TerminalNode Semicolon { get; set; }
    }
}
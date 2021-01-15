using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.Declaration
{
    public class IdOrAssignmentRule : BaseRule
    {
        public IdOrAssignmentRule(TerminalNode identifier)
        {
            Identifier = Guard.OneOf(() => identifier, Token.Identifier);
        }

        public IdOrAssignmentRule(TerminalNode identifier, TerminalNode assignmentOperator, ExpressionRule expression)
        {
            Identifier = Guard.OneOf(() => identifier, Token.Identifier);
            AssignmentOperator = Guard.OneOf(() => assignmentOperator, Token.Assignment);
            Expression = Guard.NonNull(() => expression);
        }

        public IdOrAssignmentRule()
        {
        }

        public TerminalNode Identifier { get; set; }
        public TerminalNode AssignmentOperator { get; set; }
        public ExpressionRule Expression { get; set; }
    }
}
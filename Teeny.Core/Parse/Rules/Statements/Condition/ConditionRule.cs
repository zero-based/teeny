using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.Condition
{
    public class ConditionRule : BaseRule
    {
        public ConditionRule(TokenRecord tokenRecord, TokenRecord conditionOperator, TermRule term)
        {
            Identifier.Assign(tokenRecord);
            ConditionOperator.Assign(conditionOperator);
            Term = term;
        }

        public ConditionRule()
        {
        }

        public TerminalNode Identifier { get; set; } = new TerminalNode(Token.Identifier);
        public TerminalNode ConditionOperator { get; set; } = new TerminalNode(Token.LessThan, Token.GreaterThan, Token.Equal, Token.NotEqual);
        public TermRule Term { get; set; }
    }
}
using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Parse.Rules.Operators;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.Condition
{
    public class ConditionRule : BaseRule
    {
        public ConditionRule(TokenRecord tokenRecord, ConditionOperatorRule conditionOperator, TermRule term)
        {
            Identifier.Assign(tokenRecord);
            ConditionOperator = conditionOperator;
            Term = term;
        }

        public ConditionRule()
        {
        }

        public TerminalNode Identifier { get; set; } = new TerminalNode(Token.Identifier);
        public ConditionOperatorRule ConditionOperator { get; set; }
        public TermRule Term { get; set; }
    }
}
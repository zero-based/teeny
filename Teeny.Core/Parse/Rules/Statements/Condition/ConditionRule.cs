using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.Condition
{
    public class ConditionRule : BaseRule
    {
        public ConditionRule(TerminalNode identifier, TerminalNode conditionOperator, TermRule term)
        {
            Identifier = Guard.OneOf(() => identifier, Token.Identifier);
            ConditionOperator = Guard.OneOf(() => conditionOperator, TokensGroups.ConditionOperators);
            Term = Guard.NonNull(() => term);
        }

        public ConditionRule()
        {
        }

        public TerminalNode Identifier { get; set; }
        public TerminalNode ConditionOperator { get; set; }
        public TermRule Term { get; set; }
    }
}
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.Condition
{
    public class ExtraConditionRule : BaseRule
    {
        public ExtraConditionRule(TerminalNode booleanOperator, ConditionRule condition,
            ExtraConditionRule extraCondition)
        {
            BooleanOperator = Guard.OneOf(() => booleanOperator, TokensGroups.BooleanOperators);
            Condition = Guard.NonNull(() => condition);
            ExtraCondition = extraCondition;
        }

        public ExtraConditionRule()
        {
        }

        public TerminalNode BooleanOperator { get; set; }
        public ConditionRule Condition { get; set; }
        public ExtraConditionRule ExtraCondition { get; set; }
    }
}
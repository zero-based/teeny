using Teeny.Core.Parse.Rules.Operators;

namespace Teeny.Core.Parse.Rules.Statements.Condition
{
    public class ExtraConditionRule
    {
        public ExtraConditionRule(BooleanOperatorRule booleanOperator, ConditionRule condition,
            ExtraConditionRule extraCondition)
        {
            BooleanOperator = booleanOperator;
            Condition = condition;
            ExtraCondition = extraCondition;
        }

        public BooleanOperatorRule BooleanOperator { get; set; }
        public ConditionRule Condition { get; set; }
        public ExtraConditionRule ExtraCondition { get; set; }
    }
}
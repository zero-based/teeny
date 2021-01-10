namespace Teeny.Core.Parse.Rules.Statements.Condition
{
    public class ConditionStatementRule : BaseRule
    {
        public ConditionStatementRule(ConditionRule condition, ExtraConditionRule extraCondition)
        {
            Condition = condition;
            ExtraCondition = extraCondition;
        }

        public ConditionStatementRule()
        {
        }

        public ConditionRule Condition { get; set; }
        public ExtraConditionRule ExtraCondition { get; set; }
    }
}
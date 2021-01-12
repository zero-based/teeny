using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.Condition
{
    public class ExtraConditionRule : BaseRule
    {
        public ExtraConditionRule()
        {
        }

        public ExtraConditionRule(TokenRecord booleanOperator, ConditionRule condition,
            ExtraConditionRule extraCondition)
        {
            BooleanOperator.Assign(booleanOperator);
            Condition = condition;
            ExtraCondition = extraCondition;
        }

        public TerminalNode BooleanOperator { get; set; } = new TerminalNode(Token.And, Token.Or);
        public ConditionRule Condition { get; set; }
        public ExtraConditionRule ExtraCondition { get; set; }
    }
}
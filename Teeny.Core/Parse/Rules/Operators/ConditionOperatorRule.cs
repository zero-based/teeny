using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Operators
{
    public class ConditionOperatorRule : BaseRule
    {
        public ConditionOperatorRule(TokenRecord @operator)
        {
            Operator.Assign(@operator);
        }

        public ConditionOperatorRule()
        {
        }

        public TerminalNode Operator { get; set; } =
            new TerminalNode(Token.LessThan, Token.GreaterThan, Token.Equal, Token.NotEqual);
    }
}
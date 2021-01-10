using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Operators
{
    public class BooleanOperatorRule : BaseRule
    {
        public BooleanOperatorRule(TokenRecord tokenRecord)
        {
            BooleanOperator.Assign(tokenRecord);
        }

        public BooleanOperatorRule()
        {
        }

        public TerminalNode BooleanOperator { get; set; } = new TerminalNode(Token.And, Token.Or);
    }
}
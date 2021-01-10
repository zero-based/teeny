using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Operators
{
    public class ArithmeticOperatorRule : BaseRule
    {
        public ArithmeticOperatorRule(TokenRecord arithmeticOperator)
        {
            ArithmeticOperator.Assign(arithmeticOperator);
        }

        public ArithmeticOperatorRule()
        {
        }

        public TerminalNode ArithmeticOperator { get; set; } =
            new TerminalNode(Token.Plus, Token.Minus, Token.Multiply, Token.Divide);
    }
}
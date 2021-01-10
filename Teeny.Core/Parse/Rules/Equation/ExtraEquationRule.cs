using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Equation
{
    public class ExtraEquationRule : BaseRule
    {
        public ExtraEquationRule(TokenRecord arithmeticOperator, AnyEquationRule anyEquation)
        {
            ArithmeticOperator.Assign(arithmeticOperator);
            AnyEquation = anyEquation;
        }

        public ExtraEquationRule()
        {
        }

        public TerminalNode ArithmeticOperator { get; set; } =
            new TerminalNode(Token.Plus, Token.Minus, Token.Multiply, Token.Divide);

        public AnyEquationRule AnyEquation { get; set; }
    }
}
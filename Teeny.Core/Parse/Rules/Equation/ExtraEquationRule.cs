using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Equation
{
    public class ExtraEquationRule : BaseRule
    {
        public ExtraEquationRule(TokenRecord arithmeticOperator, EquationRule equation, ExtraEquationRule extraEquation)
        {
            ArithmeticOperator.Assign(arithmeticOperator);
            Equation = equation;
            ExtraEquation = extraEquation;
        }

        public ExtraEquationRule()
        {
        }

        public TerminalNode ArithmeticOperator { get; set; } =
            new TerminalNode(Token.Plus, Token.Minus, Token.Multiply, Token.Divide);

        public EquationRule Equation { get; set; }
        public ExtraEquationRule ExtraEquation { get; set; }
    }
}
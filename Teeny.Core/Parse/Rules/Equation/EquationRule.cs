using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Equation
{
    public class EquationRule : BaseRule
    {
        public EquationRule(TokenRecord parenthesisLeft, EquationRule equation1, TokenRecord arithmeticOperator,
            EquationRule equation2, TokenRecord parenthesisRight, ExtraEquationRule extraEquation)
        {
            ParenthesisLeft.Assign(parenthesisLeft);
            Equation1 = equation1;
            ArithmeticOperator.Assign(arithmeticOperator);
            Equation2 = equation2;
            ParenthesisRight.Assign(parenthesisRight);
            ExtraEquation = extraEquation;
        }

        public EquationRule(TermRule term, ExtraEquationRule extraEquation)
        {
            Term = term;
            ExtraEquation = extraEquation;
        }

        public EquationRule()
        {
        }

        public TerminalNode ParenthesisLeft { get; set; } = new TerminalNode(Token.ParenthesisLeft);
        public EquationRule Equation1 { get; set; }

        public TerminalNode ArithmeticOperator { get; set; } =
            new TerminalNode(Token.Plus, Token.Minus, Token.Multiply, Token.Divide);

        public EquationRule Equation2 { get; set; }
        public TerminalNode ParenthesisRight { get; set; } = new TerminalNode(Token.ParenthesisRight);
        public TermRule Term { get; set; }
        public ExtraEquationRule ExtraEquation { get; set; }
    }
}
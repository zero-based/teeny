using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Equation
{
    public class AnyEquationRule : BaseRule
    {
        public AnyEquationRule(TokenRecord parenthesisLeft, NoBracketEquationRule noBracketEquation,
            TokenRecord parenthesisRight)
        {
            ParenthesisLeft.Assign(parenthesisLeft);
            NoBracketEquation = noBracketEquation;
            ParenthesisRight.Assign(parenthesisRight);
        }

        public AnyEquationRule(NoBracketEquationRule noBracketEquation)
        {
            NoBracketEquation = noBracketEquation;
        }

        public AnyEquationRule()
        {
        }

        public TerminalNode ParenthesisLeft { get; set; } = new TerminalNode(Token.ParenthesisLeft);
        public NoBracketEquationRule NoBracketEquation { get; set; }
        public TerminalNode ParenthesisRight { get; set; } = new TerminalNode(Token.ParenthesisRight);
    }
}
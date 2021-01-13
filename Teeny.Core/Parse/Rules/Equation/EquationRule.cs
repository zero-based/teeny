using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Equation
{
    public class EquationRule : BaseRule
    {
        public EquationRule(TokenRecord parenthesisLeft, EquationRule equation, TokenRecord parenthesisRight,
            ExtraEquationRule extraEquation)
        {
            ParenthesisLeft.Assign(parenthesisLeft);
            Equation = equation;
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
        public EquationRule Equation { get; set; }
        public TerminalNode ParenthesisRight { get; set; } = new TerminalNode(Token.ParenthesisRight);
        public TermRule Term { get; set; }
        public ExtraEquationRule ExtraEquation { get; set; }
    }
}
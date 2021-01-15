using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Equation
{
    public class EquationRule : BaseRule
    {
        public EquationRule(TerminalNode parenthesisLeft, EquationRule equation,
            TerminalNode parenthesisRight, ExtraEquationRule extraEquation)
        {
            ParenthesisLeft = Guard.OneOf(() => parenthesisLeft, Token.ParenthesisLeft);
            Equation = Guard.NonNull(() => equation);
            ParenthesisRight = Guard.OneOf(() => parenthesisRight, Token.ParenthesisRight);
            ExtraEquation = extraEquation;
        }

        public EquationRule(TermRule term, ExtraEquationRule extraEquation)
        {
            Term = Guard.NonNull(() => term);
            ExtraEquation = extraEquation;
        }

        public EquationRule()
        {
        }

        public TerminalNode ParenthesisLeft { get; set; }
        public EquationRule Equation { get; set; }
        public TerminalNode ParenthesisRight { get; set; }
        public TermRule Term { get; set; }
        public ExtraEquationRule ExtraEquation { get; set; }
    }
}
using Teeny.Core.Parse.Rules.Equation;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Common
{
    public class ExpressionRule : BaseRule
    {
        public ExpressionRule(TokenRecord @string)
        {
            String.Assign(@string);
        }

        public ExpressionRule(TermRule term)
        {
            Term = term;
        }

        public ExpressionRule(EquationRule equation)
        {
            Equation = equation;
        }

        public TerminalNode String { get; set; } = new TerminalNode(Token.ConstantString);
        public TermRule Term { get; set; }
        public EquationRule Equation { get; set; }
    }
}
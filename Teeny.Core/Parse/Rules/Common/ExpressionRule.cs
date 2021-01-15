using Teeny.Core.Parse.Rules.Equation;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Common
{
    public class ExpressionRule : BaseRule
    {
        public ExpressionRule(TerminalNode @string)
        {
            String = Guard.OneOf(() => @string, Token.ConstantString);
        }

        public ExpressionRule(TermRule term)
        {
            Term = Guard.NonNull(() => term);
        }

        public ExpressionRule(EquationRule equation)
        {
            Equation = Guard.NonNull(() => equation);
        }

        public ExpressionRule()
        {
        }

        public TerminalNode String { get; set; }
        public TermRule Term { get; set; }
        public EquationRule Equation { get; set; }
    }
}
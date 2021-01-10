using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Parse.Rules.Operators;

namespace Teeny.Core.Parse.Rules.Equation
{
    public class NoBracketEquationRule : BaseRule
    {
        public NoBracketEquationRule(TermRule term1, ArithmeticOperatorRule arithmeticOperator, TermRule term2,
            ExtraTermRule extraTerm)
        {
            Term1 = term1;
            ArithmeticOperator = arithmeticOperator;
            Term2 = term2;
            ExtraTerm = extraTerm;
        }

        public TermRule Term1 { get; set; }
        public ArithmeticOperatorRule ArithmeticOperator { get; set; }
        public TermRule Term2 { get; set; }
        public ExtraTermRule ExtraTerm { get; set; }
    }
}
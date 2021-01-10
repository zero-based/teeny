using Teeny.Core.Parse.Rules.Operators;

namespace Teeny.Core.Parse.Rules.Common
{
    public class ExtraTermRule : BaseRule
    {
        public ExtraTermRule(ArithmeticOperatorRule arithmeticOperator, TermRule term, ExtraTermRule extraTerm)
        {
            ArithmeticOperator = arithmeticOperator;
            Term = term;
            ExtraTerm = extraTerm;
        }

        public ExtraTermRule()
        {
        }

        public ArithmeticOperatorRule ArithmeticOperator { get; set; }
        public TermRule Term { get; set; }
        public ExtraTermRule ExtraTerm { get; set; }
    }
}
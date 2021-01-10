using Teeny.Core.Parse.Rules.Operators;

namespace Teeny.Core.Parse.Rules.Equation
{
    public class ExtraEquationRule : BaseRule
    {
        public ExtraEquationRule(ArithmeticOperatorRule arithmeticOperator, AnyEquationRule anyEquation)
        {
            ArithmeticOperator = arithmeticOperator;
            AnyEquation = anyEquation;
        }

        public ExtraEquationRule()
        {
        }

        public ArithmeticOperatorRule ArithmeticOperator { get; set; }

        public AnyEquationRule AnyEquation { get; set; }
    }
}
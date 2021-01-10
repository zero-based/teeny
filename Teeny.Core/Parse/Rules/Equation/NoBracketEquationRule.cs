using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Equation
{
    public class NoBracketEquationRule : BaseRule
    {
        public NoBracketEquationRule(TermRule term1, TokenRecord arithmeticOperator, TermRule term2,
            ExtraTermRule extraTerm)
        {
            Term1 = term1;
            ArithmeticOperator.Assign(arithmeticOperator);
            Term2 = term2;
            ExtraTerm = extraTerm;
        }

        public NoBracketEquationRule()
        {
        }

        public TermRule Term1 { get; set; }
        public TerminalNode ArithmeticOperator { get; set; } =
            new TerminalNode(Token.Plus, Token.Minus, Token.Multiply, Token.Divide);
        public TermRule Term2 { get; set; }
        public ExtraTermRule ExtraTerm { get; set; }
    }
}
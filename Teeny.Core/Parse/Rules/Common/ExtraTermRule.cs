using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Common
{
    public class ExtraTermRule : BaseRule
    {
        public ExtraTermRule(TokenRecord arithmeticOperator, TermRule term, ExtraTermRule extraTerm)
        {
            ArithmeticOperator.Assign(arithmeticOperator);
            Term = term;
            ExtraTerm = extraTerm;
        }

        public ExtraTermRule()
        {
        }

        public TerminalNode ArithmeticOperator { get; set; } =
            new TerminalNode(Token.Plus, Token.Minus, Token.Multiply, Token.Divide);
        public TermRule Term { get; set; }
        public ExtraTermRule ExtraTerm { get; set; }
    }
}
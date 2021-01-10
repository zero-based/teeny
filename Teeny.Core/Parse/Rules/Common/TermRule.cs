using Teeny.Core.Parse.Rules.Function;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Common
{
    public class TermRule : BaseRule
    {
        public TermRule(NumberRule number)
        {
            Number = number;
        }

        public TermRule(TokenRecord identifier)
        {
            Identifier.Assign(identifier);
        }

        public TermRule(FunctionCallRule functionCall)
        {
            FunctionCall = functionCall;
        }

        public NumberRule Number { get; set; }

        public TerminalNode Identifier { get; set; } = new TerminalNode(Token.Identifier);

        public FunctionCallRule FunctionCall { get; set; }
    }
}
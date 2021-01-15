using Teeny.Core.Parse.Rules.Function;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Common
{
    public class TermRule : BaseRule
    {
        public TermRule(NumberRule number)
        {
            Number = Guard.NonNull(() => number);
        }

        public TermRule(TerminalNode identifier)
        {
            Identifier = Guard.OneOf(() => identifier, Token.Identifier);
        }

        public TermRule(FunctionCallRule functionCall)
        {
            FunctionCall = Guard.NonNull(() => functionCall);
        }

        public TermRule()
        {
        }

        public NumberRule Number { get; set; }

        public TerminalNode Identifier { get; set; }

        public FunctionCallRule FunctionCall { get; set; }
    }
}
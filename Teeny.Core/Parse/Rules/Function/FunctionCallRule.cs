using Teeny.Core.Parse.Rules.Function.Arguments;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function
{
    public class FunctionCallRule : BaseRule
    {
        public FunctionCallRule(TerminalNode identifier, TerminalNode parenthesisLeft,
            ArgumentsRule arguments, TerminalNode parenthesisRight)
        {
            Identifier = Guard.OneOf(() => identifier, Token.Identifier);
            ParenthesisLeft = Guard.OneOf(() => parenthesisLeft, Token.ParenthesisLeft);
            Arguments = arguments;
            ParenthesisRight = Guard.OneOf(() => parenthesisRight, Token.ParenthesisRight);
        }

        public FunctionCallRule()
        {
        }

        public TerminalNode Identifier { get; set; }
        public TerminalNode ParenthesisLeft { get; set; }
        public ArgumentsRule Arguments { get; set; }
        public TerminalNode ParenthesisRight { get; set; }
    }
}
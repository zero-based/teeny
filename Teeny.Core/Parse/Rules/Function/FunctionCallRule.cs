using Teeny.Core.Parse.Rules.Function.Arguments;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function
{
    public class FunctionCallRule : BaseRule
    {
        public FunctionCallRule(TokenRecord identifier, TokenRecord parenthesisLeft, ArgumentsRule arguments,
            TokenRecord parenthesisRight)
        {
            Identifier.Assign(identifier);
            ParenthesisLeft.Assign(parenthesisLeft);
            Arguments = arguments;
            ParenthesisRight.Assign(parenthesisRight);
        }

        public TerminalNode Identifier { get; set; } = new TerminalNode(Token.Identifier);
        public TerminalNode ParenthesisLeft { get; set; } = new TerminalNode(Token.ParenthesisLeft);
        public ArgumentsRule Arguments { get; set; }
        public TerminalNode ParenthesisRight { get; set; } = new TerminalNode(Token.ParenthesisRight);
    }
}
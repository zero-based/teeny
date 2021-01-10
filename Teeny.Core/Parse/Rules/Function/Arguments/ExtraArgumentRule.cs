using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function.Arguments
{
    public class ExtraArgumentRule
    {
        public ExtraArgumentRule(TokenRecord comma, TokenRecord identifier)
        {
            Comma.Assign(comma);
            Identifier.Assign(identifier);
        }

        public ExtraArgumentRule()
        {
        }

        public TerminalNode Comma { get; set; } = new TerminalNode(Token.Comma);
        public TerminalNode Identifier { get; set; } = new TerminalNode(Token.Identifier);
    }
}
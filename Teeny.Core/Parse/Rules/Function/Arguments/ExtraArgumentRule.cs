using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function.Arguments
{
    public class ExtraArgumentRule : BaseRule
    {
        public ExtraArgumentRule(TerminalNode comma, TerminalNode identifier)
        {
            Comma = Guard.OneOf(() => comma, Token.Comma);
            Identifier = Guard.OneOf(() => identifier, Token.Identifier);
        }

        public ExtraArgumentRule()
        {
        }

        public TerminalNode Comma { get; set; }
        public TerminalNode Identifier { get; set; }
    }
}
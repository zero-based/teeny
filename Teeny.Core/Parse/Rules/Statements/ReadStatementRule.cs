using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements
{
    public class ReadStatementRule : StatementRule
    {
        public ReadStatementRule(TerminalNode read, TerminalNode identifier, TerminalNode semicolon)
        {
            Read = Guard.OneOf(() => read, Token.Read);
            Identifier = Guard.OneOf(() => identifier, Token.Identifier);
            Semicolon = Guard.OneOf(() => semicolon, Token.Semicolon);
        }

        public ReadStatementRule()
        {
        }

        public TerminalNode Read { get; set; }
        public TerminalNode Identifier { get; set; }
        public TerminalNode Semicolon { get; set; }
    }
}
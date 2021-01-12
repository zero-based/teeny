using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements
{
    public class ReadStatementRule : StatementRule
    {
        public ReadStatementRule(TokenRecord read, TokenRecord identifier, TokenRecord semicolon)
        {
            Read.Assign(read);
            Identifier.Assign(identifier);
            Semicolon.Assign(semicolon);
        }

        public ReadStatementRule()
        {
        }

        public TerminalNode Read { get; set; } = new TerminalNode(Token.Read);
        public TerminalNode Identifier { get; set; } = new TerminalNode(Token.Identifier);
        public TerminalNode Semicolon { get; set; } = new TerminalNode(Token.Semicolon);
    }
}
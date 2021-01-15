using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements
{
    public class WriteStatementRule : StatementRule
    {
        public WriteStatementRule(TerminalNode write, ExpressionRule expression, TerminalNode semicolon)
        {
            Write = Guard.OneOf(() => write, Token.Write);
            Expression = Guard.NonNull(() => expression);
            Semicolon = Guard.OneOf(() => semicolon, Token.Semicolon);
        }

        public WriteStatementRule(TerminalNode write, TerminalNode endl, TerminalNode semicolon)
        {
            Write = Guard.OneOf(() => write, Token.Write);
            Endl = Guard.OneOf(() => endl, Token.Endl);
            Semicolon = Guard.OneOf(() => semicolon, Token.Semicolon);
        }

        public WriteStatementRule()
        {
        }

        public TerminalNode Write { get; set; }
        public ExpressionRule Expression { get; set; }
        public TerminalNode Endl { get; set; }
        public TerminalNode Semicolon { get; set; }
    }
}
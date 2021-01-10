using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements
{
    public class WriteStatementRule : BaseRule
    {
        public WriteStatementRule(TokenRecord write, ExpressionRule expression, TokenRecord semicolon)
        {
            Write.Assign(write);
            Expression = expression;
            Semicolon.Assign(semicolon);
        }

        public WriteStatementRule(TokenRecord write, TokenRecord endl, TokenRecord semicolon)
        {
            Write.Assign(write);
            Endl.Assign(endl);
            Semicolon.Assign(semicolon);
        }

        public TerminalNode Write { get; set; } = new TerminalNode(Token.Write);
        public ExpressionRule Expression { get; set; }
        public TerminalNode Endl { get; set; } = new TerminalNode(Token.Endl);
        public TerminalNode Semicolon { get; set; } = new TerminalNode(Token.Semicolon);
    }
}
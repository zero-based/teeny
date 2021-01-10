using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements
{
    public class ReturnStatementRule : BaseRule
    {
        public ReturnStatementRule(TokenRecord @return, ExpressionRule expression, TokenRecord semicolon)
        {
            Return.Assign(@return);
            Expression = expression;
            Semicolon.Assign(semicolon);
        }

        public TerminalNode Return { get; set; } = new TerminalNode(Token.Return);
        public ExpressionRule Expression { get; set; }
        public TerminalNode Semicolon { get; set; } = new TerminalNode(Token.Semicolon);
    }
}
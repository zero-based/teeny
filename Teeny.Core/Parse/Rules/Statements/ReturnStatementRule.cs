using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements
{
    public class ReturnStatementRule : StatementRule
    {
        public ReturnStatementRule(TerminalNode @return, ExpressionRule expression, TerminalNode semicolon)
        {
            Return = Guard.OneOf(() => @return, Token.Return);
            Expression = Guard.NonNull(() => expression);
            Semicolon = Guard.OneOf(() => semicolon, Token.Semicolon);
        }

        public ReturnStatementRule()
        {
        }

        public TerminalNode Return { get; set; }
        public ExpressionRule Expression { get; set; }
        public TerminalNode Semicolon { get; set; }
    }
}
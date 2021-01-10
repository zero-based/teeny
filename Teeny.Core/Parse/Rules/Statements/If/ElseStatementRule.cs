using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.If
{
    public class ElseStatementRule : BaseRule
    {
        public ElseStatementRule(TokenRecord @else, StatementRule statement, TokenRecord end)
        {
            Else.Assign(@else);
            Statement = statement;
            End.Assign(end);
        }

        public TerminalNode Else { get; set; } = new TerminalNode(Token.Else);
        public StatementRule Statement { get; set; }
        public TerminalNode End { get; set; } = new TerminalNode(Token.End);
    }
}
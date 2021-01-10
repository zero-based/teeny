using System.Collections.Generic;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.If
{
    public class ElseStatementRule : BaseRule
    {
        public ElseStatementRule(TokenRecord @else, ICollection<StatementRule> statements, TokenRecord end)
        {
            Else.Assign(@else);
            Statements = statements;
            End.Assign(end);
        }

        public TerminalNode Else { get; set; } = new TerminalNode(Token.Else);
        public ICollection<StatementRule> Statements { get; set; }
        public TerminalNode End { get; set; } = new TerminalNode(Token.End);
    }
}
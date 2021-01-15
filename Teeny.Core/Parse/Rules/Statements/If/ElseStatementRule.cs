using System.Collections.Generic;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.If
{
    public class ElseStatementRule : BaseRule
    {
        public ElseStatementRule(TerminalNode @else, ICollection<StatementRule> statements, TerminalNode end)
        {
            Else = Guard.OneOf(() => @else, Token.Else);
            Statements = statements;
            End = Guard.OneOf(() => end, Token.End);
        }

        public ElseStatementRule()
        {
        }

        public TerminalNode Else { get; set; }
        public ICollection<StatementRule> Statements { get; set; }
        public TerminalNode End { get; set; }
    }
}
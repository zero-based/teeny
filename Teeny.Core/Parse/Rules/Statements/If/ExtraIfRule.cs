using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.If
{
    public class ExtraIfRule : BaseRule
    {
        public ExtraIfRule(ElseIfStatementRule elseIfStatement, ElseStatementRule elseStatement, TokenRecord end)
        {
            ElseIfStatement = elseIfStatement;
            ElseStatement = elseStatement;
            End.Assign(end);
        }

        public ExtraIfRule()
        {
        }

        public ElseIfStatementRule ElseIfStatement { get; set; }
        public ElseStatementRule ElseStatement { get; set; }
        public TerminalNode End { get; set; } = new TerminalNode(Token.End);
    }
}
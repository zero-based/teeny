using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.If
{
    public class ExtraElseIfRule : BaseRule
    {
        public ExtraElseIfRule(ElseIfStatementRule elseIfStatement, ElseStatementRule elseStatement, TokenRecord end)
        {
            ElseIfStatement = elseIfStatement;
            ElseStatement = elseStatement;
            End.Assign(end);
        }

        public ExtraElseIfRule()
        {
        }

        public ElseIfStatementRule ElseIfStatement { get; set; }
        public ElseStatementRule ElseStatement { get; set; }
        public TerminalNode End { get; set; } = new TerminalNode(Token.End);
    }
}
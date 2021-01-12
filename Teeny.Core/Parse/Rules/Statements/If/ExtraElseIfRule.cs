using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.If
{
    public class ExtraElseIfRule : BaseRule
    {
        public ExtraElseIfRule(ElseIfStatementRule elseIfStatement)
        {
            ElseIfStatement = elseIfStatement;
        }

        public ExtraElseIfRule(ElseStatementRule elseStatement)
        {
            ElseStatement = elseStatement;
        }

        public ExtraElseIfRule(TokenRecord end)
        {
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
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.If
{
    public class ExtraElseIfRule : BaseRule
    {
        public ExtraElseIfRule(ElseIfStatementRule elseIfStatement)
        {
            ElseIfStatement = Guard.NonNull(() => elseIfStatement);
        }

        public ExtraElseIfRule(ElseStatementRule elseStatement)
        {
            ElseStatement = Guard.NonNull(() => elseStatement);
        }

        public ExtraElseIfRule(TerminalNode end)
        {
            End = Guard.OneOf(() => end, Token.End);
        }

        public ExtraElseIfRule()
        {
        }

        public ElseIfStatementRule ElseIfStatement { get; set; }
        public ElseStatementRule ElseStatement { get; set; }
        public TerminalNode End { get; set; }
    }
}
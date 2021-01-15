using System.Collections.Generic;
using Teeny.Core.Parse.Rules.Statements;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function
{
    public class FunctionBodyRule : BaseRule
    {
        public FunctionBodyRule(TerminalNode leftCurlyBracket, ICollection<StatementRule> statements,
            ReturnStatementRule returnStatement, TerminalNode rightCurlyBracket)
        {
            LeftCurlyBracket = Guard.OneOf(() => leftCurlyBracket, Token.CurlyBracketLeft);
            Statements = statements;
            ReturnStatement = Guard.NonNull(() => returnStatement);
            RightCurlyBracket = Guard.OneOf(() => rightCurlyBracket, Token.CurlyBracketRight);
        }

        public FunctionBodyRule()
        {
        }

        public TerminalNode LeftCurlyBracket { get; set; }
        public ICollection<StatementRule> Statements { get; set; }
        public ReturnStatementRule ReturnStatement { get; set; }
        public TerminalNode RightCurlyBracket { get; set; }
    }
}
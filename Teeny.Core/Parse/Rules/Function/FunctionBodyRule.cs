using System.Collections.Generic;
using Teeny.Core.Parse.Rules.Statements;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function
{
    public class FunctionBodyRule : BaseRule
    {
        public FunctionBodyRule(TokenRecord leftCurlyBracket,
            ICollection<StatementRule> statements, ReturnStatementRule returnStatement, TokenRecord rightCurlyBracket)
        {
            LeftCurlyBracket.Assign(leftCurlyBracket);
            Statements = statements;
            ReturnStatement = returnStatement;
            RightCurlyBracket.Assign(rightCurlyBracket);
        }

        public FunctionBodyRule()
        {
        }

        public TerminalNode RightCurlyBracket { get; set; } = new TerminalNode(Token.CurlyBracketRight);
        public ICollection<StatementRule> Statements { get; set; }
        public ReturnStatementRule ReturnStatement { get; set; }
        public TerminalNode LeftCurlyBracket { get; set; } = new TerminalNode(Token.CurlyBracketLeft);
    }
}
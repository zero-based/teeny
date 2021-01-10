using System.Collections.Generic;
using Teeny.Core.Parse.Rules.Statements;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function
{
    public class FunctionBodyRule : BaseRule
    {
        public FunctionBodyRule(TokenRecord leftCurlyBracket, TokenRecord rightCurlyBracket,
            ICollection<StatementRule> statements, ReturnStatementRule returnStatement)
        {
            LeftCurlyBracket.Assign(leftCurlyBracket);
            RightCurlyBracket.Assign(rightCurlyBracket);
            Statements = statements;
            ReturnStatement = returnStatement;
        }

        public FunctionBodyRule()
        {
        }

        public TerminalNode LeftCurlyBracket { get; set; } = new TerminalNode(Token.CurlyBracketLeft);
        public TerminalNode RightCurlyBracket { get; set; } = new TerminalNode(Token.CurlyBracketRight);
        public ICollection<StatementRule> Statements { get; set; }
        public ReturnStatementRule ReturnStatement { get; set; }
    }
}
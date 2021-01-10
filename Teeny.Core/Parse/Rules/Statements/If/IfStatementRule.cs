using System.Collections.Generic;
using Teeny.Core.Parse.Rules.Statements.Condition;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.If
{
    public class IfStatementRule : BaseRule
    {
        public IfStatementRule(TokenRecord @if, ConditionStatementRule conditionStatement, TokenRecord then,
            ICollection<StatementRule> statements, ExtraIfRule extraIf)
        {
            If.Assign(@if);
            ConditionStatement = conditionStatement;
            Then.Assign(then);
            Statements = statements;
            ExtraIf = extraIf;
        }

        public TerminalNode If { get; set; } = new TerminalNode(Token.If);
        public ConditionStatementRule ConditionStatement { get; set; }
        public TerminalNode Then { get; set; } = new TerminalNode(Token.Then);
        public ICollection<StatementRule> Statements { get; set; }
        public ExtraIfRule ExtraIf { get; set; }
    }
}
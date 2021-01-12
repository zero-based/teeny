using System.Collections.Generic;
using Teeny.Core.Parse.Rules.Statements.Condition;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.If
{
    public class IfStatementRule : StatementRule
    {
        public IfStatementRule(TokenRecord @if, ConditionStatementRule conditionStatement, TokenRecord then,
            ICollection<StatementRule> statements, ExtraElseIfRule extraElseIf)
        {
            If.Assign(@if);
            ConditionStatement = conditionStatement;
            Then.Assign(then);
            Statements = statements;
            ExtraElseIf = extraElseIf;
        }

        public IfStatementRule()
        {
        }

        public TerminalNode If { get; set; } = new TerminalNode(Token.If);
        public ConditionStatementRule ConditionStatement { get; set; }
        public TerminalNode Then { get; set; } = new TerminalNode(Token.Then);
        public ICollection<StatementRule> Statements { get; set; }
        public ExtraElseIfRule ExtraElseIf { get; set; }
    }
}
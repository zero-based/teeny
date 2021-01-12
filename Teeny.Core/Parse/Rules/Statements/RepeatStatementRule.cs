using System.Collections.Generic;
using Teeny.Core.Parse.Rules.Statements.Condition;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements
{
    public class RepeatStatementRule : StatementRule
    {
        public RepeatStatementRule(TokenRecord repeat, ICollection<StatementRule> statements, TokenRecord until,
            ConditionStatementRule conditionStatement)
        {
            Repeat.Assign(repeat);
            Statements = statements;
            Until.Assign(until);
            ConditionStatement = conditionStatement;
        }

        public RepeatStatementRule()
        {
        }

        public TerminalNode Repeat { get; set; } = new TerminalNode(Token.Repeat);
        public ICollection<StatementRule> Statements { get; set; }
        public TerminalNode Until { get; set; } = new TerminalNode(Token.Until);
        public ConditionStatementRule ConditionStatement { get; set; }
    }
}
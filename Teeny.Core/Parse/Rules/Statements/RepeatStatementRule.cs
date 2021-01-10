using Teeny.Core.Parse.Rules.Statements.Condition;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements
{
    public class RepeatStatementRule : BaseRule
    {
        public RepeatStatementRule(TokenRecord repeat, StatementRule statement, TokenRecord until,
            ConditionStatementRule conditionStatement)
        {
            Repeat.Assign(repeat);
            Statement = statement;
            Until.Assign(until);
            ConditionStatement = conditionStatement;
        }

        public RepeatStatementRule()
        {
        }

        public TerminalNode Repeat { get; set; } = new TerminalNode(Token.Repeat);
        public StatementRule Statement { get; set; }
        public TerminalNode Until { get; set; } = new TerminalNode(Token.Until);
        public ConditionStatementRule ConditionStatement { get; set; }
    }
}
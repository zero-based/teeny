using System.Collections.Generic;
using Teeny.Core.Parse.Rules.Statements.Condition;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements
{
    public class RepeatStatementRule : StatementRule
    {
        public RepeatStatementRule(TerminalNode repeat, ICollection<StatementRule> statements,
            TerminalNode until, ConditionStatementRule conditionStatement)
        {
            Repeat = Guard.OneOf(() => repeat, Token.Repeat);
            Statements = statements;
            Until = Guard.OneOf(() => until, Token.Until);
            ConditionStatement = Guard.NonNull(() => conditionStatement);
        }

        public RepeatStatementRule()
        {
        }

        public TerminalNode Repeat { get; set; }
        public ICollection<StatementRule> Statements { get; set; }
        public TerminalNode Until { get; set; }
        public ConditionStatementRule ConditionStatement { get; set; }
    }
}
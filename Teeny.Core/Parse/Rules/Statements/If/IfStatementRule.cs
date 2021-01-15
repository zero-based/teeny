using System.Collections.Generic;
using Teeny.Core.Parse.Rules.Statements.Condition;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.If
{
    public class IfStatementRule : StatementRule
    {
        public IfStatementRule(TerminalNode @if, ConditionStatementRule conditionStatement, TerminalNode then,
            ICollection<StatementRule> statements, ExtraElseIfRule extraElseIf)
        {
            If = Guard.OneOf(() => @if, Token.If);
            ConditionStatement = Guard.NonNull(() => conditionStatement);
            Then = Guard.OneOf(() => then, Token.Then);
            Statements = statements;
            ExtraElseIf = Guard.NonNull(() => extraElseIf);
        }

        public IfStatementRule()
        {
        }

        public TerminalNode If { get; set; }
        public ConditionStatementRule ConditionStatement { get; set; }
        public TerminalNode Then { get; set; }
        public ICollection<StatementRule> Statements { get; set; }
        public ExtraElseIfRule ExtraElseIf { get; set; }
    }
}
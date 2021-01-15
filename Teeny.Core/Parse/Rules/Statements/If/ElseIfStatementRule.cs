using System.Collections.Generic;
using Teeny.Core.Parse.Rules.Statements.Condition;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.If
{
    public class ElseIfStatementRule : BaseRule
    {
        public ElseIfStatementRule(TerminalNode elseIf, ConditionStatementRule conditionStatement, TerminalNode then,
            ICollection<StatementRule> statements, ExtraElseIfRule extraElseIf)
        {
            ElseIf = Guard.OneOf(() => elseIf, Token.ElseIf);
            ConditionStatement = Guard.NonNull(() => conditionStatement);
            Then = Guard.OneOf(() => then, Token.Then);
            Statements = statements;
            ExtraElseIf = Guard.NonNull(() => extraElseIf);
        }

        public ElseIfStatementRule()
        {
        }

        public TerminalNode ElseIf { get; set; }
        public ConditionStatementRule ConditionStatement { get; set; }
        public TerminalNode Then { get; set; }
        public ICollection<StatementRule> Statements { get; set; }
        public ExtraElseIfRule ExtraElseIf { get; set; }
    }
}
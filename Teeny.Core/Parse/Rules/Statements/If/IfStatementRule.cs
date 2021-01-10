using Teeny.Core.Parse.Rules.Statements.Condition;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.If
{
    public class IfStatementRule : BaseRule
    {
        public IfStatementRule(TokenRecord @if, ConditionStatementRule conditionStatement, TokenRecord then,
            StatementRule statement, ExtraIfRule extraIf)
        {
            If.Assign(@if);
            ConditionStatement = conditionStatement;
            Then.Assign(then);
            Statement = statement;
            ExtraIf = extraIf;
        }

        public IfStatementRule()
        {
        }

        public TerminalNode If { get; set; } = new TerminalNode(Token.If);
        public ConditionStatementRule ConditionStatement { get; set; }
        public TerminalNode Then { get; set; } = new TerminalNode(Token.Then);
        public StatementRule Statement { get; set; }
        public ExtraIfRule ExtraIf { get; set; }
    }
}
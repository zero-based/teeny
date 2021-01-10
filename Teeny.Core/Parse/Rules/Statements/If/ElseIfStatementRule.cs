using Teeny.Core.Parse.Rules.Statements.Condition;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.If
{
    public class ElseIfStatementRule : BaseRule
    {
        public ElseIfStatementRule(TokenRecord elseIf, ConditionStatementRule conditionStatement, TokenRecord then,
            StatementRule statement, ExtraElseIfRule extraElseIf)
        {
            ElseIf.Assign(elseIf);
            ConditionStatement = conditionStatement;
            Then.Assign(then);
            Statement = statement;
            ExtraElseIf = extraElseIf;
        }

        public TerminalNode ElseIf { get; set; } = new TerminalNode(Token.ElseIf);
        public ConditionStatementRule ConditionStatement { get; set; }
        public TerminalNode Then { get; set; } = new TerminalNode(Token.Then);
        public StatementRule Statement { get; set; }
        public ExtraElseIfRule ExtraElseIf { get; set; }
    }
}
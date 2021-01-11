﻿using System.Collections.Generic;
using Teeny.Core.Parse.Rules.Statements.Condition;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.If
{
    public class ElseIfStatementRule : BaseRule
    {
        public ElseIfStatementRule(TokenRecord elseIf, ConditionStatementRule conditionStatement, TokenRecord then,
            ICollection<StatementRule> statements, ExtraElseIfRule extraElseIf)
        {
            ElseIf.Assign(elseIf);
            ConditionStatement = conditionStatement;
            Then.Assign(then);
            Statements = statements;
            ExtraElseIf = extraElseIf;
        }

        public ElseIfStatementRule()
        {
        }

        public TerminalNode ElseIf { get; set; } = new TerminalNode(Token.ElseIf);
        public ConditionStatementRule ConditionStatement { get; set; }
        public TerminalNode Then { get; set; } = new TerminalNode(Token.Then);
        public ICollection<StatementRule> Statements { get; set; }
        public ExtraElseIfRule ExtraElseIf { get; set; }
    }
}
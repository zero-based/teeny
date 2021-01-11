﻿using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements
{
    public class AssignmentStatementRule : StatementRule
    {
        public AssignmentStatementRule(TokenRecord identifier, TokenRecord assignmentOperator,
            ExpressionRule expression, TokenRecord semicolon)
        {
            Identifier.Assign(identifier);
            AssignmentOperator.Assign(assignmentOperator);
            Expression = expression;
            Semicolon.Assign(semicolon);
        }

        public AssignmentStatementRule()
        {
        }

        public TerminalNode Identifier { get; set; } = new TerminalNode(Token.Identifier);
        public TerminalNode AssignmentOperator { get; set; } = new TerminalNode(Token.Assignment);
        public ExpressionRule Expression { get; set; }
        public TerminalNode Semicolon { get; set; } = new TerminalNode(Token.Semicolon);
    }
}
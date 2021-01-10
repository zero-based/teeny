using System;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.Declaration
{
    public class ExtraIdOrAssignRule
    {
        public ExtraIdOrAssignRule(TokenRecord comma, IdOrAssignmentRule idOrAssignment,
            ExtraIdOrAssignRule extraIdOrAssign)
        {
            Comma.Assign(comma);
            IdOrAssignment = idOrAssignment;
            ExtraIdOrAssign = extraIdOrAssign;
        }

        public ExtraIdOrAssignRule()
        {
        }

        public TerminalNode Comma { get; set; } = new TerminalNode(Token.Comma);
        public IdOrAssignmentRule IdOrAssignment { get; set; }
        public ExtraIdOrAssignRule ExtraIdOrAssign { get; set; }
    }
}
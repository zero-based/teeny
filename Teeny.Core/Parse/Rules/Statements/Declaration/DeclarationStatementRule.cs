using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.Declaration
{
    public class DeclarationStatementRule : BaseRule
    {
        public DeclarationStatementRule(TokenRecord dataType, IdOrAssignmentRule idOrAssignment,
            ExtraIdOrAssignRule extraIdOrAssign, TokenRecord semicolon)
        {
            DataType.Assign(dataType);
            IdOrAssignment = idOrAssignment;
            ExtraIdOrAssign = extraIdOrAssign;
            Semicolon.Assign(semicolon);
        }

        public DeclarationStatementRule()
        {
        }

        public TerminalNode DataType { get; set; } = new TerminalNode(Token.Int, Token.Float, Token.String);
        public IdOrAssignmentRule IdOrAssignment { get; set; }
        public ExtraIdOrAssignRule ExtraIdOrAssign { get; set; }
        public TerminalNode Semicolon { get; set; } = new TerminalNode(Token.Semicolon);
    }
}
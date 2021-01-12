using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.Declaration
{
    public class DeclarationStatementRule : StatementRule
    {
        public DeclarationStatementRule(TokenRecord dataType, IdOrAssignmentRule idOrAssignment,
            ExtraIdOrAssignmentRule extraIdOrAssignment, TokenRecord semicolon)
        {
            DataType.Assign(dataType);
            IdOrAssignment = idOrAssignment;
            ExtraIdOrAssignment = extraIdOrAssignment;
            Semicolon.Assign(semicolon);
        }

        public DeclarationStatementRule()
        {
        }

        public TerminalNode DataType { get; set; } = new TerminalNode(Token.Int, Token.Float, Token.String);
        public IdOrAssignmentRule IdOrAssignment { get; set; }
        public ExtraIdOrAssignmentRule ExtraIdOrAssignment { get; set; }
        public TerminalNode Semicolon { get; set; } = new TerminalNode(Token.Semicolon);
    }
}
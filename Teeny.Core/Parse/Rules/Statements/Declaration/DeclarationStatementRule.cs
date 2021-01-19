using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.Declaration
{
    public class DeclarationStatementRule : StatementRule
    {
        public DeclarationStatementRule(TerminalNode dataType, IdOrAssignmentRule idOrAssignment,
            ExtraIdOrAssignmentRule extraIdOrAssignment, TerminalNode semicolon)
        {
            DataType = Guard.OneOf(() => dataType, TokensGroups.DataTypes);
            IdOrAssignment = Guard.NonNull(() => idOrAssignment);
            ExtraIdOrAssignment = extraIdOrAssignment;
            Semicolon = Guard.OneOf(() => semicolon, Token.Semicolon);
        }

        public DeclarationStatementRule()
        {
        }

        public TerminalNode DataType { get; set; }
        public IdOrAssignmentRule IdOrAssignment { get; set; }
        public ExtraIdOrAssignmentRule ExtraIdOrAssignment { get; set; }
        public TerminalNode Semicolon { get; set; }
    }
}
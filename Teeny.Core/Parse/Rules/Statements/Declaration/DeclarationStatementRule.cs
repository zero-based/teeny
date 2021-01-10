using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Statements.Declaration
{
    public class DeclarationStatementRule : BaseRule
    {
        public DeclarationStatementRule(DataTypeRule dataType, IdOrAssignmentRule idOrAssignment,
            ExtraIdOrAssignRule extraIdOrAssign, TokenRecord semicolon)
        {
            DataType = dataType;
            IdOrAssignment = idOrAssignment;
            ExtraIdOrAssign = extraIdOrAssign;
            Semicolon.Assign(semicolon);
        }

        public DeclarationStatementRule()
        {
        }

        public DataTypeRule DataType { get; set; }
        public IdOrAssignmentRule IdOrAssignment { get; set; }
        public ExtraIdOrAssignRule ExtraIdOrAssign { get; set; }
        public TerminalNode Semicolon { get; set; } = new TerminalNode(Token.Semicolon);
    }
}
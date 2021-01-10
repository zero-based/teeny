using Teeny.Core.Parse.Rules.Statements.Declaration;
using Teeny.Core.Parse.Rules.Statements.If;

namespace Teeny.Core.Parse.Rules.Statements
{
    public class StatementRule : BaseRule
    {
        public StatementRule(AssignmentStatementRule assignmentStatement)
        {
            AssignmentStatement = assignmentStatement;
        }

        public StatementRule(DeclarationStatementRule declarationStatement)
        {
            DeclarationStatement = declarationStatement;
        }

        public StatementRule(WriteStatementRule writeStatement)
        {
            WriteStatement = writeStatement;
        }

        public StatementRule(ReadStatementRule readStatement)
        {
            ReadStatement = readStatement;
        }

        public StatementRule(IfStatementRule ifStatement)
        {
            IfStatement = ifStatement;
        }

        public StatementRule(RepeatStatementRule repeatStatement)
        {
            RepeatStatement = repeatStatement;
        }

        public StatementRule(ReturnStatementRule returnStatement)
        {
            ReturnStatement = returnStatement;
        }

        public StatementRule()
        {
        }

        public AssignmentStatementRule AssignmentStatement { get; set; }
        public DeclarationStatementRule DeclarationStatement { get; set; }
        public WriteStatementRule WriteStatement { get; set; }
        public ReadStatementRule ReadStatement { get; set; }
        public IfStatementRule IfStatement { get; set; }
        public RepeatStatementRule RepeatStatement { get; set; }
        public ReturnStatementRule ReturnStatement { get; set; }
    }
}
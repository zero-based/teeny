namespace Teeny.Core.Parse.Rules.Function
{
    public class FunctionStatementRule : BaseRule
    {
        public FunctionStatementRule(FunctionDeclarationRule functionDeclaration, FunctionBodyRule functionBody)
        {
            FunctionDeclaration = Guard.NonNull(() => functionDeclaration);
            FunctionBody = Guard.NonNull(() => functionBody);
        }

        public FunctionStatementRule()
        {
        }

        public FunctionDeclarationRule FunctionDeclaration { get; set; }
        public FunctionBodyRule FunctionBody { get; set; }
    }
}
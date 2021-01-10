namespace Teeny.Core.Parse.Rules.Function
{
    public class FunctionStatementRule : BaseRule
    {
        public FunctionStatementRule(FunctionDeclarationRule functionDeclaration, FunctionBodyRule functionBody)
        {
            FunctionDeclaration = functionDeclaration;
            FunctionBody = functionBody;
        }

        public FunctionStatementRule()
        {
        }

        public FunctionDeclarationRule FunctionDeclaration { get; set; }
        public FunctionBodyRule FunctionBody { get; set; }
    }
}